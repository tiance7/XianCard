/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace UI.Battle
{
	public class BattleBinder
	{
		public static void BindAll()
		{
			UIObjectFactory.SetPackageItemExtension(FxSword.URL, typeof(FxSword));
			UIObjectFactory.SetPackageItemExtension(HpBar.URL, typeof(HpBar));
			UIObjectFactory.SetPackageItemExtension(Fighter.URL, typeof(Fighter));
			UIObjectFactory.SetPackageItemExtension(BattleFrame.URL, typeof(BattleFrame));
			UIObjectFactory.SetPackageItemExtension(CardCom.URL, typeof(CardCom));
			UIObjectFactory.SetPackageItemExtension(CardDeckCom.URL, typeof(CardDeckCom));
			UIObjectFactory.SetPackageItemExtension(UsedCardCom.URL, typeof(UsedCardCom));
			UIObjectFactory.SetPackageItemExtension(BuffRender.URL, typeof(BuffRender));
		}
	}
}