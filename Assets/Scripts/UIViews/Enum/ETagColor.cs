using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EColor
{
    public static class TagColor
    {
        public static Color Efficacy
        {
            get
            {
                ColorUtility.TryParseHtmlString("#F7A591", out Color color);
                return color;
            }
        }

        public static Color SideEffect
        {
            get
            {
                ColorUtility.TryParseHtmlString("#ADA7EC", out Color color);
                return color;
            }
        }
    }
}
