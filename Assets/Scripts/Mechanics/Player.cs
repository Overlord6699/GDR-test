using UnityEngine;

namespace GDRTest
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private ChangeableValue<int> _numOfCoins;

        [SerializeField]
        private ChangeableValue<bool> _isPlayerAlive;

        public delegate void InitPlayer();
        public event InitPlayer OnInitialized;

        const string SPIKE_TAG = "Spike", COIN_TAG = "Coin";

        private void OnEnable()
        {
            _isPlayerAlive.Value = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == SPIKE_TAG)
                Die();
            if (collision.tag == COIN_TAG) {
                Destroy(collision.gameObject);
                _numOfCoins.Value++;
            }
                
        }

        public void Init()
        {
            _numOfCoins.Value = 0;
            OnInitialized?.Invoke();
        }

        private void Die()
        {
            _isPlayerAlive.Value = false;
            gameObject.SetActive(false);

        }
    }
}
