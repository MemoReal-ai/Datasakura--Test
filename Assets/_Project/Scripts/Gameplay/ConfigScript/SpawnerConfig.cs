using System.Collections.Generic;
using _Project.Scripts.Gameplay.Core;
using UnityEngine;

namespace _Project.Scripts.Gameplay.ConfigScript
{
    [CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Configs/SpawnerConfig", order = 0)]
    public class SpawnerConfig : ScriptableObject
    {
        [field: SerializeField]
        public float SpawnCooldown { get; private set; }

        [field: SerializeField]
        public List<AnimalBase> Animals { get; private set; }
    }
}