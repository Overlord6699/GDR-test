using UnityEngine;

namespace GDRTest
{
    [RequireComponent(typeof(LineRenderer))]
    public class TrackRenderer : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer _renderer;

        [SerializeField]
        private float _distance = 0.2f;

        public void ShowTrajectory(in Vector2 pos, in float speed, in Vector2 destination)
        {
            Vector3[] points = new Vector3[10];


            for (int i = 0; i < points.Length; i++)
            {
                float time = i * _distance;
                points[i] = pos + time * (destination - pos);
            }

            _renderer.positionCount = points.Length;
            _renderer.SetPositions(points);
        }
    }
}