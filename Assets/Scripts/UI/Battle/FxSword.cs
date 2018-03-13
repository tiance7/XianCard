/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class FxSword : GComponent
	{
		public GImage img;

		public const string URL = "ui://n5b6g3gaest9j";

		public static FxSword CreateInstance()
		{
			return (FxSword)UIPackage.CreateObject("Battle","FxSword");
		}

		public FxSword()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			img = (GImage)this.GetChild("img");
		}
	}
}