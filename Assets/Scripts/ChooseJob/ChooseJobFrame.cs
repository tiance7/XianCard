using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.ChooseJob
{
    public partial class ChooseJobFrame
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