/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class BlockText : GComponent
	{
		public GTextField txtBlock;

		public const string URL = "ui://n5b6g3gacuv8l";

		public static BlockText CreateInstance()
		{
			return (BlockText)UIPackage.CreateObject("Battle","BlockText");
		}

		public BlockText()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			txtBlock = (GTextField)this.GetChild("txtBlock");
		}
	}
}