/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class CardCom : GComponent
	{
		public Controller ctrlState;
		public GTextField txtCost;
		public GTextField txtType;
		public GTextField txtDesc;
		public GTextField txtName;
		public Transition tFlyToUsed;

		public const string URL = "ui://n5b6g3gas4o71";

		public static CardCom CreateInstance()
		{
			return (CardCom)UIPackage.CreateObject("Battle","CardCom");
		}

		public CardCom()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			ctrlState = this.GetController("ctrlState");
			txtCost = (GTextField)this.GetChild("txtCost");
			txtType = (GTextField)this.GetChild("txtType");
			txtDesc = (GTextField)this.GetChild("txtDesc");
			txtName = (GTextField)this.GetChild("txtName");
			tFlyToUsed = this.GetTransition("tFlyToUsed");
		}
	}
}