/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Common
{
	public partial class TopFrame : GComponent
	{
		public GTextField txtHp;
		public GTextField txtGold;
		public GList lstRelic;

		public const string URL = "ui://kwo75vkrf2xp0";

		public static TopFrame CreateInstance()
		{
			return (TopFrame)UIPackage.CreateObject("Common","TopFrame");
		}

		public TopFrame()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			txtHp = (GTextField)this.GetChild("txtHp");
			txtGold = (GTextField)this.GetChild("txtGold");
			lstRelic = (GList)this.GetChild("lstRelic");
		}
	}
}