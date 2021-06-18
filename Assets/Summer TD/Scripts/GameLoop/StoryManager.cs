using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class StoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject _welcomeMessageObj;
        [SerializeField] private GameObject _startGameMessageObj;
        [SerializeField] private GameObject _trapsUnlockMessageObj;
        [SerializeField] private GameObject _defaultMessageObj;

        private GameProgressData _gameProgress;

        private void OnEnable()
        {
            TurretSpawner.OnBuyCannon += OnBuyCannon;
        }

        private void OnDisable()
        {
            TurretSpawner.OnBuyCannon -= OnBuyCannon;
        }

        private void Start()
        {
            _welcomeMessageObj.SetActive(false);
            _startGameMessageObj.SetActive(false);
            //_trapsUnlockMessageObj.SetActive(false);
            //_defaultMessageObj.SetActive(false);

            _gameProgress = AssetResources.GameProgress;
            switch(_gameProgress.Data.Level)
            {
                case 0:
                    _welcomeMessageObj.SetActive(true);
                    break;

                case 1:
                    //_trapsUnlockMessageObj.SetActive(true);
                    break;

                default:
                    //_defaultMessageObj.SetActive(true);
                    break;
            }
        }

        private void OnBuyCannon()
        {
            if (_gameProgress.Data.Level != 0)
            {
                return;
            }

            _welcomeMessageObj.SetActive(false);
            _startGameMessageObj.SetActive(true);
        }
    }
}
