using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    /// <summary>
    /// Place the StatHolder script on any character or object that uses the stat system, and then define
    /// the values of each stat in the inspector. Add or remove variables from this script as needed for your game, but
    /// ALSO UPDATE THE eStatType ENUM.
    /// </summary>
    public class StatHolder : MonoBehaviour
    {
        // Dictionary that holds the different stats of characters. 
        public Dictionary<eStatType, Stat> stats;

        public Stat vitality;
        public Stat defence;
        public Stat strength;
        
        private void Awake()
        {
            stats = new Dictionary<eStatType, Stat>
            {
                {eStatType.Vitality, vitality}, {eStatType.Defence, defence}, {eStatType.Strength, strength}
            };
        }
    }
}