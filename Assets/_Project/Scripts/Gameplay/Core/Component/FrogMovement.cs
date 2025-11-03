using System;
using System.Threading;
using _Project.Scripts.Gameplay.ConfigScript;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Service;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Core.Component
{
    [RequireComponent(typeof(Rigidbody))]
    public class FrogMovement : MonoBehaviour, IMovementComponent
    {
        private const float DURATION_PREPARE_JUMP = 0.15f;
        private const float BASE_SCALE_Y = 1f;
        private const float ANIMATOIN_SCALE_Y = 0.75f;

        private Rigidbody _rigidbody;
        private float _distanceJump;
        private float _heightJump;
        private float _cooldownJump;
        private bool _canJump = true;
        private FrogConfig _frogConfig;
        private ICameraService _cameraService;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        [Inject]
        public void Construct(FrogConfig frogConfig, ICameraService cameraService)
        {
            _cameraService = cameraService;
            _frogConfig = frogConfig;
            SetConfig();
        }

        private void SetConfig()
        {
            _distanceJump = _frogConfig.DistanceJump;
            _heightJump = _frogConfig.HeightJump;
            _cooldownJump = _frogConfig.CooldownJump;
        }

        public async UniTaskVoid Move()
        {
            if (!_canJump || !IsAlive()) return;
            _canJump = false;

            _rigidbody.linearVelocity = Vector3.zero;

            Vector3 direction = Random.insideUnitSphere;
            direction.y = 0;
            direction.Normalize();
            
            if (direction == Vector3.zero)
            {
                direction = Vector3.forward;
            }

            direction = _cameraService.ClampDirectionViewPort(direction, transform.position);

            await PrepareJump();
            
            if (!IsAlive()) return;

            Vector3 jumpForce = direction * _distanceJump + Vector3.up * _heightJump;

            if (IsAlive())
                _rigidbody.AddForce(jumpForce, ForceMode.Impulse);

            if (direction != Vector3.zero && IsAlive())
                transform.forward = direction;

            if (_cameraService.IsOutOfView(transform.position))
            {
                transform.position = _cameraService.ClampToViewport(transform.position);
                transform.forward = -transform.forward;
            }


            await UniTask.Delay(TimeSpan.FromSeconds(_cooldownJump), cancellationToken: _cancellationTokenSource.Token)
                .SuppressCancellationThrow();

            if (IsAlive())
                _canJump = true;
        }


        private bool IsAlive() => this != null && _rigidbody != null;


        private async UniTask PrepareJump()
        {
            try
            {
                var tweenStartPrepare =
                    transform.DOScaleY(ANIMATOIN_SCALE_Y, DURATION_PREPARE_JUMP).SetEase(Ease.InOutSine);
                await tweenStartPrepare.AsyncWaitForCompletion();

                var tweenEndPrepare = transform.DOScaleY(BASE_SCALE_Y, DURATION_PREPARE_JUMP).SetEase(Ease.InOutSine);
                await tweenEndPrepare.AsyncWaitForCompletion();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }
}