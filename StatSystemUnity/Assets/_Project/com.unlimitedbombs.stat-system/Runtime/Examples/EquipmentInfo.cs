using System.Collections.Generic;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    [CreateAssetMenu(fileName = "Equipment", menuName = "Stat System/Examples/Equipment Info")]
    public class EquipmentInfo : ScriptableObject 
    {
        public string itemName;
        public Sprite itemSprite;
        
        [TextArea(5, 10)] 
        public string itemDescription;
        
        public List<StatModifier> statMods = new List<StatModifier>();
    }
}