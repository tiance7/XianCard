/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class HpBar : GProgressBar
	{
		public Controller ctrlArmor;
		public GTextField txtArmor;
		public Transition tGetArmor;

		public const string URL = "ui://n5b6g3gas0y68";

		public static HpBar CreateInstance()
		{
			return (HpBar)UIPackage.CreateObject("Battle","HpBar");
		}

		public HpBar()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			ctrlArmor = this.GetController("ctrlArmor");
			txtArmor = (GTextField)this.GetChild("txtArmor");
			tGetArmor = this.GetTransition("tGetArmor");
		}
	}
}