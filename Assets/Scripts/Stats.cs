using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats : Life
{
    #region Structs
    [Serializable] 
    public struct StatBlock
    {
        public string name;
        public int value;
        public int tempValue;
    }
    #endregion
    #region Variables
    public StatBlock[] characterStats = new StatBlock[6];
    #endregion


}
