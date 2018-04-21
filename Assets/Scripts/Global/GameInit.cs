using System;
using System.Collections;
using System.Collections.Generic;
using UI.ChooseJob;
using UI.Common;
using UI.Map;
using UI.Start;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    private static GameInit _inst;

    // Use this for initialization
    void Awake()
    {
        if (_inst == null)
        {
            _inst = this;
        }
        else if (_inst != null)
        {
            Destroy(this.gameObject);   //∑¿÷π÷ÿ∏¥¥¥Ω®
            return;
        }

        BindUI();
    }

    private void BindUI()
    {
        CommonBinder.BindAll();
        StartBinder.BindAll();
        ChooseJobBinder.BindAll();
        MapBinder.BindAll();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
