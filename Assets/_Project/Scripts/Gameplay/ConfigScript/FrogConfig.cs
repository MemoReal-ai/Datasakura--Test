using UnityEngine;

namespace _Project.Scripts.Gameplay.ConfigScript
{
    [CreateAssetMenu(fileName = "FrogConfig", menuName = "Configs/FrogConfig", order = 0)]
    public class FrogConfig : ScriptableObject
    {
        [field: SerializeField]
        public float CooldownJump { get; private set; }

        [field: SerializeField]
        public float HeightJump { get; private set; }

        [field: SerializeField]
        public float DistanceJump { get; private set; }
    }
}