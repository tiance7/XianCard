/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class CardDeckCom : GComponent
	{
		public GTextField txtDeckNum;

		public const string URL = "ui://n5b6g3gas4o73";

		public static CardDeckCom CreateInstance()
		{
			return (CardDeckCom)UIPackage.CreateObject("Battle","CardDeckCom");
		}

		public CardDeckCom()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			txtDeckNum = (GTextField)this.GetChild("txtDeckNum");
		}
	}
}