/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace UI.Map
{
	public class MapBinder
	{
		public static void BindAll()
		{
			UIObjectFactory.SetPackageItemExtension(MapCom.URL, typeof(MapCom));
			UIObjectFactory.SetPackageItemExtension(MapFrame.URL, typeof(MapFrame));
			UIObjectFactory.SetPackageItemExtension(MonsterCom.URL, typeof(MonsterCom));
		}
	}
}