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

    [RequireComponent(typeof(CustomAction))]
    public class GameLoopController : MonoBehaviour, IAction
    {
        public static Action<GameState> OnChangeGameState;

        [SerializeField] private MinifigController _minifigController;
        [SerializeField] private Transform _tpsCamTransform;

        public static Action OnReleaseFrogs;
        private GameState _currentGameState;

        private void OnEnable()
        {
            EventManager.AddListener<OptionsMenuEvent>(OnGamePause);
            GameStartAction.OnGameStart += OnLevelStart;
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OptionsMenuEvent>(OnGamePause);
            GameStartAction.OnGameStart -= OnLevelStart;
        }

        private void Start()
        {
            ChangeToBuildMode();
        }

        public void Activate()
        {
            switch (_currentGameState)
            {
                case GameState.BuildMode:
                    {
                        ChangeToShootMode();
                        break;
                    }
                default:
                    break;
            }

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

        #region System.Action Handlers
        private void OnLevelStart()
        {
            Activate();
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
            // TODO: Activate Intro Cam
            // TODO: Activate Enemy Spawn
        }

        private void ChangeToShootMode()
        {
            OnReleaseFrogs?.Invoke();
            _currentGameState = GameState.ShootMode;
            _minifigController.SetInputEnabled(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        #endregion
    }
}
