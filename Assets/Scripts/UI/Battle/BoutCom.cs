/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class BoutCom : GComponent
	{
		public GTextField txxBout;
		public Transition tBout;

		public const string URL = "ui://n5b6g3gamq4yk";

		public static BoutCom CreateInstance()
		{
			return (BoutCom)UIPackage.CreateObject("Battle","BoutCom");
		}

		public BoutCom()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			txxBout = (GTextField)this.GetChild("txxBout");
			tBout = this.GetTransition("tBout");
		}
	}
}