namespace UnlimitedBombs.StatSystem
{
    public enum eModifierType
    {
        Flat = 100,
        PercentageAdd = 200,
        PercentageMult = 300
    }
    
    public class StatModifier
    {
        public readonly float value;
        public readonly eModifierType modType;
        public readonly int order;
        public readonly object source;

        /// <param name="_value">The value of this modifier, affecting how it increases or decreases the stat.</param>
        /// 
        /// <param name="_modType">The type of this modifier. Flat will add directly to the stat.
        ///                         PercentageAdd will additively add percentages.
        ///                         PercentageMult will multiplicatively add percentages.</param>
        /// 
        /// <param name="_order">The order / priority of this modifier. Mods with lower values will be applied
        ///                      before those with higher values. Leave this empty to use default sorting order.</param>
        ///
        /// <param name="_source">The source that is applying this modifier, such as an item or skill.</param>
        public StatModifier(float _value, eModifierType _modType, int _order, object _source) // Full constructor
        {
            value = _value;
            modType = _modType;
            order = _order;
            source = _source;
        }

        /// <param name="_value">The value of this modifier, affecting how it increases or decreases the stat.</param>
        /// 
        /// <param name="_modType">The type of this modifier. Flat will add directly to the stat.
        ///                         PercentageAdd will additively add percentages.
        ///                         PercentageMult will multiplicatively add percentages.</param>
        /// <param name="_source">The source that is applying this modifier, such as an item or skill.</param>
        public StatModifier(float _value, eModifierType _modType, object _source) // Default order, assigned source
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
        public StatModifier(float _value, eModifierType _modType, int _order) // Custom order, null source
        {
            value = _value;
            modType = _modType;
            order = _order;
            source = null;
        }
        
        /// <param name="_value">The value of this modifier, affecting how it increases or decreases the stat.</param>
        /// 
        /// <param name="_modType">The type of this modifier. Flat will add directly to the stat.
        ///                         PercentageAdd will additively add percentages.
        ///                         PercentageMult will multiplicatively add percentages.</param>
        public StatModifier(float _value, eModifierType _modType) // Default order and null source
        {
            value = _value;
            modType = _modType;
            order = (int)_modType;
            source = null;
        }
    }
}
