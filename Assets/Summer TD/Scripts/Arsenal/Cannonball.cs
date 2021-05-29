using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class Cannonball : MonoBehaviour
    {
        private float _damage = 1.0f;

        private void OnCollisionEnter(Collision collision)
        {
            //RaycastHit[] hits = Physics.SphereCastAll(transform.position, 5.0f, Vector3.zero);
            // TOFIX:
            LayerMask mask = LayerMask.GetMask(new string[] { "Default", "Environment" });
            Collider[] colliderHits = Physics.OverlapSphere(transform.position, 5.0f, mask);

            foreach (Collider hit in colliderHits)
            {
                Frog frog = hit.gameObject.GetComponent<Frog>();
                if (frog == null)
                {
                    continue;
                }

                frog.Damage(_damage);
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.AddExplosionForce(300.0f, transform.position, 20.0f);
                }
            }

            Destroy(gameObject);
        }
    }
}
