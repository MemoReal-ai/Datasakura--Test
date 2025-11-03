using System;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Core;
using _Project.Scripts.Gameplay.Interfaces;
using R3;
using UnityEngine;

namespace _Project.Scripts.Service
{
    public class DeadCounter : IDeadCounterService, IDisposable
    {
        public event Action<Vector3> OnDeadAnimal;
        public ReactiveProperty<int> PredatorScore { get; set; } = new(0);
        public ReactiveProperty<int> PreyScore { get; set; } = new(0);

        private readonly List<IAnimal> _trackedAnimals = new();

        public void RegisterAnimal(IAnimal animal)
        {
            if (animal == null || _trackedAnimals.Contains(animal))
                return;

            _trackedAnimals.Add(animal);
            animal.OnDeath+= OnAnimalDeath;
        }

        private void OnAnimalDeath(IAnimal animal,Transform deadPlace)
        {
            switch (animal)
            {
                case IPredator:
                    PredatorScore.Value++;
                    break;
                case IPrey:
                    PreyScore.Value++;
                    break;
            }

            _trackedAnimals.Remove(animal);
            animal.OnDeath -= OnAnimalDeath;
            
            OnDeadAnimal?.Invoke(deadPlace.position);
        }

        public void Dispose()
        {    
            _trackedAnimals.Clear();
        }
    }
}