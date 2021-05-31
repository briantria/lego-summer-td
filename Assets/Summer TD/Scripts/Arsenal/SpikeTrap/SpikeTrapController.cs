using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lego.SummerJam.NoFrogsAllowed
{
    public class SpikeTrapController : MonoBehaviour
    {
        [SerializeField] private float _damage;

        private void OnTriggerEnter(Collider collider)
        {
            //Debug.Log("triggered by " + collider.name);
            Frog frog = collider.GetComponent<Frog>();
            if (frog != null)
            {
                frog.Damage(_damage);
            }
        }
    }
}
