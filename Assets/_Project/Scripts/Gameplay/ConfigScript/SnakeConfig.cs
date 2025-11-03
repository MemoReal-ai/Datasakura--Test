using UnityEngine;

[CreateAssetMenu(fileName = "SnakeConfig", menuName = "Configs/SnakeConfig", order = 1)]
public class SnakeConfig : ScriptableObject
{
    [field: SerializeField]
    public float Speed { get; private set; }

    [field: SerializeField]
    public float TimeToChangeDirection { get; private set; }
}