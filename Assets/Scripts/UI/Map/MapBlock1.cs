/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Map
{
	public partial class MapBlock1 : GComponent
	{
		public MapNode node1;
		public MapNode node2;
		public MapNode node3;
		public MapNode node4;
		public MapNode node5;
		public MapNode node6;
		public MapNode node7;
		public MapNode node8;
		public MapNode node9;
		public MapNode node10;
		public MapNode node11;

		public const string URL = "ui://9zqi84sykp7y6";

		public static MapBlock1 CreateInstance()
		{
			return (MapBlock1)UIPackage.CreateObject("Map","MapBlock1");
		}

		public MapBlock1()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			node1 = (MapNode)this.GetChild("node1");
			node2 = (MapNode)this.GetChild("node2");
			node3 = (MapNode)this.GetChild("node3");
			node4 = (MapNode)this.GetChild("node4");
			node5 = (MapNode)this.GetChild("node5");
			node6 = (MapNode)this.GetChild("node6");
			node7 = (MapNode)this.GetChild("node7");
			node8 = (MapNode)this.GetChild("node8");
			node9 = (MapNode)this.GetChild("node9");
			node10 = (MapNode)this.GetChild("node10");
			node11 = (MapNode)this.GetChild("node11");
		}
	}
}