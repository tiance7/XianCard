using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FGUIInit : MonoBehaviour
{
    private static FGUIInit _inst;

    private void Awake()
    {
        if (_inst == null)
        {
            _inst = this;
        }
        else if (_inst != this)
        {
            Destroy(this.gameObject);   //防止创建出多个对象
            return;
        }
        DontDestroyOnLoad(gameObject);

        UIObjectFactory.SetLoaderExtension(typeof(MyGLoader));

        //AssetManager.LoadAudioClip(GamePath.SOUND + "button_click", audioClip =>
        //{
        //    UIConfig.buttonSound = audioClip;
        //});

        //init control
        SoundTool.inst.AddListener(SoundEvent.MUTE_UPDATE, OnMuteUpdate);
    }

    private void OnMuteUpdate(object obj)
    {
        if (SoundTool.inst.IsMute())
            Stage.inst.DisableSound();
        else
            Stage.inst.EnableSound();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
