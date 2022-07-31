using UnityEngine;

namespace GDRTest {

    [RequireComponent(typeof(InputController))]
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField]
        private ChangeableValue<bool> _isMoving;
        [SerializeField]
        private InputController _input;
        [SerializeField]
        private TrackRenderer _trackRenderer;

        [SerializeField]
        private float _speed = 0.5f;
        [SerializeField]
        private float _accuracy = 0.05f;

        private Vector2 _direction, _target, _prevPosition;

        private void OnEnable()
        {
            GetComponent<Player>().OnInitialized += ResetPlayer;
        }

        private void Start()
        {
            _prevPosition = transform.position;
            _isMoving.OnValueChanged.AddListener(SetDirection);            
        }

        private void ResetPlayer()
        {
            _target = transform.position;
            _prevPosition = transform.position;
            _direction = _target - _prevPosition;
            _isMoving.Value = false;
        }

        private void FixedUpdate()
        {
            if (_isMoving.Value)
            {
                Move();
            }
        }

        private void SetDirection(bool isMoving)
        {
            if (isMoving)
            {
                _target = _input.GetPoint();
                _direction = _target - _prevPosition;
            }
        }

        

        private void Move()
        {
            transform.Translate(_direction * _speed * Time.deltaTime);

            _trackRenderer.ShowTrajectory(transform.position, _speed, _target);

            ProcessSavedPoints();
        }

        private void ProcessSavedPoints()
        {
            if (((Vector2)transform.position - _target).magnitude < _accuracy)
            {
                _prevPosition = transform.position;

                if (_input.NumOfPoints > 0)
                    _input.RemovePoint();
                if (_input.NumOfPoints > 0)
                {
                    SetDirection(true);
                }

                if (_input.NumOfPoints == 0)
                {
                    _isMoving.Value = false;
                }
            }
        }

        private void OnDisable()
        {
            GetComponent<Player>().OnInitialized -= ResetPlayer;
        }
    }
}
