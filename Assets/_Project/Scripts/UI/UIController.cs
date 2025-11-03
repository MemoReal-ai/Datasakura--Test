using System;
using UnityEngine;
using _Project.Scripts.Infrastructure.Factorys;
using _Project.Scripts.Service;
using Zenject;

namespace _Project.Scripts.UI
{
    public class UIController : IInitializable, IDisposable
    {
        private readonly TastyView _tastyViewPrefab;
        private readonly UIFactory _uiFactory;
        private readonly Camera _mainCamera;
        private readonly DeadCounter _deadCounter;

        public UIController(TastyView tastyViewPrefab, UIFactory uiFactory, DeadCounter deadCounter, Camera mainCamera)
        {
            _tastyViewPrefab = tastyViewPrefab;
            _uiFactory = uiFactory;
            _mainCamera = mainCamera;
            _deadCounter = deadCounter;
        }

        public void Initialize()
        {
            _deadCounter.OnDeadAnimal += ShowTasty;
        }

        private void ShowTasty(Vector3 worldPosition)
        {
            if (_mainCamera == null)
            {
                return;
            }

            Vector3 screenPos = _mainCamera.WorldToScreenPoint(worldPosition);
            var tastyInstance = _uiFactory.CreateUIElement(_tastyViewPrefab);

            tastyInstance.transform.position = screenPos;
            tastyInstance.Play();
        }

        public void Dispose()
        {
            _deadCounter.OnDeadAnimal -= ShowTasty;
        }
    }
}