using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class CameraDirector : MonoBehaviour, IAction
    {
        [SerializeField] private GameObject _setupCamObj;
        [SerializeField] private GameObject _tpsCamObj;

        #region Unity Messages
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
            _setupCamObj.SetActive(true);
            _tpsCamObj.SetActive(false);
        }
        #endregion

        public void Activate()
        {
            // TODO: Statemachine
            _setupCamObj.SetActive(false);
            _tpsCamObj.SetActive(true);
        }

        #region System.Action Handlers
        private void OnChangeGameState(GameState currentGameState)
        {
            switch (currentGameState)
            {
                case GameState.ShootMode:
                    Activate();
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}
