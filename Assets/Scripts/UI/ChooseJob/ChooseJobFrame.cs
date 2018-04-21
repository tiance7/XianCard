/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.ChooseJob
{
	public partial class ChooseJobFrame : GComponent
	{
		public Controller cJob;
		public GButton btnStart;

		public const string URL = "ui://t8jxoy6mh8c30";

		public static ChooseJobFrame CreateInstance()
		{
			return (ChooseJobFrame)UIPackage.CreateObject("ChooseJob","ChooseJobFrame");
		}

		public ChooseJobFrame()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			cJob = this.GetController("cJob");
			btnStart = (GButton)this.GetChild("btnStart");
		}
	}
}