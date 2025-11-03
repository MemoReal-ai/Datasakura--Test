using System;
using _Project.Scripts.Gameplay.Interfaces;
using _Project.Scripts.Service;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Core
{
    public abstract class AnimalBase : MonoBehaviour, IAnimal
    {
        public event Action<IAnimal,Transform> OnDeath;
        
        private IMovementComponent _movementComponent;
        private ICollisionBehaviourComponent _collisionBehaviourComponent;
        private IDeadCounterService _deadCounterService;
        
        [Inject]
        public void Construct(IDeadCounterService deadCounterService)
        {
            _deadCounterService = deadCounterService;
        }

        private void Start()
        {
            _movementComponent = GetComponent<IMovementComponent>();
            _collisionBehaviourComponent = GetComponent<ICollisionBehaviourComponent>();
            if (_movementComponent == null || _collisionBehaviourComponent == null)
            {
                Debug.LogWarning($"{gameObject.name} is missing required components.");
            }
            _deadCounterService.RegisterAnimal(this);
        }

        private void Update()
        {
            _movementComponent.Move();
        }

        private void OnCollisionEnter(Collision collision)
        {
            _collisionBehaviourComponent.Handle(collision);
        }

        private void OnDestroy()
        {
            OnDeath?.Invoke(this,gameObject.transform);
        }
    }
}