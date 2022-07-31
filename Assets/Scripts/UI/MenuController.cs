using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GDRTest.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private Achievement<int> _coinsGoal;
        [SerializeField]
        private ChangeableValue<bool> _isPlayerAlive;

        [SerializeField]
        private UIAnimationContoller _anim;

        public UnityEvent OnMenuHidden, OnMenuShown;

        [SerializeField]
        private TextMeshProUGUI _resultText;

        [SerializeField]
        private string _winText;
        [SerializeField]
        private string _loseText;

        private void Awake()
        {
            _isPlayerAlive.OnValueChanged.AddListener(ProcessPlayerState);
            _coinsGoal.OnAchievementComplete.AddListener(OnWinGame);
        }

        private void OnDisable()
        {
            _isPlayerAlive.OnValueChanged.RemoveListener(ProcessPlayerState);
        }

        public void ProcessPlayerState(bool isAlive)
        {
            if (isAlive)
            {
                //Hide();
            }
            else
            {
                SetLoseText();
                Show();
            }
        }

        private void OnWinGame(int winVal)
        {
            SetWinText();
            Show();
        }

        private void SetLoseText()
        {
            _resultText.text = _loseText;
        }

        private void SetWinText()
        {
            _resultText.text = _winText;
        }

        public void Show()
        {
            _anim.StartShowAnim();
        }

        public void Hide()
        {
            _anim.StartHideAnim();
        }

        private void OnHideAnimOver()
        {
            OnMenuHidden?.Invoke();
        }

        private void OnShowAnimOver()
        {
            OnMenuShown?.Invoke();
        }
    }
}
