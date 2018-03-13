using System;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager
{
    private static Dictionary<uint, TimerStruct> _dicTimer;

    static TimeManager()    //泛型
    {
        _dicTimer = new Dictionary<uint, TimerStruct>();
    }

    // 延迟_timeDelay后回调，单位：秒
    public static uint Add(float delaySecond, Action callBack, bool isLoop = false)
    {
        uint timerId = 1;
        while (_dicTimer.ContainsKey(timerId))
        {
            timerId++;
        }
        _dicTimer.Add(timerId, new TimerStruct(timerId, delaySecond, callBack, isLoop));
        return timerId;
    }

    /// <summary>
    /// 返回int型，可以根据剩余的次数立即执行未执行的回调函数
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="removeAll"></param>false时仅取消最后注册的（和延迟时间无关）
    /// <returns></returns>
    public static int Remove(Action callback, bool removeAll = true)
    {
        List<uint> lstRemoveTimerId = new List<uint>();
        foreach (KeyValuePair<uint, TimerStruct> kv in _dicTimer)
        {
            if (kv.Value.callBack.Equals(callback))
            {
                lstRemoveTimerId.Insert(0, kv.Key);
            }
        }
        foreach (uint timerId in lstRemoveTimerId)
        {
            _dicTimer.Remove(timerId);
            if (!removeAll)
                break;
        }
        return lstRemoveTimerId.Count;
    }

    /// <summary>
    /// 移除计时器
    /// </summary>
    /// <param name="timerId"></param>
    /// <returns></returns>
    public static void Remove(uint timerId)
    {
        if (_dicTimer.ContainsKey(timerId))
            _dicTimer.Remove(timerId);
    }

    public static void ClearInvoke()
    {
        _dicTimer.Clear();
    }

    public static void Update(float _deltaTime)
    {
        if (_deltaTime == 0)
            return;

        List<uint> lstRemoveTimerId = new List<uint>();
        List<Action> lstCallBack = new List<Action>();
        foreach (KeyValuePair<uint, TimerStruct> kv in _dicTimer)
        {
            kv.Value.curTime -= _deltaTime;
            if (kv.Value.curTime > 0)
                continue;
            lstCallBack.Add(kv.Value.callBack);
            if (kv.Value.isLoop)
            {
                kv.Value.curTime = kv.Value.timerDelay;
                continue;
            }
            lstRemoveTimerId.Add(kv.Key);
        }
        foreach (uint timerId in lstRemoveTimerId)
        {
            _dicTimer.Remove(timerId);
        }
        foreach (Action callBack in lstCallBack)
        {
            try
            {
                callBack();
            }
            catch
            {
                Remove(callBack);
                Debug.Log("TimeManager callbacks do not exist!");
            }
        }
    }

}

public sealed class TimerStruct
{
    public uint timerId;
    public float timerDelay;
    public Action callBack;
    public bool isLoop;

    public float curTime;

    public TimerStruct(uint _timerId, float _timerDelay, Action _callBack, bool _isLoop)
    {
        timerId = _timerId;
        timerDelay = _timerDelay;
        curTime = _timerDelay;
        callBack = _callBack;
        isLoop = _isLoop;
    }

}