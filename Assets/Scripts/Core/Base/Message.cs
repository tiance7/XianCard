using System;
using System.Collections.Generic;

public static class Message
{
    private static Dictionary<MsgType, Action<object>> _list = new Dictionary<MsgType, Action<object>>();

    public static void Send(MsgType type, object param = null)
    {
        if (!_list.ContainsKey(type))
            return;
        var callback = _list[type];
        callback(param);
    }

    public static void AddListener(MsgType type, Action<object> listener)
    {
        if (_list.ContainsKey(type))
        {
            _list[type] += listener;
        }
        else
        {
            _list[type] = listener;
        }
    }

    public static void RemoveListener(MsgType type, Action<object> listener)
    {
        if (!_list.ContainsKey(type))
            return;
        _list[type] -= listener;

        if (_list[type] == null)
            _list.Remove(type);
    }
}
