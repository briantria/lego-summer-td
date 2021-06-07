using System.Collections;
using UnityEngine;
using Unity.LEGO.Game;

namespace Lego.SummerJam.NoFrogsAllowed
{
    [RequireComponent(typeof(CustomAction))]
    public class SpikeTrapSpawner : MonoBehaviour, IAction
    {
        #region Serialized Fields
        [SerializeField] private GameObject _spikeSellerObj;
        [SerializeField] private GameObject _spikeTrapObj;

        [Space(8)]
        [SerializeField] private int _price;

        [Space(10)]
        // Note: The following 'Variable(s)' was created using LEGO Microgame Editors
        // It's a ScriptableObject located at Assets/LEGO/Scriptable Objects
        [SerializeField] private Variable _coins;

        #endregion

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
            int currentCoins = VariableManager.GetValue(_coins);
            if (currentCoins - _price < 0)
            {
                return;
            }

            VariableManager.SetValue(_coins, currentCoins - _price);
            ShowSpikeTrap();
        }
    }
}
