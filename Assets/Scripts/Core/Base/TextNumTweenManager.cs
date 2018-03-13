/***************************
 * 作者:      魏强
 * 创建时间:  10/25/2017
 * 最后修改时间:
 * 描述:
 * 
**/
using System;
using FairyGUI;

public class TextNumTweenManager : IDisposable
{
    private GTextField _textField;
    private int _frequency;            // 变化次数
    private int _startNum;
    private int _endNum;
    private int _currUpdateTime;       // 已刷新的次数

    public TextNumTweenManager()
	{
		
	}

    public void Dispose()
    {
        StopTime();
    }

    // 设置Text文本数字逐步变化  GTextField：UI文本控件  startNum：开始数字  endNum：结束数字  second：Tween的秒数  frequency：变化次数
    public void SetNumberTween(GTextField textField, int startNum, int endNum, float second, int frequency = 24)
    {
        if (textField == null)
            return;

        StopTime();                         // 如果之前有技数先停止计数
        _textField = textField;
        _frequency = frequency;
        _startNum = startNum;
        _endNum = endNum;

        float timeDelay = second / frequency;

        textField.text = _startNum.ToString();

        _currUpdateTime = 1;
        TimeManager.Add(timeDelay, OnTextNumberTween, true);
    }

    void OnTextNumberTween()
    {
        if (_textField == null)
            return;

        if(_currUpdateTime == _frequency)
        {
            TimeManager.Remove(OnTextNumberTween);
            _textField.text = _endNum.ToString();
            _textField = null;
            _currUpdateTime = 0;
            return;
        }

        float currNum = (float)(_endNum - _startNum) / _frequency * _currUpdateTime + _startNum;
        int showNum = (int)currNum;

        _textField.text = showNum.ToString();
        ++_currUpdateTime;
    }

    void StopTime()
    {
        if (_textField == null)
            return;
        TimeManager.Remove(OnTextNumberTween);
        _textField = null;
    }
}
