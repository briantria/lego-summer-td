using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class EnemySpawner : MonoBehaviour, IAction
    {
        public void Activate()
        {
            Debug.Log("GO!");
        }
    }
}
