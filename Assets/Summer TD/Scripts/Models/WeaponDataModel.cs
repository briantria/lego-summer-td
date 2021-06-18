using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public enum WeaponType
    { 
        Cannon
    }

    public class WeaponDataModel
    {
        public int ID { get; set; }
        public WeaponType Type { get; set; }
    }
}
