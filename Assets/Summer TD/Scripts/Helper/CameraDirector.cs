using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    [RequireComponent(typeof(CustomAction))]
    public class CameraDirector : MonoBehaviour, IAction
    {
        [SerializeField] private GameObject _setupCamObj;
        [SerializeField] private GameObject _tpsCamObj;

        private void Start()
        {
            _setupCamObj.SetActive(true);
            _tpsCamObj.SetActive(false);
        }

        public void Activate()
        {
            // TODO: Statemachine
            _setupCamObj.SetActive(false);
            _tpsCamObj.SetActive(true);
        }

    }
}
