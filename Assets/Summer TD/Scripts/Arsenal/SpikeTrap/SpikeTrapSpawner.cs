using System.Collections;
using UnityEngine;
using Unity.LEGO.Game;

namespace Lego.SummerJam.NoFrogsAllowed
{
    [RequireComponent(typeof(CustomAction))]
    public class SpikeTrapSpawner : MonoBehaviour, IAction
    {
        [SerializeField] private GameObject _spikeSellerObj;
        [SerializeField] private GameObject _spikeTrapObj;

        private void Start()
        {
            ShowSpikeSeller();
        }

        private void ShowSpikeSeller()
        {
            _spikeSellerObj.SetActive(true);
            _spikeTrapObj.SetActive(false);
        }

        private void ShowSpikeTrap()
        {
            _spikeSellerObj.SetActive(false);
            _spikeTrapObj.SetActive(true);
        }

        public void Activate()
        {
            ShowSpikeTrap();
        }
    }
}
