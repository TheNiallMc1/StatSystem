﻿using System;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    public enum eModifierType
    {
        Flat = 100,
        PercentageAdd = 200,
        PercentageMult = 300
    }
    
    [Serializable]
    public class StatModifier
    {
        #region Inspector Initialisation

        public eStatType statToModify;
        [Tooltip("The type of the modifier. Flat adds directly, PercentageAdd additively adds percentages, and PercentageMult multiplicatively adds percentages.")]
        public eModifierType _modType;
        [Tooltip("The value to add or subtract from the stat.")]
        public float _value;
        [Tooltip("The order / priority the modifier is applied in. Lower orders are applied first. Leave empty or enter 0 to use default. Defaults: Flat = 100, PAdd = 200, PMult = 300")]
        public int _order;
        
        #endregion

        public readonly eModifierType modType;
        public readonly float value;
        public readonly int order;
        public readonly object source;

        /// <param name="_value">The value of this modifier, affecting how it increases or decreases the stat.</param>
        /// 
        /// <param name="_modType">The type of this modifier. Flat will add directly to the stat.
        ///                         PercentageAdd will additively add percentages.
        ///                         PercentageMult will multiplicatively add percentages.</param>
        /// 
        /// <param name="_order">The order / priority of this modifier. Mods with lower values will be applied
        ///                      before those with higher values. Leave this empty or enter 0 to use default sorting order.</param>
        ///
        /// <param name="_source">The source that is applying this modifier, such as an item or skill.</param>
        public StatModifier(eModifierType _modType, float _value, int _order, object _source) // Full constructor
        {
            value = _value;
            modType = _modType;
            
            if (order > 0)
            {
                order = _order;
            }
            else
            {
                order = (int) _modType;
            }
            
            source = _source;
        }

        /// <param name="_value">The value of this modifier, affecting how it increases or decreases the stat.</param>
        /// 
        /// <param name="_modType">The type of this modifier. Flat will add directly to the stat.
        ///                         PercentageAdd will additively add percentages.
        ///                         PercentageMult will multiplicatively add percentages.</param>
        /// <param name="_source">The source that is applying this modifier, such as an item or skill.</param>
        public StatModifier(eModifierType _modType, float _value, object _source) // Default order, assigned source
        {
            value = _value;
            modType = _modType;
            order = (int)_modType;
            source = _source;
        }
        
        /// <param name="_value">The value of this modifier, affecting how it increases or decreases the stat.</param>
        /// 
        /// <param name="_modType">The type of this modifier. Flat will add directly to the stat.
        ///                         PercentageAdd will additively add percentages.
        ///                         PercentageMult will multiplicatively add percentages.</param>
        ///<param name="_order">The order / priority of this modifier. Mods with lower values will be applied
        ///                      before those with higher values. Leave this empty to use default sorting order.</param>
        public StatModifier(eModifierType _modType, float _value,  int _order) // Custom order, null source
        {
            value = _value;
            modType = _modType;
            
            if (order > 0)
            {
                order = _order;
            }
            else
            {
                order = (int) _modType;
            }
            
            source = null;
        }
        
        /// <param name="_value">The value of this modifier, affecting how it increases or decreases the stat.</param>
        /// 
        /// <param name="_modType">The type of this modifier. Flat will add directly to the stat.
        ///                         PercentageAdd will additively add percentages.
        ///                         PercentageMult will multiplicatively add percentages.</param>
        public StatModifier(eModifierType _modType, float _value) // Default order and null source
        {
            value = _value;
            modType = _modType;
            order = (int)_modType;
            source = null;
        }
    }
}
