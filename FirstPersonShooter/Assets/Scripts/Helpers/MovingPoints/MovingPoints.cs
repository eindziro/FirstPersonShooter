using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FirstPersonShooter
{
    public sealed class MovingPoints : MonoBehaviour
    {
        [SerializeField] private Bot _agent;
        [SerializeField] private DestroyPoint _point;

        private readonly Queue<Vector3> _points = new Queue<Vector3>();
        private readonly Color _startColor = Color.red;
        private readonly Color _endColor = Color.blue;
        private LineRenderer _lineRenderer;
        private Camera _mainCamera;
        private NavMeshPath _path;
        private Vector3 _startPoint;

        public Vector3 CurrentPoint { get; private set; }

        private void Start()
        {
            var lineRendererGo = new GameObject("LineRenderer");
            _lineRenderer = lineRendererGo.AddComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.5f;
            _lineRenderer.endWidth = 0.2f;
            _lineRenderer.material = new Material(Shader.Find("Mobile/Particles/Additive"));
            _lineRenderer.startColor = _startColor;
            _lineRenderer.endColor = _endColor;

            _startPoint = _agent.transform.position;
            _path = new NavMeshPath();

            _mainCamera = GetComponent<Camera>();
            CurrentPoint = Vector3.positiveInfinity;
        }

        private void Update()
        {
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(Input.mousePosition),out var hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    DrawPoint(hit.point);
                }
            }
        }

        private void DrawPoint(Vector3 position)
        {
            var point = Instantiate(_point, position, Quaternion.identity);
            point.OnFinishChanged += MovePoint;
            _points.Enqueue(point.transform.position);
            _startPoint = point.transform.position;
        }

        private void MovePoint(GameObject obj)
        {
            if (CurrentPoint == obj.transform.position)
            {
                obj.GetComponent<DestroyPoint>().OnFinishChanged -= MovePoint;
                Destroy(obj);
            }
        }
    }
}