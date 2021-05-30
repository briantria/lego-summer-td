using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class Cannonball : MonoBehaviour
    {
        private float _damage = 1.0f;

        private void OnCollisionEnter(Collision collision)
        {
            int hitLayer = collision.gameObject.layer;
            string hitLayerName = LayerMask.LayerToName(hitLayer);
            string[] maskNames = new string[] { "Enemy", "Environment" };
            LayerMask mask = LayerMask.GetMask(maskNames);
            if (!maskNames.ToList().Contains(hitLayerName))
            {
                return;
            }

            Debug.Log("on hit: " + hitLayerName);
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
                    Debug.Log("explosion affect " + frog.gameObject.name);
                    rb.AddExplosionForce(300.0f, transform.position, 20.0f);
                }
            }

            Destroy(gameObject);
        }
    }
}
