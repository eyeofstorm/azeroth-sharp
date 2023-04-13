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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzerothCore.Database;

public class SQLQueryHolder<T> where T : notnull
{
    public Dictionary<T, PreparedStatement> m_queries = new();
    Dictionary<T, SQLResult> _results = new();

    public void SetQuery(T index, string sql, params object[] args)
    {
        SetQuery(index, new PreparedStatement(string.Format(sql, args)));
    }

    public void SetQuery(T index, PreparedStatement stmt)
    {
        m_queries[index] = stmt;
    }

    public void SetResult(T index, SQLResult result)
    {
        _results[index] = result;
    }

    public SQLResult GetResult(T index)
    {
        if (!_results.ContainsKey(index))
            return new SQLResult();

        return _results[index];
    }
}

class SQLQueryHolderTask<R> : ISqlOperation where R : notnull
{
    SQLQueryHolder<R> m_holder;
    TaskCompletionSource<SQLQueryHolder<R>> m_result;

    public SQLQueryHolderTask(SQLQueryHolder<R> holder)
    {
        m_holder = holder;
        m_result = new TaskCompletionSource<SQLQueryHolder<R>>();
    }

    public bool Execute<T>(MySqlBase<T> mySqlBase) where T : notnull
    {
        if (m_holder == null)
            return false;

        // execute all queries in the holder and pass the results
        foreach (var pair in m_holder.m_queries)
            m_holder.SetResult(pair.Key, mySqlBase.Query(pair.Value));

        return m_result.TrySetResult(m_holder);
    }

    public Task<SQLQueryHolder<R>> GetFuture() { return m_result.Task; }
}

public class SQLQueryHolderCallback<R> : ISqlCallback where R : notnull
{
    Task<SQLQueryHolder<R>> m_future;
    Action<SQLQueryHolder<R>>? m_callback;

    public SQLQueryHolderCallback(Task<SQLQueryHolder<R>> future)
    {
        m_future = future;
    }

    public void AfterComplete(Action<SQLQueryHolder<R>> callback)
    {
        m_callback = callback;
    }

    public bool InvokeIfReady()
    {
        if (m_future != null && m_future.Wait(0))
        {
            if (m_callback != null)
            {
                m_callback(m_future.Result);
            }

            return true;
        }

        return false;
    }
}
