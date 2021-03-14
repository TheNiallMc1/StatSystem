using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    [CustomEditor(typeof(Equipment))]
    [Serializable]
    public class EquipmentEditor : Editor
    {
        private string[] statNames;
        
        private Equipment thisEquipment;
        
        private Rect lastRemoveRect;

        private bool foldoutToggle;

        public void OnEnable()
        {
            thisEquipment = (Equipment) target;
            statNames = thisEquipment.statList.statNames.ToArray();
            StatSystemStyle.originalColour = GUI.backgroundColor;
            StatSystemStyle.originalColourBase = StatSystemStyle.originalColour;
            //Style.originalColour = Style.customBgColour;
        }

        public override void OnInspectorGUI()
        {
            if (thisEquipment.statList == null)
            {
                Debug.LogError("No stat list assigned to equipment", this);
            }
            
            base.OnInspectorGUI();
            
            StatSystemStyle.HorizontalSeperator();
            
            GUILayout.Label("Modifiers", StatSystemStyle.HeaderStyle());
            
            StatSystemStyle.HorizontalSeperator();

            GenerateModifierFields();
            
            DrawAddButton();
            
            GUILayout.Space(15f);

            DrawClearModButton();
            
            GUILayout.Space(35f); 
        }

        private void OnValidate()
        {
            Repaint();
        }

        private void GenerateModifierFields()
        {
            if (thisEquipment.statModifiers.Count == 0) return;

            for (int i = 0; i < thisEquipment.statModifiers.Count; i++)
            { 
                if (i % 2 == 0)
                {
                    GUI.backgroundColor = StatSystemStyle.alternateModBg;
                }
                
                GUILayout.BeginVertical(EditorStyles.helpBox);
                GUI.backgroundColor = StatSystemStyle.originalColour;

                DrawRemoveButton(i);
                
                GUILayout.BeginHorizontal();
                
                DrawStatDropdown(i);
                DrawModTypeDropdown(i);
                
                GUILayout.EndHorizontal();
                
                GUILayout.BeginHorizontal();
                
                DrawOrderField(i);
                DrawValueField(i);
                
                GUILayout.EndHorizontal();
                
                GUILayout.EndVertical();
                
                GUILayout.Space(10f); 
            }
        }

        private void DrawStatDropdown(int modIndex)
        {
            if (thisEquipment.statModifiers.Count < modIndex + 1 || thisEquipment.statNameIndices.Count < modIndex + 1) return;
            GUILayout.BeginVertical((GUILayoutOption[]) default);
            
            GUIContent guiContent = new GUIContent("Stat to Modify", 
                "The stat this modifier will affect. The options are determined by the assigned Stat List.");
            
            EditorGUILayout.LabelField(guiContent, GUILayout.MaxWidth(100f) );

            StatModifier mod = thisEquipment.statModifiers[modIndex];

            thisEquipment.statNameIndices[modIndex] = EditorGUILayout.Popup(thisEquipment.statNameIndices[modIndex], statNames, GUILayout.MaxWidth(150f));
            int index = thisEquipment.statNameIndices[modIndex];
            mod.statToModify = statNames[index];
            
            UpdateModifierList();
            
            GUILayout.EndVertical();
        }

        private void DrawModTypeDropdown(int modIndex)
        {
            if (thisEquipment.statModifiers.Count < modIndex + 1) return;
            
            GUILayout.BeginVertical();
            
            StatModifier mod = thisEquipment.statModifiers[modIndex];
            
            GUIContent guiContent = new GUIContent("Mod Type", 
                "The type of the modifier. Percentage Add additively applies percentages. Percentage Mult multiplicatively applies percentages." +
                "Flat will add a flat integer bonus to the stat.");
            
            EditorGUILayout.LabelField(guiContent, GUILayout.MaxWidth(100f) );
            
            EditorGUI.BeginChangeCheck();

            eModifierType lastModType = mod.modType;
            mod.modType = (eModifierType) EditorGUILayout.EnumPopup(mod.modType, GUILayout.MaxWidth(150f));

            if (EditorGUI.EndChangeCheck())
            {
                if (mod.order == (int) lastModType || mod.order == 0)
                {
                    mod.order = (int) mod.modType;
                }
            }
            
            
            UpdateModifierList();
            
            GUILayout.EndVertical();
        }

        private void DrawValueField(int modIndex)
        {
            if (thisEquipment.statModifiers.Count < modIndex + 1) return;
         
            GUILayout.BeginVertical();
            
            StatModifier mod = thisEquipment.statModifiers[modIndex];

            GUIContent guiContent = new GUIContent("Value", 
                "The value of the modifier. This is an int for flat modifiers and a float for percentages.");
            
            EditorGUILayout.LabelField(guiContent, GUILayout.MaxWidth(50f) );
            
            if (mod.modType != eModifierType.Flat)
            {
                mod.value = EditorGUILayout.FloatField( mod.value, GUILayout.MaxWidth(50f) );
                UpdateModifierList();
            }
            else
            {
                mod.value = EditorGUILayout.IntField( (int) mod.value, GUILayout.MaxWidth(50f) );
                UpdateModifierList();
            }
            
            GUILayout.EndVertical();
        }    

        private void DrawOrderField(int modIndex)
        {
            if (thisEquipment.statModifiers.Count < modIndex + 1) return;
            
            GUILayout.BeginVertical();
            
            StatModifier mod = thisEquipment.statModifiers[modIndex];

            GUIContent guiContent = new GUIContent("Order", 
                "The order the modifiers are applied in. Higher values are applied first. " +
                "Set this to zero to use the default order of each mod type.");
            
            EditorGUILayout.LabelField(guiContent, GUILayout.MaxWidth(40f));
            
            EditorGUI.BeginChangeCheck();
            mod.order = EditorGUILayout.IntField( mod.order, GUILayout.MaxWidth(50f) );
            if (EditorGUI.EndChangeCheck() && mod.order == 0)
            {
                mod.order = (int) mod.modType;
            }
            
            GUILayout.EndVertical();
        }
        
        private void DrawAddButton()
        {
            GUI.backgroundColor = StatSystemStyle.confirmColour;
            
            if (GUILayout.Button("New Modifier"))
            {
                Undo.RecordObject(thisEquipment, "Add Modifier");
                thisEquipment.statModifiers.Add(new StatModifier(eModifierType.Flat, 0));
                thisEquipment.statNameIndices.Add(0);
                UpdateModifierList();
            }
            
            GUI.backgroundColor = StatSystemStyle.originalColour;
            
            // if (thisEquipment.statModifiers.Count > 0)
            // {
            //     GUI.backgroundColor = Style.confirmColour;
            //     
            //     Rect rect = new Rect(lastRemoveRect.x, lastRemoveRect.y + 190, lastRemoveRect.width, lastRemoveRect.height);
            //
            //     if (GUI.Button(rect, " +"))
            //     {
            //         Undo.RecordObject(thisEquipment, "Add Modifier");
            //         thisEquipment.statModifiers.Add(new StatModifier(eModifierType.Flat, 0));
            //         UpdateModifierList();
            //     }
            // }
            // else
            // {
            //     GUI.backgroundColor = Style.originalColour;
            //     
            //     
            //     if (GUILayout.Button("Create Modifiers"))
            //     {
            //         Undo.RecordObject(thisEquipment, "Add Modifier");
            //         thisEquipment.statModifiers.Add(new StatModifier(eModifierType.Flat, 0));
            //         UpdateModifierList();
            //     }
            // }
            //
            // GUI.backgroundColor = Style.originalColour;
        }

        private void DrawRemoveButton(int removeIndex)
        {
            GUI.backgroundColor = StatSystemStyle.removeColour;
            
            if (GUILayout.Button("X", GUILayout.Width(30f)))
            {
                Undo.RecordObject(thisEquipment, "Remove Modifier");
                RemoveMod(removeIndex);
                UpdateModifierList();
            }

            lastRemoveRect = GUILayoutUtility.GetLastRect();
            
            GUI.backgroundColor = StatSystemStyle.originalColour;
        }

        private void RemoveMod(int removeIndex)
        {
            thisEquipment.statModifiers.RemoveAt(removeIndex);
            thisEquipment.statNameIndices.RemoveAt(removeIndex);
        }

        private void DrawClearModButton()
        {
            if (thisEquipment.statModifiers.Count == 0) return;
            
            if (GUILayout.Button("Clear Modifiers"))
            {
                bool confirmation = EditorUtility.DisplayDialog
                    ("Clear modifiers?",
                    "Are you sure you want to completely clear all the current modifiers on this Equipment?",
                    "Clear",
                    "Don't Clear");

                if (confirmation)
                {
                    Undo.RecordObject(thisEquipment, "Clear Modifier");
                    thisEquipment.ClearModList();
                    UpdateModifierList();
                }
            }
        }

        private void UpdateModifierList()
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
