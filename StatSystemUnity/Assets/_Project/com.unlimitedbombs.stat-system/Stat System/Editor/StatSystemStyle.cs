using UnityEditor;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    public class StatSystemStyle : Editor
    {
        public static Color originalColourBase;
        public static Color originalColour;
        // public static readonly Color confirmColour = new Color(0.39f, 1f, 0.46f);
        // public static readonly Color removeColour = new Color(1f, 0.53f, 0.43f);
        public static readonly Color confirmColour = new Color(0.44f, 0.76f, 0.46f);
        public static readonly Color removeColour = new Color(0.79f, 0.36f, 0.34f);
        public static readonly Color alternateModBg = new Color(0.26f, 0.25f, 0.25f);

        public static GUIStyle HeaderStyle()
        {
            GUIStyle header = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 15
            };
            
            return header;
        }
    
        public static void HorizontalSeperator()
        {
            GUILayout.Label("", GUI.skin.horizontalSlider);
            GUILayout.Space(15f);
        }
    }
}