using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using _Project.Scripts.Gameplay.ConfigScript;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Gameplay.Core
{
    public class Spawner : IInitializable, IDisposable
    {
        private readonly SpawnerConfig _config;
        private readonly AnimalFactory _animalFactory;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public Spawner(SpawnerConfig config, AnimalFactory animalFactory)
        {
            _config = config;
            _animalFactory = animalFactory;
        }

        public void Initialize()
        {
            StartSpawning();
        }

        public void Dispose()
        {
            _cts?.Dispose();
        }

        private void StartSpawning()
        {
            SpawnLoopAsync(_cts.Token).Forget();
        }


        private async UniTaskVoid SpawnLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_config.SpawnCooldown), cancellationToken: token);

                int index = Random.Range(0, _config.Animals.Count);
                SpawnAnimal(_config.Animals[index]);
            }
        }

        private void SpawnAnimal(AnimalBase prefab)
        {
            Vector3 position = GetRandomPosition();
            Quaternion rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            var instance = _animalFactory.Create(prefab, position, rotation, null);
        }

        private Vector3 GetRandomPosition()
        {
            float x = Random.Range(-10f, 10f);
            float z = Random.Range(-10f, 10f);
            float y = 0f;
            return new Vector3(x, y, z);
        }
    }
}