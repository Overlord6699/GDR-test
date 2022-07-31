using GDRTest.Config;
using GDRTest.Generators;
using System;
using System.Collections;
using UnityEngine;

namespace GDRTest {
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private Achievement<int> _coinsGoal;
        [SerializeField]
        private GameConfig _config;
        [SerializeField]
        private ChangeableValue<bool> _isPlayerAlive;

        [SerializeField]
        private Player _player;


        [SerializeField][Header("Спавнеры:")]
        private Generator<Coin> _coinGenerator;
        [SerializeField]
        private Generator<Spike> _spikeGenerator;

        [Header("Настройки:")]
        private float _spawnTime = 1;

        private ScreenController _screen;

        private void Start()
        {
            _coinsGoal.Init();
            _coinsGoal.OnAchievementComplete.AddListener(OnWinGame);
            _isPlayerAlive.OnValueChanged.AddListener(OnPlayerStateChanged);

            _screen = ScreenController.Instance;

            InitSystems();

            StartCoroutine(SpawnPlayerCoroutine(SpawnPlayer));
        }

        private void OnDisable()
        {
            _isPlayerAlive.OnValueChanged.RemoveListener(OnPlayerStateChanged);
        }

        private void InitSystems()
        {
            //можно было им и просто конфиг передать
            _coinGenerator.Init(_config.NumOfMoney);
            _spikeGenerator.Init(_config.NumOfSpikes);

        }

        private void ClearSystems()
        {
            _coinGenerator.Clear();
            _spikeGenerator.Clear();
        }

        private void OnWinGame(int winValue)
        {

        }

        public void RestartGame()
        {
            _isPlayerAlive.Value = true;

            _player.transform.position = GetPlayerPosition();
            //_player.gameObject.SetActive(true);          
            StartCoroutine(SpawnPlayerCoroutine(
                 () => {_player.gameObject.SetActive(true);
                     _player.Init();
                 }
             ));

            InitSystems();
        }

        public void  OnPlayerStateChanged(bool isPlayerAlive)
        {
            if(!isPlayerAlive)
            {
                OnPlayerDied();
            }
        }

        private void OnPlayerDied()
        {
            ClearSystems();
        }

        IEnumerator SpawnPlayerCoroutine(Action action)
        {
            yield return new WaitForSeconds(_spawnTime);
            action();
        }

        private Vector2 GetPlayerPosition()
        {
            var position = _screen.GetRandomPosition();
            while (!_screen.CheckFreeSpace(position))
            {
                position = _screen.GetRandomPosition();
            }

            return position;
        }

        private void SpawnPlayer()
        {         
            _player = Instantiate(_player, GetPlayerPosition(), Quaternion.identity);
            _player.Init();
        }
    }

}