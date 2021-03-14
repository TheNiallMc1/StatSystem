using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnlimitedBombs.StatSystem
{
    
    /// <summary>
    /// StatList is a scriptable object which is used to add or remove Stats from your game. Adding a stat here will allow
    /// any StatHolder to use that stat
    /// </summary>
    [CreateAssetMenu(menuName = "Stat System / Stat List", fileName = "Stat List")]
    [Serializable]
    public class StatList : ScriptableObject
    {
        public List<string> statNames = new List<string>();
    }
}
