using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    /// <summary>
    /// The StatHolder ScriptableObject is used to define the stats of individual actors who make use of the stat
    /// system. As it is an SO, you should instantiate this after making reference to it to avoid changing the asset directly.
    /// </summary>
    [CreateAssetMenu(menuName = "Stat System / Stat Holder", fileName = "Stat Holder")]
    [ExecuteAlways]
    [Serializable]
    public class StatHolder : ScriptableObject
    {
        public StatList statList; 
        // Used to determine the stats available to this StatHolder
        
        public Dictionary<string, Stat> stats = new Dictionary<string, Stat>(); 
        // Dictionary storing the Stats, which can be fetched by using the name of the stat as a string

        public List<string> statNames = new List<string>();
        public List<Stat> statValues = new List<Stat>();

        private List<string> tempKeyList;
        private List<Stat> tempValueList;
        // Temp lists to get around the lack of dictionary serialization 

        public void OnValidate()
        {
            
        }

        public void UpdateDictionaryEntry(string statName, int baseValue)
        {
            
            if (stats.ContainsKey(statName))
            {
                stats[statName].baseValue = baseValue;
                stats[statName].statName = statName;
            }
            else
            {
                Stat newStat = new Stat(statName, baseValue);
                stats.Add(statName, newStat);
            }   
        }

        public void ClearDictionary()
        {
            stats.Clear();
        }
    }
}
