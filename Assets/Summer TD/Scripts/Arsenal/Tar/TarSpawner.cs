using System.Collections;
using UnityEngine;
using Unity.LEGO.Game;

namespace Lego.SummerJam.NoFrogsAllowed
{
    [RequireComponent(typeof(CustomAction))]
    public class TarSpawner : MonoBehaviour, IAction
    {
        #region Serialized Fields
        [SerializeField] private GameObject _sellerObj;
        [SerializeField] private GameObject _tarPrefab;

        [Space(8)]
        [SerializeField] private int _price;

        [Space(10)]
        // Note: The following 'Variable(s)' was created using LEGO Microgame Editors
        // It's a ScriptableObject located at Assets/LEGO/Scriptable Objects
        [SerializeField] private Variable _coins;
        #endregion

        private GameObject _tarObj;

        private void Start()
        {
            ShowSeller();
        }

        private void ShowSeller()
        {
            _sellerObj.SetActive(true);
            if (_tarObj != null)
            {
                Destroy(_tarObj);
            }
        }

        private void ShowTar()
        {
            _sellerObj.SetActive(false);
            _tarObj = Instantiate(_tarPrefab, transform);
            _tarObj.SetActive(true);
        }

        public void Activate()
        {
            int currentCoins = VariableManager.GetValue(_coins);
            if (currentCoins - _price < 0)
            {
                return;
            }

            VariableManager.SetValue(_coins, currentCoins - _price);
            ShowTar();
        }
    }
}
