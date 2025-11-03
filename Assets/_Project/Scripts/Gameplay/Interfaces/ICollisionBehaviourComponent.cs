using UnityEngine;

namespace _Project.Scripts.Gameplay.Interfaces
{
    public interface ICollisionBehaviourComponent
    {
        void Handle(Collision collision);
    }
}