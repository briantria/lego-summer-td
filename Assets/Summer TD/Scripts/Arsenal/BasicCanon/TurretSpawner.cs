using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.LEGO.Game;

namespace Lego.SummerJam.NoFrogsAllowed
{
    //[RequireComponent(typeof(CustomAction))]
    public class TurretSpawner : MonoBehaviour, IAction
    {
        #region Serialized Fields
        [SerializeField] private GameObject _basicTurret;
        [SerializeField] private GameObject _turretSeller;
        [SerializeField] private GameObject _turretBuyer;
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
            _turretBuyer.SetActive(true);
            _gameStartTrigger.SetActive(true);

            GameObject basicTurretObj = Instantiate(_basicTurret, transform);
            basicTurretObj.SetActive(true);

            _turretController = basicTurretObj.GetComponent<TurretController>();
        }

        private void ShowTurretSeller()
        {
            _gameStartTrigger.SetActive(false);
            _turretBuyer.SetActive(false);
            _turretSeller.SetActive(true);

            if (_turretController != null)
            {
                Destroy(_turretController.gameObject);
            }
        }

        public void SetPlayer(Transform playerT)
        {
            _turretController.SetPlayer(playerT);
        }

        public void SetTpsCam(CinemachineVirtualCamera tpsCam)
        {
            _turretController.SetTpsCam(tpsCam);
        }

        #region System.Action Handlers
        private void OnChangeGameState(GameState currentGameState)
        {
            switch (currentGameState)
            {
                case GameState.ShootMode:
                    _gameStartTrigger.SetActive(false);
                    _turretBuyer.SetActive(false);
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

        private void SellTurret()
        {
            int currentCoins = VariableManager.GetValue(_coins);
            VariableManager.SetValue(_coins, currentCoins + _price);
            ShowTurretSeller();
        }

        private void BuyTurret()
        {
            int currentCoins = VariableManager.GetValue(_coins);
            if (currentCoins - _price < 0)
            {
                return;
            }

            VariableManager.SetValue(_coins, currentCoins - _price);
            ShowTurret();
        }

        public void Activate()
        {
            if (_turretController != null)
            {
                SellTurret();
            }
            else
            {
                BuyTurret();
            }
        }
    }
}
