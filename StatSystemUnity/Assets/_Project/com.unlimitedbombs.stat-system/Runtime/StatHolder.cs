using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    /// <summary>
    /// ScriptableObject that stores base values of stats and their modifiers. Remove and add variables in this script to change the
    /// stats your game characters will use. To avoid overwriting base values, it is recommended you instantiate this asset
    /// for the character that will use it.
    /// </summary>
    [CreateAssetMenu(fileName = "Stat Holder", menuName = "Stat System/Stat Holder")]
    public class StatHolder : ScriptableObject
    {
        public CharacterStat health;
        public CharacterStat mana;
        public CharacterStat defense;
        public CharacterStat strength;
        public CharacterStat magic;
    }
}
