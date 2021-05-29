using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField] private Transform _turretPivot;

        [Space(8)]
        [SerializeField] private float _horizontalSpeed = 2.0F;
        [SerializeField] private float _verticalSpeed = 2.0F;

        void Update()
        {
            float h = _horizontalSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(0, h, 0);

            if (_turretPivot != null)
            {
                float v = _verticalSpeed * Input.GetAxis("Mouse Y");
                _turretPivot.Rotate(v, 0, 0);
            }
        }
    }
}
