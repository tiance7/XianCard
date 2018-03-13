/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class BattleFrame : GComponent
	{
		public CardDeckCom comCardDeck;
		public UsedCardCom frmUsedCard;
		public Fighter ftSelf;
		public Fighter ftEnemy;
		public GButton btnEndTurn;
		public GTextField txtCost;
		public GComponent frmHand;

		public const string URL = "ui://n5b6g3gas4o70";

		public static BattleFrame CreateInstance()
		{
			return (BattleFrame)UIPackage.CreateObject("Battle","BattleFrame");
		}

		public BattleFrame()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			comCardDeck = (CardDeckCom)this.GetChild("comCardDeck");
			frmUsedCard = (UsedCardCom)this.GetChild("frmUsedCard");
			ftSelf = (Fighter)this.GetChild("ftSelf");
			ftEnemy = (Fighter)this.GetChild("ftEnemy");
			btnEndTurn = (GButton)this.GetChild("btnEndTurn");
			txtCost = (GTextField)this.GetChild("txtCost");
			frmHand = (GComponent)this.GetChild("frmHand");
		}
	}
}