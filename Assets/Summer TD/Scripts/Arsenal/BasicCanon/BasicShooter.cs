using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class BasicShooter : MonoBehaviour
    {
        [SerializeField] private GameObject _activateGuideObj;

        private void OnEnable()
        {
            GameLoopController.OnReleaseFrogs += OnReleaseFrogs;
        }

        private void OnDisable()
        {
            GameLoopController.OnReleaseFrogs -= OnReleaseFrogs;
        }

        private void OnReleaseFrogs()
        {
            _activateGuideObj.SetActive(false);
        }
    }
}
