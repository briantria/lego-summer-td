using System;
using UnityEngine;
using Unity.LEGO.Minifig;
using Unity.LEGO.Game;

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

        private void OnEnable()
        {
            EventManager.AddListener<OptionsMenuEvent>(OnGamePause);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OptionsMenuEvent>(OnGamePause);
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
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
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        break;
                    }
                default:
                    break;
            }
        }

        public void OnGamePause(OptionsMenuEvent evt)
        {
            Cursor.visible = evt.Active;
            if (evt.Active)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
