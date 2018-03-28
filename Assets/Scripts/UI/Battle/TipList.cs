/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI.Battle
{
	public partial class TipList : GComponent
	{
		public GList lstTip;

		public const string URL = "ui://n5b6g3ga96fmv";

		public static TipList CreateInstance()
		{
			return (TipList)UIPackage.CreateObject("Battle","TipList");
		}

		public TipList()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			lstTip = (GList)this.GetChild("lstTip");
		}
	}
}