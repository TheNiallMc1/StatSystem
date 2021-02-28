using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UnlimitedBombs.StatSystem
{
    [Serializable]
    public class Stat
    {
        public float baseValue;

        public float value
        {
            get
            {
                if (isDirty || Math.Abs(baseValue - lastBaseValue) > 0.01f)
                {
                    lastBaseValue = baseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }

                return _value;
            }
        }

        private bool isDirty = true;
        private float _value;
        private float lastBaseValue = float.MinValue;
        
        private readonly List<StatModifier> _statModifiers;
        public readonly ReadOnlyCollection<StatModifier> statModifiers;

        public Stat(float _baseValue)
        {
            baseValue = _baseValue;
            _statModifiers = new List<StatModifier>();
            statModifiers = _statModifiers.AsReadOnly();
        }

        public void AddModifier(StatModifier mod)
        {
            isDirty = true;
            _statModifiers.Add(mod);
            _statModifiers.Sort(CompareModifierOrder);
        }
            
        public bool RemoveModifier(StatModifier mod)
        {
            isDirty = true;
            return _statModifiers.Remove(mod);
        }

        public bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = _statModifiers.Count - 1; i >= 0; i--)
            {
                isDirty = true;
                _statModifiers.RemoveAt(i);
                didRemove = true;
            }
            
            return didRemove;
        }
        
        private int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.order < b.order)
            {
                return -1;
            }
            else if (a.order > b.order)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private float CalculateFinalValue()
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0;

            foreach (StatModifier mod in _statModifiers)
            {
                switch (mod.modType)
                {
                    case eModifierType.Flat:
                        finalValue += mod.value;
                        break;
                    
                    case eModifierType.PercentageAdd:
                        finalValue = CalculateFinalValuePercentAdd();
                        break;
                    
                    case eModifierType.PercentageMult:
                        finalValue *= 1 + mod.value;
                        break;
                }
            }

            return (float) Math.Round(finalValue, 2);
        }

        private float CalculateFinalValuePercentAdd()
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < _statModifiers.Count; i++)
            {
                StatModifier mod = _statModifiers[i];
                
                sumPercentAdd += mod.value;

                if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].modType != eModifierType.PercentageAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }

            return finalValue;
        }
    }
}
