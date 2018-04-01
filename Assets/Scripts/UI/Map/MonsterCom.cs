/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Map
{
	public partial class MonsterCom : GButton
	{
		public GImage img;

		public const string URL = "ui://9zqi84syuu6x1";

		public static MonsterCom CreateInstance()
		{
			return (MonsterCom)UIPackage.CreateObject("Map","MonsterCom");
		}

		public MonsterCom()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			img = (GImage)this.GetChild("img");
		}
	}
}