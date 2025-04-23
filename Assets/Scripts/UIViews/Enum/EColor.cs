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

    public static class PatienceColor
    {
        public static Color Green
        {
            get
            {
                ColorUtility.TryParseHtmlString("#38E000", out Color color);
                return color;
            }
        }

        public static Color Yellow
        {
            get
            {
                ColorUtility.TryParseHtmlString("#FFA200", out Color color);
                return color;
            }
        }

        public static Color Red
        {
            get
            {
                ColorUtility.TryParseHtmlString("#FF2F2F", out Color color);
                return color;
            }
        }
    }
}
