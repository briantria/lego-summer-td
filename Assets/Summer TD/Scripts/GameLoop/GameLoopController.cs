using System;
using UnityEngine;
using Unity.LEGO.Minifig;
using Unity.LEGO.Game;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public enum GameState
    {
        BuildMode,
        LevelIntro,
        ShootMode
    }

    public class GameLoopController : MonoBehaviour
    {
        public static Action<GameState> OnChangeGameState;
        //public static Action OnReleaseFrogs;

        [SerializeField] private MinifigController _minifigController;

        private GameState _currentGameState;

        private void OnEnable()
        {
            EventManager.AddListener<OptionsMenuEvent>(OnGamePause);
            GameStartAction.OnGameStart += NextState;
            CameraDirector.OnLevelIntroDone += NextState;
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OptionsMenuEvent>(OnGamePause);
            GameStartAction.OnGameStart -= NextState;
            CameraDirector.OnLevelIntroDone -= NextState;
        }

        private void Start()
        {
            ChangeToBuildMode();
        }

        #region Event Handlers
        private void NextState()
        {
            switch (_currentGameState)
            {
                case GameState.BuildMode:
                    {
                        //ChangeToShootMode();
                        ChangeToLevelIntro();
                        break;
                    }
                case GameState.LevelIntro:
                    {
                        ChangeToShootMode();
                        break;
                    }
                default:
                    break;
            }

            Debug.Log("current state: " + _currentGameState.ToString());
            OnChangeGameState?.Invoke(_currentGameState);
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
        #endregion

        #region Game State Handlers
        private void ChangeToBuildMode()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            _currentGameState = GameState.BuildMode;
            _minifigController.SetInputEnabled(true);
        }

        private void ChangeToLevelIntro()
        {
            //OnReleaseFrogs?.Invoke();
            _currentGameState = GameState.LevelIntro;
            _minifigController.SetInputEnabled(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void ChangeToShootMode()
        {
            //OnReleaseFrogs?.Invoke();
            _currentGameState = GameState.ShootMode;
            _minifigController.SetInputEnabled(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        #endregion
    }
}
