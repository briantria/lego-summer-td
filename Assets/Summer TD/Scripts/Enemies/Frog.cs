using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class Frog : MonoBehaviour
    {
        [SerializeField] private float _jumpStrength = 2.0f;

        private Transform _target;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_target == null || !IsGrounded())
            {
                return;
            }

            Vector3 jumpVector = _target.position - transform.position;
            jumpVector.y = 0;
            jumpVector.y = jumpVector.magnitude * 5;
            jumpVector.Normalize();
            jumpVector *= _jumpStrength;

            _rb.velocity = Vector3.zero;
            _rb.AddForce(jumpVector, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Base"))
            {
                Debug.Log("Base Attacked!");
                Destroy(gameObject);
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        // source: https://gamedev.stackexchange.com/questions/105399/how-to-check-if-grounded-with-rigidbody
        private bool IsGrounded()
        {
            RaycastHit[] hits;

            //We raycast down 1 pixel from this position to check for a collider
            float radius = 0.1f;
            Vector3 positionToCheck = transform.position;
            positionToCheck.y -= radius * 1.01f;
            hits = Physics.SphereCastAll(positionToCheck, radius, new Vector3(0, -1, 0), radius);

            //if a collider was hit, we are grounded
            bool isGrounded = hits.Length > 0;
            Debug.Log("grounded? " + isGrounded.ToString());
            return isGrounded;
        }
    }
}
