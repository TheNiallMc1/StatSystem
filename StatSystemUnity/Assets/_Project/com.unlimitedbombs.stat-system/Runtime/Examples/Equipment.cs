using System.Collections.Generic;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    public class Equipment : MonoBehaviour, IStatModApplicator
    {
        [SerializeField]
        private EquipmentInfo _equipmentInfo;
        [HideInInspector]
        public EquipmentInfo equipmentInfo;

        public StatHolder myStatHolder;

        private void Awake()
        {
            equipmentInfo = Instantiate(_equipmentInfo);
        }

        public void Equip()
        {
            ApplyModifier();
        }

        public void Unequip()
        {
            RemoveAllModifiers();
        }
        
        // Finds the appropriate stat in the StatHolder's list and apply the modifier
        public void ApplyModifier()
        {
            foreach (StatModifier mod in equipmentInfo.statMods)
            {
                eStatType statToModify = mod.statToModify;

                Stat stat = myStatHolder.stats[statToModify];
                
                if (stat == null)
                {
                    Debug.LogError
                        ("No Stat object of the appropriate eStatType (" + statToModify + ") was found in the StatHolder's dictionary", this);
                }
                else
                {
                    stat.AddModifier(mod);
                }
            }
        }

        public void RemoveModifier(StatModifier mod)
        {
            eStatType statToModify = mod.statToModify;

            Stat stat = myStatHolder.stats[statToModify];
                
            if (stat == null)
            {
                Debug.LogError
                    ("No Stat object of the appropriate eStatType (" + statToModify + ") was found in the StatHolder's dictionary", this);
            }
            else
            {
                stat.RemoveModifier(mod);
            }
        }

        // For each stat, remove all the modifiers this equipment applied
        public void RemoveAllModifiers()
        {
            foreach (KeyValuePair<eStatType, Stat> statEntry in myStatHolder.stats)
            {
                statEntry.Value.RemoveAllModifiersFromSource(equipmentInfo);
            }
        }
    }
}
