using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    [RequireComponent(typeof(CustomAction))]
    public class TurretSpawner : MonoBehaviour, IAction
    {
        #region Serialized Fields
        [SerializeField] private Transform _player;
        [SerializeField] private GameObject _basicTurret;
        [SerializeField] private GameObject _turretSeller;
        [SerializeField] private GameObject _gameStartTrigger;
        #endregion

        private TurretController _turretController;

        private void OnEnable()
        {
            GameLoopController.OnReleaseFrogs += OnReleaseFrogs;
        }

        private void OnDisable()
        {
            GameLoopController.OnReleaseFrogs -= OnReleaseFrogs;
        }

        private void Start()
        {
            ShowTurretSeller();
        }

        private void ShowTurret()
        {
            _turretSeller.SetActive(false);
            _gameStartTrigger.SetActive(true);

            GameObject basicTurretObj = Instantiate(_basicTurret, transform);
            basicTurretObj.SetActive(true);

            _turretController = basicTurretObj.GetComponent<TurretController>();
            _turretController.SetPlayer(_player);
        }

        private void ShowTurretSeller()
        {
            _gameStartTrigger.SetActive(false);
            _turretSeller.SetActive(true);

            if (_turretController != null)
            {
                Destroy(_turretController.gameObject);
            }
        }

        private void OnReleaseFrogs()
        {
            _gameStartTrigger.SetActive(false);
        }

        public void Activate()
        {
            ShowTurret();
        }
    }
}
