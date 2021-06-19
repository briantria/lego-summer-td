using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class GatesController : MonoBehaviour
    {
        [SerializeField] private GameObject _staticGates;
        [SerializeField] private GameObject _explodingGatesPrefab;

        private GameObject _explodingGates;

        private void OnEnable()
        {
            GameLoopController.OnChangeGameState += OnGameStateChange;
        }

        private void OnDisable()
        {
            GameLoopController.OnChangeGameState -= OnGameStateChange;
        }
        private void OnGameStateChange(GameState currentGameState)
        {
            switch (currentGameState)
            {
                case GameState.GateExplosion:
                    {
                        _staticGates.SetActive(false);
                        _explodingGates = Instantiate(_explodingGatesPrefab, transform);
                        _explodingGates.SetActive(true);
                        _explodingGates.transform.localPosition = Vector3.zero;
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
