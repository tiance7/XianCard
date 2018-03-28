/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class TipRender : GComponent
	{
		public GTextField txtName;
		public GLoader imgIcon;
		public GTextField txtDesc;

		public const string URL = "ui://n5b6g3gah5ixu";

		public static TipRender CreateInstance()
		{
			return (TipRender)UIPackage.CreateObject("Battle","TipRender");
		}

		public TipRender()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			txtName = (GTextField)this.GetChild("txtName");
			imgIcon = (GLoader)this.GetChild("imgIcon");
			txtDesc = (GTextField)this.GetChild("txtDesc");
		}
	}
}