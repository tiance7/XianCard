using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTool : ModelBase
{
    #region Inst
    private static readonly SoundTool _inst = new SoundTool();
    static SoundTool() { }
    public static SoundTool inst { get { return _inst; } }
    #endregion

    private GameObject _soundGo;
    private AudioSource _audioSource;

    private SoundTool()
    {
        _soundGo = new GameObject("Sound");
        _audioSource = _soundGo.AddComponent<AudioSource>();
        GameObject.DontDestroyOnLoad(_soundGo);

        int isMute = PlayerPrefs.GetInt(PrefsDef.MUTE, 0);
        if (isMute == 1)    //如果是静音
            SwitchMute();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="path"></param>
    public void PlaySoundEffect(string path)
    {
        var audioClip = AssetManager.Load<AudioClip>(path);
        if (audioClip != null)
            _audioSource.PlayOneShot(audioClip);
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioClip">音效资源</param>
    public void PlaySoundEffect(AudioClip audioClip)
    {
        if (audioClip != null)
            _audioSource.PlayOneShot(audioClip);
    }


    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="path"></param>
    public void PlayBGM(string path, float volume = 1.0f)
    {
        _audioSource.Stop();
        var audioClip = AssetManager.Load<AudioClip>(path);
        if (audioClip != null)
        {
            _audioSource.clip = audioClip;
            _audioSource.loop = true;
            _audioSource.volume = volume;
            _audioSource.Play();
        }
    }

    /// <summary>
    /// 停止播放背景音乐
    /// </summary>
    public void StopBGM()
    {
        _audioSource.Stop();
    }

    //切换静音/正常状态
    public void SwitchMute()
    {
        _audioSource.mute = !_audioSource.mute;
        PlayerPrefs.SetInt(PrefsDef.MUTE, _audioSource.mute ? 1 : 0);
        SendEvent(SoundEvent.MUTE_UPDATE);
    }

    /// <summary>
    /// 是否是静音
    /// </summary>
    /// <returns></returns>
    public bool IsMute()
    {
        return _audioSource.mute;
    }

}

public enum SoundEvent
{
    MUTE_UPDATE //静音改变
}