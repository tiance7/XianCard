/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Map
{
	public partial class EnemyCom : GButton
	{
		public GImage img;

		public const string URL = "ui://9zqi84syuu6x1";

		public static EnemyCom CreateInstance()
		{
			return (EnemyCom)UIPackage.CreateObject("Map","EnemyCom");
		}

		public EnemyCom()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			img = (GImage)this.GetChild("img");
		}
	}
}