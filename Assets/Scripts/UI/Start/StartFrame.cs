/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Start
{
	public partial class StartFrame : GComponent
	{
		public GButton btnStart;

		public const string URL = "ui://99yi2rtpu8jz0";

		public static StartFrame CreateInstance()
		{
			return (StartFrame)UIPackage.CreateObject("Start","StartFrame");
		}

		public StartFrame()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			btnStart = (GButton)this.GetChild("btnStart");
		}
	}
}