using System;
using System.Threading;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Service;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Core.Component
{
    [RequireComponent(typeof(Rigidbody))]
    public class SnakeMoveComponent : MonoBehaviour, IMovementComponent
    {
        private float _speed;
        private float _timeToChangeDirection;
        private Rigidbody _rigidbody;
        private SnakeConfig _snakeConfig;
        private ICameraService _cameraService;
        private Vector3 _currentDirection;
        private bool _isMoving = false;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        [Inject]
        public void Construct(SnakeConfig snakeConfig, ICameraService cameraService)
        {
            _cameraService = cameraService;
            _snakeConfig = snakeConfig;
            SetConfig();
        }

        private void SetConfig()
        {
            _timeToChangeDirection = _snakeConfig.TimeToChangeDirection;
            _speed = _snakeConfig.Speed;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private Vector3 GetRandomDirection()
        {
            Vector3 randomDirection = Random.onUnitSphere;
            randomDirection.y = 0;
            randomDirection.Normalize();
            return randomDirection;
        }

        public async UniTaskVoid Move()
        {
            if (_isMoving || _rigidbody == null) return;
            _isMoving = true;

            _currentDirection = GetRandomDirection();
            float timer = 0f;

            while (_isMoving && this != null && _rigidbody != null)
            {
                Vector3 checkedDirection = _cameraService.ClampDirectionViewPort(_currentDirection, transform.position);
                if (checkedDirection != _currentDirection)
                {
                    _currentDirection = -_currentDirection; 
                }

                _rigidbody.linearVelocity = _currentDirection * _speed;
                if (_currentDirection != Vector3.zero)
                    transform.forward = _currentDirection;

                timer += Time.deltaTime;
                if (timer >= _timeToChangeDirection)
                {
                    _currentDirection = GetRandomDirection();
                    timer = 0f;
                }

                await UniTask.Yield(); 
            }

            if (_rigidbody != null)
                _rigidbody.linearVelocity = Vector3.zero;

            _isMoving = false;
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}
