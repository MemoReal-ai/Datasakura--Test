using _Project.Scripts.Gameplay.Core;
using _Project.Scripts.Gameplay.Interfaces;
using UnityEngine;
using Zenject;

public class AnimalFactory
{
    private IInstantiator _instantiator;

    public AnimalFactory(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public IAnimal Create(AnimalBase prefab, Vector3 position, Quaternion rotation, object o)
    {
        var instance = _instantiator.InstantiatePrefabForComponent<IAnimal>(prefab, position, rotation, null);
        return instance;
    }
}