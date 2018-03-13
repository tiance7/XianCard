using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>  
/// 泛型非单例池
/// </summary>  
public class ObjPool<T> where T : class
{
    private Action<T> _actReset;                            //重置对象的委托  
    private Func<T> _funNew;                                //创建新对象的委托  
    private Stack<T> _stack;                              //存放对象的池子

    /// <summary>
    /// <para> funNew() 创建对象的方法 需要return t;</para>
    /// actReset(t) 可选；重置旧对象的方法
    /// </summary>
    /// <param name="funNew"></param>
    /// <param name="actReset"></param>
    public ObjPool(Func<T> funNew, Action<T> actReset = null)
    {
        this._funNew = funNew;
        this._actReset = actReset;
        _stack = new Stack<T>();
    }

    //从池子中获取对象的方法，思路是若池子的数量为0，则调用创建新对象委托创建一个对象返回  
    //否则从池子中拿出一个对象并返回  
    public T Get()
    {
        if (_stack.Count == 0)
        {
            T t = _funNew();
            return t;
        }
        else
        {
            T t = _stack.Pop();
            if (_actReset != null)
                _actReset(t);
            return t;
        }
    }

    /// <summary>
    /// 获取全部对象
    /// </summary>
    /// <returns></returns>
    public Stack<T> GetAll()
    {
        return _stack;
    }

    /// <summary>
    /// 此方法用于将销毁的对象存入池子  
    /// </summary>
    /// <param name="t"></param>
    public void Store(T t)
    {
        _stack.Push(t);
    }

    /// <summary>
    /// 清空池子  
    /// </summary>
    public void Clear()
    {
        _stack.Clear();
    }

}