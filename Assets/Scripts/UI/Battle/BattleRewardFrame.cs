/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class BattleRewardFrame : GComponent
	{
		public Controller ctrlReward;
		public GButton btnJumpCard;
		public GList lstReward;
		public GButton btnJumpSelectCard;
		public CardCom card1;
		public CardCom card2;
		public CardCom card3;
		public Transition tSelect3Card;

		public const string URL = "ui://n5b6g3gaxtfzn";

		public static BattleRewardFrame CreateInstance()
		{
			return (BattleRewardFrame)UIPackage.CreateObject("Battle","BattleRewardFrame");
		}

		public BattleRewardFrame()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			ctrlReward = this.GetController("ctrlReward");
			btnJumpCard = (GButton)this.GetChild("btnJumpCard");
			lstReward = (GList)this.GetChild("lstReward");
			btnJumpSelectCard = (GButton)this.GetChild("btnJumpSelectCard");
			card1 = (CardCom)this.GetChild("card1");
			card2 = (CardCom)this.GetChild("card2");
			card3 = (CardCom)this.GetChild("card3");
			tSelect3Card = this.GetTransition("tSelect3Card");
		}
	}
}