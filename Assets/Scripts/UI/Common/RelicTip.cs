/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Common
{
	public partial class RelicTip : GComponent
	{
		public GTextField txtName;
		public GTextField txtDesc;

		public const string URL = "ui://kwo75vkrn38l3";

		public static RelicTip CreateInstance()
		{
			return (RelicTip)UIPackage.CreateObject("Common","RelicTip");
		}

		public RelicTip()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			txtName = (GTextField)this.GetChild("txtName");
			txtDesc = (GTextField)this.GetChild("txtDesc");
		}
	}
}