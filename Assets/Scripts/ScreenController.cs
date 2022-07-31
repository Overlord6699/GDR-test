using System.Collections.Generic;
using UnityEngine;

namespace GDRTest
{
    public class ScreenController : MonoBehaviour
    {
        private static ScreenController _instance;
        public static ScreenController Instance
        {
            get
            {
                if (_instance)
                    return _instance;

                _instance = new GameObject("SpaceChecker").AddComponent<ScreenController>();
                //DontDestroyOnLoad(_instance);
                return _instance;
            }
        }

        [SerializeField][Min(0)]
        private float _distanceBetweenObjects = 0.1f;


        public float ScreenBottom { get { return _screenBottom; } }
        private float _screenBottom;

        public float ScreenTop { get { return _screenTop; } }
        private float _screenTop;

        public float ScreenRight { get { return _screenRight; } }
        private float _screenRight;

        public float ScreenLeft { get { return _screenLeft; } }
        private float _screenLeft;

        private Camera _camera;

        private List<Vector2> _positions = new List<Vector2>(50);

        private void Awake()
        {
            _instance = this;
            _camera = Camera.main;

            var screenWidth = _camera.pixelWidth;
            var screenHeight = _camera.pixelHeight;
            _screenBottom = _camera.ScreenToWorldPoint(new Vector2(0f, 0f)).y;
            _screenTop = _camera.ScreenToWorldPoint(new Vector2(0f, screenHeight)).y;
            _screenLeft = _camera.ScreenToWorldPoint(new Vector2(0f, 0f)).x;
            _screenRight = _camera.ScreenToWorldPoint(new Vector2(screenWidth, 0f)).x;
        }

        public bool CheckFreeSpace(in Vector2 position)
        {
            foreach (Vector2 pos in _positions)
            {
                if ((pos - position).magnitude < _distanceBetweenObjects)
                    return false;
            }

            _positions.Add(position);
            return true;
        }

        public bool CheckFreeSpace(in Vector2 position, in float distance)
        {
            foreach (Vector2 pos in _positions)
            {
                if ((pos - position).magnitude < distance)
                    return false;
            }

            _positions.Add(position);
            return true;
        }

        public Vector2 GetRandomPosition()
        {
            return new Vector2(
                Random.Range(_screenLeft, _screenRight),
                Random.Range(_screenBottom, _screenTop)
            );
        }
    }
}