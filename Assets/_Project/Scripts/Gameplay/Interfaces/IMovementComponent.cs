using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Gameplay.Interfaces
{
    public interface IMovementComponent
    {
        UniTaskVoid Move();
    }
}