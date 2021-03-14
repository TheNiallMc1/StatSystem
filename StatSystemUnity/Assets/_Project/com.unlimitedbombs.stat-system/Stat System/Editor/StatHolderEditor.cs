using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    [CustomEditor(typeof(StatHolder))]
    public class StatHolderEditor : Editor
    {
        private StatHolder statHolder;

        private List<string> statNames;
        private List<int> baseValues;
        
        public void OnEnable()
        {
            statHolder = (StatHolder) target;
 
        }
        
        public override void OnInspectorGUI()
        {
            if (statHolder.statList == null)
            {
                Debug.LogError("No stat list assigned to stat holder", this);
            }
            
            base.OnInspectorGUI();
        }
        
        private void GenerateInputFields()
        {
            EditorGUILayout.BeginHorizontal();
            
            
            
            EditorGUILayout.EndHorizontal();
        }

        private void GenerateClearButton()
        {
            if (GUILayout.Button("Clear Stats"))
            {
                bool confirmation = EditorUtility.DisplayDialog
                    ("Clear stats dictionary?",
                    "Are you sure you want to completely clear all the current stats on this StatHolder?",
                    "Clear",
                    "Don't Clear");

                if (confirmation)
                {
                    statHolder.statValues.Clear();
                    statHolder.statNames.Clear();
                }
            }
        }
    }
}
