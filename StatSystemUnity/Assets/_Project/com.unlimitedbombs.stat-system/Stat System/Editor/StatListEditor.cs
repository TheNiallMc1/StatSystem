using UnityEditor;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    [CustomEditor(typeof(StatList))]
    public class StatListEditor : UnityEditor.Editor
    {
        private StatList statList;
        
        private int numberOfStats;

        private Rect lastRemoveRect;
        
        public void OnEnable()
        {
            statList = (StatList) target;
            StatSystemStyle.originalColour = GUI.backgroundColor;
        }

        #region GUI

        public override void OnInspectorGUI()
        {            
            GenerateTextFields();
            
            DrawAddButton();

            GUILayout.Space(35f);
        }

        private void DrawAddButton()
        {
            if (statList.statNames.Count > 0)
            {
                GUI.backgroundColor = StatSystemStyle.confirmColour;
                
                Rect rect = new Rect(lastRemoveRect.x, lastRemoveRect.y + 25, lastRemoveRect.width, lastRemoveRect.height);
                
                if (GUI.Button(rect, " +"))
                {
                    statList.statNames.Add("");
                }
            }
            else
            {
                GUI.backgroundColor = StatSystemStyle.originalColour;
                
                if (GUILayout.Button("Create Stats"))
                {
                    statList.statNames.Add("");
                }
            }
            
            GUI.backgroundColor = StatSystemStyle.originalColour;
        }

        private void GenerateTextFields()
        {
            for (int i = 0; i < statList.statNames.Count; i++)
            {
                GUILayout.BeginHorizontal();
                
                statList.statNames[i] = EditorGUILayout.TextField(statList.statNames[i]);

                DrawRemoveButton(i);
                GUILayout.EndHorizontal();
            }
        }

        private void DrawRemoveButton(int removeIndex)
        {
            GUI.backgroundColor = StatSystemStyle.removeColour;
            
            if (GUILayout.Button("X", GUILayout.Width(30f)))
            {
                RemoveStat(removeIndex);
            }

            lastRemoveRect = GUILayoutUtility.GetLastRect();
            
            GUI.backgroundColor = StatSystemStyle.originalColour;
        }

        #endregion

        #region Logic Methods

        private void RemoveStat(int removeIndex)
        {
            statList.statNames.RemoveAt(removeIndex);
        }

        #endregion
    }
}
