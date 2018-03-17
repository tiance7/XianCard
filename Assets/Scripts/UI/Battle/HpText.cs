/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class HpText : GComponent
	{
		public GTextField txtHp;

		public const string URL = "ui://n5b6g3gacuv8m";

		public static HpText CreateInstance()
		{
			return (HpText)UIPackage.CreateObject("Battle","HpText");
		}

		public HpText()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			txtHp = (GTextField)this.GetChild("txtHp");
		}
	}
}