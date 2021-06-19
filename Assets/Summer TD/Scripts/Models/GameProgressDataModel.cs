using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class GameProgressDataModel
    { 
        public int Level { get; set; }
        public int Money { get; set; }
        public bool Win { get; set; }
        public List<WeaponDataModel> WeaponList { get; set; }
        public List<TrapDataModel> TrapList { get; set; }
    }
}
