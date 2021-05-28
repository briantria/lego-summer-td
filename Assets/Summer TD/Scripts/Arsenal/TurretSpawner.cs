using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    [RequireComponent(typeof(CustomAction))]
    public class TurretSpawner : MonoBehaviour, IAction
    {
        [SerializeField] private GameObject _turretSeller;

        [Space(8)]
        [SerializeField] private GameObject _basicTurret;
        [SerializeField] private GameObject _gameStartTrigger;

        private void Start()
        {
            ShowTurretSeller();
        }

        private void ShowTurret()
        {
            _turretSeller.SetActive(false);
            _basicTurret.SetActive(true);
            _gameStartTrigger.SetActive(true);
        }

        private void ShowTurretSeller()
        {
            _turretSeller.SetActive(true);
            _basicTurret.SetActive(false);
            _gameStartTrigger.SetActive(false);
        }

        public void Activate()
        {
            ShowTurret();
        }
    }
}
