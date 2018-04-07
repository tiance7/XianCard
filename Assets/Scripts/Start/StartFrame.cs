using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Start
{
    public partial class StartFrame
    {
        public override void ConstructFromResource()
        {
            base.ConstructFromResource();
            InitControl();
        }

        private void InitControl()
        {
            btnStart.onClick.Add(OnStartClick);
        }

        private void OnStartClick(EventContext context)
        {
            SceneManager.LoadSceneAsync(SceneName.MAP);
        }
    }
}