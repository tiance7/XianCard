using FairyGUI;
using UnityEngine.SceneManagement;

namespace UI.Map
{
    public partial class MapFrame
    {
        public override void ConstructFromResource()
        {
            base.ConstructFromResource();
            InitControl();
        }

        private void InitControl()
        {
            monster1.onClick.Add(OnMonster1Click);
        }

        private void OnMonster1Click(EventContext context)
        {
            SceneManager.LoadScene(SceneName.BATTLE);
        }
    }
}