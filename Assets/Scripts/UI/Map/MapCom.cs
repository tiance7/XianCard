/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Map
{
	public partial class MapCom : GComponent
	{
		public GLoader imgBoss;

		public const string URL = "ui://9zqi84syfrdb4";

		public static MapCom CreateInstance()
		{
			return (MapCom)UIPackage.CreateObject("Map","MapCom");
		}

		public MapCom()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			imgBoss = (GLoader)this.GetChild("imgBoss");
		}
	}
}