/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Map
{
	public partial class MapNodeCom : GButton
	{
		public Controller cType;
		public Controller cPass;
		public Transition tCanEnter;

		public const string URL = "ui://9zqi84sykp7y7";

		public static MapNodeCom CreateInstance()
		{
			return (MapNodeCom)UIPackage.CreateObject("Map","MapNodeCom");
		}

		public MapNodeCom()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			cType = this.GetController("cType");
			cPass = this.GetController("cPass");
			tCanEnter = this.GetTransition("tCanEnter");
		}
	}
}