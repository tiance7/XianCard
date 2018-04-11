/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Map
{
	public partial class MapNode : GButton
	{
		public Controller cType;

		public const string URL = "ui://9zqi84sykp7y7";

		public static MapNode CreateInstance()
		{
			return (MapNode)UIPackage.CreateObject("Map","MapNode");
		}

		public MapNode()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			cType = this.GetController("cType");
		}
	}
}