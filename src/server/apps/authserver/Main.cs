﻿/*
 * This file is part of the AzerothCore Project. See AUTHORS file for Copyright information
 *
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU Affero General Public License as published by the
 * Free Software Foundation; either version 3 of the License, or (at your
 * option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for
 * more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with this program. If not, see <http://www.gnu.org/licenses/>.
 */

using System.Globalization;
using System.Timers;

using AzerothCore.Configuration;
using AzerothCore.Database;
using AzerothCore.Logging;
using AzerothCore.Networking;
using AzerothCore.Realms;

namespace AzerothCore;

public class AuthServer
{
    private static readonly Logger logger = LoggerFactory.GetLogger();

    private static System.Timers.Timer? _banExpiryCheckTimer;

    public static void Main()
	{
        // Set Culture
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

        if (!ConfigMgr.LoadAppConfigs("authserver.conf"))
        {
            ExitNow();
        }

        Banner.Show();

        // Initialize the database connection
        if (!StartDB())
        {
            ExitNow();
        }

        // Get the list of realms for the server
        RealmList.Instance.Initialize(ConfigMgr.GetValueOrDefault("RealmsStateUpdateDelay", 20));

        if (RealmList.Instance == null || RealmList.Instance.GetRealms().Empty())
        {
            logger.Error(LogFilter.ServerLoading, "No valid realms specified.");
            ExitNow();
        }

        // Start the listening port(acceptor) for auth connections
        string bindIp = ConfigMgr.GetValueOrDefault("BindIP", "0.0.0.0");
        int port = ConfigMgr.GetValueOrDefault("RealmServerPort", 3724);

        if (port < 0 || port > 0xFFFF)
        {
            logger.Error(LogFilter.ServerLoading, "Specified port out of allowed range (1-65535)");
            ExitNow();
        }

        if (!AuthSocketManager.Instance.StartNetwork(bindIp, port))
        {
            logger.Error(LogFilter.ServerLoading, "Failed to start authserver Network");
            ExitNow();
        }

        uint _banExpiryCheckInterval = ConfigMgr.GetValueOrDefault("BanExpiryCheckInterval", 60u);
        _banExpiryCheckTimer = new System.Timers.Timer(_banExpiryCheckInterval);
        _banExpiryCheckTimer.Elapsed += BanExpiryCheckTimer_Elapsed;
        _banExpiryCheckTimer.Start();

        logger.Info(LogFilter.ServerLoading, "authserver started");
    }

    private static bool StartDB()
    {
        DatabaseLoader loader = new(DatabaseTypeFlags.None);

        loader.AddDatabase(DB.Login, "Login");

        if (!loader.Load())
        {
            return false;
        }

        return true;
    }

    private static void BanExpiryCheckTimer_Elapsed(object? sender, ElapsedEventArgs? e)
    {
        DB.Login.Execute(LoginDatabase.GetPreparedStatement(LoginStatements.LOGIN_DEL_EXPIRED_IP_BANS));
        DB.Login.Execute(LoginDatabase.GetPreparedStatement(LoginStatements.LOGIN_UPD_EXPIRED_ACCOUNT_BANS));
    }

    static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
    {
        Exception ex = (Exception)e.ExceptionObject;

        logger.Fatal(LogFilter.Server, ex);
    }

    private static void ExitNow()
    {
        Console.WriteLine("Halting process...");
        System.Threading.Thread.Sleep(2000);

        Environment.Exit(-1);
    }
}
