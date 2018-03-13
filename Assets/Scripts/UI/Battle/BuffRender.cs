/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class BuffRender : GComponent
	{
		public GLoader img;
		public GTextField txtValue;

		public const string URL = "ui://n5b6g3gasdmte";

		public static BuffRender CreateInstance()
		{
			return (BuffRender)UIPackage.CreateObject("Battle","BuffRender");
		}

		public BuffRender()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			img = (GLoader)this.GetChild("img");
			txtValue = (GTextField)this.GetChild("txtValue");
		}
	}
}