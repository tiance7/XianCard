using System;
using System.Collections.Generic;

public class ModelBase : IDisposable
{

    private Dictionary<Enum, Action<object>> _list = new Dictionary<Enum, Action<object>>();

    public ModelBase()
    {

    }

    public virtual void Dispose()
    {
        ClearAllEventListener();
    }

    /// <summary>
    /// 发送事件消息
    /// </summary>
    /// <param name="modelEvent"></param>
    /// <param name="param"></param>
    public void SendEvent(Enum modelEvent, object param = null)
    {
        if (!_list.ContainsKey(modelEvent))
            return;
        Action<object> callback = _list[modelEvent];
        callback(param);
    }

    /// <summary>
    /// 添加事件
    /// </summary>
    /// <param name="modelEvent"></param>
    /// <param name="listener"></param>
    public void AddListener(Enum modelEvent, Action<object> listener)
    {
        if (_list.ContainsKey(modelEvent))
        {
            _list[modelEvent] -= listener;
            _list[modelEvent] += listener;
        }
        else
        {
            _list[modelEvent] = listener;
        }
    }

    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="modelEvent"></param>
    /// <param name="listener"></param>
    public void RemoveListener(Enum modelEvent, Action<object> listener)
    {
        if (!_list.ContainsKey(modelEvent))
            return;
        _list[modelEvent] -= listener;

        if (_list[modelEvent] == null)
            _list.Remove(modelEvent);
    }

    /// <summary>
    /// 清空事件
    /// </summary>
    public void ClearAllEventListener()
    {
        _list.Clear();
    }

}