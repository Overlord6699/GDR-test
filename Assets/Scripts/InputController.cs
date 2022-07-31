using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDRTest
{
    public enum MouseClick
    {
        LeftButton,
        RightButton
    }

    public class InputController : MonoBehaviour
    {
        [SerializeField]
        private MouseClick _mouseButton = MouseClick.LeftButton;
        [SerializeField]
        private int _maxNumOfPoints = 5;
        [SerializeField]
        private ChangeableValue<bool> _isMoving;
        [SerializeField][Header("Интервал затора клика мышки")]
        private float _stopTime = 0.2f;

        public int NumOfPoints { get { return _positions.Count; } }

        private Queue<Vector2> _positions = new Queue<Vector2>(20);
        private Camera _mainCam;

        private bool _checkMouseInput = true;

        private void Awake()
        {
            _mainCam = Camera.main;
        }

        private void OnEnable()
        {
            GetComponent<Player>().OnInitialized += ClearPoints;
            _checkMouseInput = true;
        }

        private void OnDisable()
        {
            GetComponent<Player>().OnInitialized -= ClearPoints;
        }

        private void FixedUpdate()
        {
            if(_checkMouseInput)
                if (Input.GetMouseButton((int)_mouseButton))
                {
                    SavePoint();
                    _isMoving.Value = true;
                    //блокировка мышки на время
                    _checkMouseInput = false;
                    StartCoroutine(WaitCoroutine());
                }
        }

        private void ClearPoints()
        {
            _positions.Clear();
        }

        IEnumerator WaitCoroutine()
        {
            yield return new WaitForSeconds(_stopTime);
            _checkMouseInput = true;
        }

        private void SavePoint()
        {
            if (_positions.Count < _maxNumOfPoints)
            {
                _positions.Enqueue(_mainCam.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        public void RemovePoint()
        {
            _positions.Dequeue();
        }

        public Vector2 GetPoint()
        {
            return _positions.Peek();
        }
    }
}
