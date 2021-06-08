using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    [RequireComponent(typeof(CustomAction))]
    public class GameStartAction : MonoBehaviour, IAction
    {
        public static Action OnGameStart;

        public void Activate()
        {
            OnGameStart?.Invoke();
        }
    }
}
