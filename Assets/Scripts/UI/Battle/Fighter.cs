/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class Fighter : GComponent
	{
		public Controller ctrlAction;
		public GLoader imgAvatar;
		public HpBar pgsHp;
		public GTextField txtAttack;
		public GList lstBuff;

		public const string URL = "ui://n5b6g3gas0y69";

		public static Fighter CreateInstance()
		{
			return (Fighter)UIPackage.CreateObject("Battle","Fighter");
		}

		public Fighter()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			ctrlAction = this.GetController("ctrlAction");
			imgAvatar = (GLoader)this.GetChild("imgAvatar");
			pgsHp = (HpBar)this.GetChild("pgsHp");
			txtAttack = (GTextField)this.GetChild("txtAttack");
			lstBuff = (GList)this.GetChild("lstBuff");
		}
	}
}