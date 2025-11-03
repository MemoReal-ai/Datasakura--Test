using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Interfaces
{
    public interface IAnimal
    {
        event Action<IAnimal,Transform> OnDeath;
    }
}