using _Project.Scripts.Gameplay.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Core.Component
{
    public class PredatorCollisionHandleComponent : MonoBehaviour, ICollisionBehaviourComponent
    {
        public void Handle(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IAnimal _))
            {
                if (collision.gameObject.TryGetComponent(out IPredator _))
                {
                    if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }

                    return;
                }

                Destroy(collision.gameObject);
            }
        }
    }
}