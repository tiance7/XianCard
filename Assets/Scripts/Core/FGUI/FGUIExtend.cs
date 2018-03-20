using UnityEngine;

namespace FairyGUI
{
    public static class FGUIExtend
    {
        private const float COLOR_MAX = 255.0F;

        public static void SetColor(this GImage image, int color)
        {
            float r = (color >> 16) / COLOR_MAX;
            float g = ((color & 0xFF00) >> 8) / COLOR_MAX;
            float b = (color & 0xFF) / COLOR_MAX;
            image.color = new Color(r, g, b);
        }
    }
}