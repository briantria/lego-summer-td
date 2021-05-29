using System;
using UnityEngine;
using Unity.LEGO.Minifig;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public enum GameState
    {
        BuildMode,
        ShootMode
    }

    [RequireComponent(typeof(CustomAction))]
    public class GameLoopController : MonoBehaviour, IAction
    {
        [SerializeField] private MinifigController _minifigController;

        public static Action OnReleaseFrogs;
        private GameState _currentGameState;

        private void Start()
        {
            _currentGameState = GameState.BuildMode;
            _minifigController.SetInputEnabled(true);
        }

        public void Activate()
        {
            switch (_currentGameState)
            {
                case GameState.BuildMode:
                    {
                        OnReleaseFrogs?.Invoke();
                        _currentGameState = GameState.ShootMode;
                        _minifigController.SetInputEnabled(false);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
