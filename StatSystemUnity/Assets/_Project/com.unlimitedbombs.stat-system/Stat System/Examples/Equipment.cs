using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    [CreateAssetMenu(menuName = "Stat System / Examples / Equipment", fileName = "Equipment")]
    [Serializable]
    [ExecuteAlways]
    public class Equipment : ScriptableObject
    {
        public StatList statList;
        public string equipmentName; 
        [TextArea(5, 10)]
        public string equipmentDescription;
        public Sprite equipmentIcon;
        
        //[HideInInspector]
        public List<StatModifier> statModifiers = new List<StatModifier>();
        //[HideInInspector]
        public List<int> statNameIndices = new List<int>();

        public void ClearModList()
        {
            statModifiers.Clear();
            statNameIndices.Clear();
        }
    } 
}