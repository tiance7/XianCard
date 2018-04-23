/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Map
{
	public partial class MapBlock2 : GComponent
	{
		public MapNodeCom node1;
		public MapNodeCom node2;
		public MapNodeCom node3;
		public MapNodeCom node4;
		public MapNodeCom node5;
		public MapNodeCom node7;
		public MapNodeCom node8;

		public const string URL = "ui://9zqi84syy8afa";

		public static MapBlock2 CreateInstance()
		{
			return (MapBlock2)UIPackage.CreateObject("Map","MapBlock2");
		}

		public MapBlock2()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			node1 = (MapNodeCom)this.GetChild("node1");
			node2 = (MapNodeCom)this.GetChild("node2");
			node3 = (MapNodeCom)this.GetChild("node3");
			node4 = (MapNodeCom)this.GetChild("node4");
			node5 = (MapNodeCom)this.GetChild("node5");
			node7 = (MapNodeCom)this.GetChild("node7");
			node8 = (MapNodeCom)this.GetChild("node8");
		}
	}
}