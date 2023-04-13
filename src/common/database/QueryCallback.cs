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

public class QueryCallback : ISqlCallback
{
    public QueryCallback(Task<SQLResult>? result)
    {
        _result = result;
    }

    public QueryCallback WithCallback(Action<SQLResult> callback)
    {
        return WithChainingCallback((queryCallback, result) => callback(result));
    }

    public QueryCallback WithCallback<T>(Action<T, SQLResult> callback, T obj)
    {
        return WithChainingCallback((queryCallback, result) => callback(obj, result));
    }

    public QueryCallback WithChainingCallback(Action<QueryCallback, SQLResult> callback)
    {
        _callbacks.Enqueue(new QueryCallbackData(callback));
        return this;
    }

    public void SetNextQuery(QueryCallback next)
    {
        _result = next._result;
    }

    public bool InvokeIfReady()
    {
        QueryCallbackData callback = _callbacks.Peek();

        while (true)
        {
            if (_result != null && _result.Wait(0))
            {
                Task<SQLResult> f = _result;
                Action<QueryCallback, SQLResult>? cb = callback._result;
                _result = null;

                if (cb != null)
                {
                    cb(this, f.Result);
                }

                _callbacks.Dequeue();
                bool hasNext = _result != null;

                if (_callbacks.Count == 0)
                {
                    return true;
                }

                // abort chain
                if (!hasNext)
                {
                    return true;
                }

                callback = _callbacks.Peek();
            }
            else
                return false;
        }
    }

    Task<SQLResult>? _result;
    Queue<QueryCallbackData> _callbacks = new();
}

struct QueryCallbackData
{
    public QueryCallbackData(Action<QueryCallback, SQLResult> callback)
    {
        _result = callback;
    }

    public void Clear()
    {
        _result = null;
    }

    public Action<QueryCallback, SQLResult>? _result;
}
