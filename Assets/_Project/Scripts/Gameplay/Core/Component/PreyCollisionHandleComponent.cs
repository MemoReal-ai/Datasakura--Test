using _Project.Scripts.Gameplay.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Core
{
    public class PreyCollisionHandleComponent : MonoBehaviour, ICollisionBehaviourComponent
    {
        [SerializeField] private float _bounceForce = 5f;

        public void Handle(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IPrey prey))
            {
                Rigidbody mainCollision = GetComponent<Rigidbody>();
                Rigidbody preyCollision = collision.gameObject.GetComponent<Rigidbody>();

                Vector3 direction = (preyCollision.position - mainCollision.position).normalized;
                preyCollision.AddForce(direction * _bounceForce, ForceMode.Impulse);
                mainCollision.AddForce(-direction * _bounceForce, ForceMode.Impulse);
            }
        }
    }
}