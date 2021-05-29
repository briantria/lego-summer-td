using System.Collections;
using UnityEngine;
using Unity.LEGO.Game;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class TurretController : MonoBehaviour
    {
        #region Serialized Field
        [SerializeField] private Transform _turretPivot;

        [Space(8)]
        [SerializeField] private float _horizontalSpeed = 2.0f;
        [SerializeField] private float _verticalSpeed = 2.0f;
        #endregion

        private Coroutine _aimRoutine;

        private void OnEnable()
        {
            EventManager.AddListener<OptionsMenuEvent>(OnGamePause);
            _aimRoutine = StartCoroutine(AimRoutine());
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OptionsMenuEvent>(OnGamePause);
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

        private IEnumerator AimRoutine()
        {
            while (true)
            {
                float h = _horizontalSpeed * Input.GetAxis("Mouse X");
                transform.Rotate(0, h, 0);

                if (_turretPivot != null)
                {
                    float v = _verticalSpeed * Input.GetAxis("Mouse Y");
                    _turretPivot.Rotate(v, 0, 0);
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
