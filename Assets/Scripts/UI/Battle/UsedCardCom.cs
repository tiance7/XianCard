/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class UsedCardCom : GComponent
	{
		public GTextField txtUsedNum;

		public const string URL = "ui://n5b6g3gas4o74";

		public static UsedCardCom CreateInstance()
		{
			return (UsedCardCom)UIPackage.CreateObject("Battle","UsedCardCom");
		}

		public UsedCardCom()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			txtUsedNum = (GTextField)this.GetChild("txtUsedNum");
		}
	}
}