using System.Collections;
namespace UnityEngine
{
    public static class UnityExtension
    {
        public static Color ParseHexColorCode(this string colorCode)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(colorCode, out color))
            {
                return color;
            }
            else
            {
                Debug.LogError("色の出力ができません");
                return Color.magenta;
            }
        }
    }
}