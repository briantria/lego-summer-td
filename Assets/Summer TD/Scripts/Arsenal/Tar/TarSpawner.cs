using System.Collections;
using UnityEngine;
using Unity.LEGO.Game;

namespace Lego.SummerJam.NoFrogsAllowed
{
    [RequireComponent(typeof(CustomAction))]
    public class TarSpawner : MonoBehaviour, IAction
    {
        #region Serialized Fields
        [SerializeField] private GameObject _buyerObj;
        [SerializeField] private GameObject _sellerObj;
        [SerializeField] private GameObject _tarPrefab;

        [Space(8)]
        [SerializeField] private int _price;

        [Space(10)]
        // Note: The following 'Variable(s)' was created using LEGO Microgame Editors
        // It's a ScriptableObject located at Assets/LEGO/Scriptable Objects
        [SerializeField] private Variable _coins;
        #endregion

        private GameObject _tarObj;

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
            ShowSeller();
        }
        #endregion

        private void ShowSeller()
        {
            _sellerObj.SetActive(true);
            _buyerObj.SetActive(false);
            if (_tarObj != null)
            {
                Destroy(_tarObj);
            }
        }

        private void ShowTar()
        {
            _sellerObj.SetActive(false);
            _buyerObj.SetActive(true);
            _tarObj = Instantiate(_tarPrefab, transform);
            _tarObj.SetActive(true);
        }

        private void BuyTrap()
        {
            int currentCoins = VariableManager.GetValue(_coins);
            if (currentCoins - _price < 0)
            {
                return;
            }

            VariableManager.SetValue(_coins, currentCoins - _price);
            ShowTar();
        }

        private void SellTrap()
        {
            int currentCoins = VariableManager.GetValue(_coins);
            VariableManager.SetValue(_coins, currentCoins + _price);
            ShowSeller();
        }

        public void Activate()
        {
            if (_tarObj != null)
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
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
