/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Map
{
	public partial class MapFrame : GComponent
	{
		public GButton monster1;
		public GButton monster2;
		public GButton monster3;

		public const string URL = "ui://9zqi84syuu6x0";

		public static MapFrame CreateInstance()
		{
			return (MapFrame)UIPackage.CreateObject("Map","MapFrame");
		}

		public MapFrame()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			monster1 = (GButton)this.GetChild("monster1");
			monster2 = (GButton)this.GetChild("monster2");
			monster3 = (GButton)this.GetChild("monster3");
		}
	}
}