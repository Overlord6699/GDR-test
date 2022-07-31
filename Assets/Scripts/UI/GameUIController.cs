using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GDRTest.UI
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField]
        private ChangeableValue<int> _numOfCoins;

        [SerializeField]
        private TextMeshProUGUI _coinsText;

        private void Awake()
        {
            _numOfCoins.OnValueChanged.AddListener(UpdateCoinsText);
        }

        private void UpdateCoinsText(int numOfCoins)
        {
            _coinsText.text = numOfCoins.ToString();
        }

        private void OnDisable()
        {
            _numOfCoins.OnValueChanged.RemoveListener(UpdateCoinsText);
        }
    }
}
