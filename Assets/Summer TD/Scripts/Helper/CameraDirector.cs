using System;
using System.Collections;
using UnityEngine;
using Cinemachine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class CameraDirector : MonoBehaviour
    {
        public static Action OnLevelIntroDone;

        [SerializeField] private GameObject _setupCamObj;
        [SerializeField] private GameObject _tpsCamObj;
        [SerializeField] private GameObject _levelIntroCamObj;

        private CinemachineVirtualCamera _levelIntroCam;
        private CinemachineTrackedDolly _levelIntroDollyPath;

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
            _levelIntroCam = _levelIntroCamObj.GetComponent<CinemachineVirtualCamera>();
            _levelIntroDollyPath = _levelIntroCam.GetCinemachineComponent<CinemachineTrackedDolly>();
            ActivateSetupCam();
        }
        #endregion

        private void ActivateTPSCam()
        {
            // TODO: Statemachine
            _tpsCamObj.SetActive(true);
            _setupCamObj.SetActive(false);
            _levelIntroCamObj.SetActive(false);
        }

        private void ActivateSetupCam()
        {
            _setupCamObj.SetActive(true);
            _tpsCamObj.SetActive(false);
            _levelIntroCamObj.SetActive(false);
        }

        private void ActivateLevelIntroCam()
        {
            _levelIntroCamObj.SetActive(true);
            _setupCamObj.SetActive(false);
            _tpsCamObj.SetActive(false);

            _levelIntroDollyPath.m_PathPosition = 0.0f;
            StartCoroutine(LevelIntroTrackRoutine());
            //_levelIntroCam.setpath
        }

        private IEnumerator LevelIntroTrackRoutine()
        {
            while (_levelIntroDollyPath.m_PathPosition < 1.0f)
            {
                _levelIntroDollyPath.m_PathPosition += Time.deltaTime * 0.5f;
                yield return null;
            }

            yield return new WaitForSeconds(1.0f);
            OnLevelIntroDone?.Invoke();
        }

        #region System.Action Handlers
        private void OnChangeGameState(GameState currentGameState)
        {
            switch (currentGameState)
            {
                case GameState.LevelIntro:
                    ActivateLevelIntroCam();
                    break;

                case GameState.ShootMode:
                    ActivateTPSCam();
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}
