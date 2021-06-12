using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.LEGO.Game;

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

        [Space(8)]
        [SerializeField] private int _price;

        [Space(10)]
        // Note: The following 'Variable(s)' was created using LEGO Microgame Editors
        // It's a ScriptableObject located at Assets/LEGO/Scriptable Objects
        [SerializeField] private Variable _coins;
        #endregion

        private TurretController _turretController;

        private void OnEnable()
        {
            GameLoopController.OnChangeGameState += OnChangeGameState;
        }

        private void OnDisable()
        {
            GameLoopController.OnChangeGameState -= OnChangeGameState;
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

        #region System.Action Handlers
        private void OnChangeGameState(GameState currentGameState)
        {
            switch (currentGameState)
            {
                case GameState.ShootMode:
                    _gameStartTrigger.SetActive(false);
                    break;

                default:
                    break;
            }
        }
        #endregion

        //private void OnReleaseFrogs()
        //{
        //    _gameStartTrigger.SetActive(false);
        //}

        public void Activate()
        {
            int currentCoins = VariableManager.GetValue(_coins);
            if (currentCoins - _price < 0)
            {
                return;
            }

            VariableManager.SetValue(_coins, currentCoins - _price);
            ShowTurret();
        }
    }
}
