using System.Collections;
using UnityEngine;
using Cinemachine;
using Unity.LEGO.Game;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class TurretController : MonoBehaviour
    {
        #region Serialized Field
        [SerializeField] private Transform _verticalPivot;
        [SerializeField] private Transform _horizontalPivot;
        [SerializeField] private Transform _playerSeat;
        [SerializeField] private Transform _tpsCamAnchor;

        [Space(8)]
        [SerializeField] private float _horizontalSpeed = 2.0f;
        [SerializeField] private float _verticalSpeed = 2.0f;
        #endregion

        private Coroutine _aimRoutine;
        private Transform _playerTransform;
        private bool _selected;

        private void OnEnable()
        {
            GameLoopController.OnChangeGameState += OnChangeGameState;
            EventManager.AddListener<OptionsMenuEvent>(OnGamePause);
        }

        private void OnDisable()
        {
            GameLoopController.OnChangeGameState -= OnChangeGameState;
            EventManager.RemoveListener<OptionsMenuEvent>(OnGamePause);
        }

        private void OnReleaseFrogs()
        {
            if (_playerTransform == null)
            {
                Debug.Log("Missing player transform");
                return;
            }

            //Vector3 playerSeat = _playerSeat.position;
            //playerSeat.y += 0.3f;

            //_playerTransform.position = playerSeat;
            //_playerTransform.forward = _playerSeat.right;
            _aimRoutine = StartCoroutine(AimRoutine());
        }

        public void OnGamePause(OptionsMenuEvent evt)
        {
            if (evt.Active && _aimRoutine != null)
            {
                StopCoroutine(_aimRoutine);
            }
            else if (!evt.Active)
            {
                _aimRoutine = StartCoroutine(AimRoutine());
            }
        }

        public void SetPlayer(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        public void SetTpsCam(CinemachineVirtualCamera tpsCam)
        {
            Debug.Log("Set TPS cam.");
            tpsCam.Follow = _tpsCamAnchor;
            tpsCam.LookAt = _tpsCamAnchor;
        }

        #region System.Action Handlers
        private void OnChangeGameState(GameState currentGameState)
        {
            switch (currentGameState)
            {
                case GameState.ShootMode:
                    OnReleaseFrogs();
                    //_gameStartTrigger.SetActive(false);
                    break;

                default:
                    break;
            }
        }
        #endregion

        private IEnumerator AimRoutine()
        {
            if (_horizontalPivot == null ||
                _verticalPivot == null)
            {
                yield break;
            }

            while (true)
            {
                float h = _horizontalSpeed * Input.GetAxis("Mouse X");
                _horizontalPivot.Rotate(0, h, 0);

                float v = _verticalSpeed * Input.GetAxis("Mouse Y");
                _verticalPivot.Rotate(0, 0, v);

                Vector3 playerSeat = _playerSeat.position;
                playerSeat.y += 0.3f;
                _playerTransform.position = playerSeat;
                _playerTransform.forward = _playerSeat.right;

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
