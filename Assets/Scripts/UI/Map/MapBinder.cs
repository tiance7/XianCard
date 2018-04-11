/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace UI.Map
{
	public class MapBinder
	{
		public static void BindAll()
		{
			UIObjectFactory.SetPackageItemExtension(MapCom.URL, typeof(MapCom));
			UIObjectFactory.SetPackageItemExtension(MapBlock1.URL, typeof(MapBlock1));
			UIObjectFactory.SetPackageItemExtension(MapNode.URL, typeof(MapNode));
			UIObjectFactory.SetPackageItemExtension(MapFrame.URL, typeof(MapFrame));
			UIObjectFactory.SetPackageItemExtension(EnemyCom.URL, typeof(EnemyCom));
		}
	}
}