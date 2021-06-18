using System.Collections;
using UnityEngine;
using Unity.LEGO.Game;

namespace Lego.SummerJam.NoFrogsAllowed
{
    [RequireComponent(typeof(CustomAction))]
    public class SpikeTrapSpawner : MonoBehaviour, IAction
    {
        #region Serialized Fields
        [SerializeField] private GameObject _buyerObj;
        [SerializeField] private GameObject _sellerObj;
        [SerializeField] private GameObject _trapGroundObj;
        [SerializeField] private GameObject _spikeTrapPrefab;

        [Space(8)]
        [SerializeField] private int _price;

        [Space(10)]
        // Note: The following 'Variable(s)' was created using LEGO Microgame Editors
        // It's a ScriptableObject located at Assets/LEGO/Scriptable Objects
        [SerializeField] private Variable _coins;
        #endregion

        private GameObject _spikeTrapObj;

        #region Unity Messages
        private void OnEnable()
        {
            GameLoopController.OnChangeGameState += OnGameStateChange;
        }

        private void OnDisable()
        {
            GameLoopController.OnChangeGameState -= OnGameStateChange;
        }

        private void Start()
        {
            ShowSpikeSeller();
        }
        #endregion

        private void ShowSpikeSeller()
        {
            _sellerObj.SetActive(true);
            _trapGroundObj.SetActive(true);
            _buyerObj.SetActive(false);
            if (_spikeTrapObj != null)
            {
                Destroy(_spikeTrapObj);
            }
        }

        private void ShowSpikeTrap()
        {
            _sellerObj.SetActive(false);
            _trapGroundObj.SetActive(false);
            _buyerObj.SetActive(true);
            _spikeTrapObj = Instantiate(_spikeTrapPrefab, transform);
            _spikeTrapObj.SetActive(true);
        }

        private void BuyTrap()
        {
            int currentCoins = VariableManager.GetValue(_coins);
            if (currentCoins - _price < 0)
            {
                return;
            }

            VariableManager.SetValue(_coins, currentCoins - _price);
            ShowSpikeTrap();
        }

        private void SellTrap()
        {
            int currentCoins = VariableManager.GetValue(_coins);
            VariableManager.SetValue(_coins, currentCoins + _price);
            ShowSpikeSeller();
        }

        public void Activate()
        {
            if (_spikeTrapObj != null)
            {
                SellTrap();
            }
            else
            {
                BuyTrap();
            }
        }

        private void OnGameStateChange(GameState currentGameState)
        {
            switch (currentGameState)
            {
                //case GameState.BuildMode:
                //    {
                //        _buyerObj.SetActive(_spikeTrapObj != null);
                //        break;
                //    }

                case GameState.ShootMode:
                    {
                        _buyerObj.SetActive(false);
                        _sellerObj.SetActive(false);
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
