using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    [RequireComponent(typeof(EnemySpawner))]
    public class TurretSpawner : MonoBehaviour, IAction
    {
        [SerializeField] private GameObject _turretSeller;
        [SerializeField] private GameObject _basicTurret;

        private void Start()
        {
            ShowTurretSeller();
        }

        private void ShowTurret()
        {
            _turretSeller.SetActive(false);
            _basicTurret.SetActive(true);
        }

        private void ShowTurretSeller()
        {
            _turretSeller.SetActive(true);
            _basicTurret.SetActive(false);
        }

        public void Activate()
        {
            ShowTurret();
        }
    }
}
