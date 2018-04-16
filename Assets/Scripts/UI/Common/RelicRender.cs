/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Common
{
	public partial class RelicRender : GComponent
	{
		public GLoader imgRelic;

		public const string URL = "ui://kwo75vkrn38l2";

		public static RelicRender CreateInstance()
		{
			return (RelicRender)UIPackage.CreateObject("Common","RelicRender");
		}

		public RelicRender()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			imgRelic = (GLoader)this.GetChild("imgRelic");
		}
	}
}