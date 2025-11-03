using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Factorys
{
    public class UIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly RootCanvas _uiRoot;

        public UIFactory(IInstantiator instantiator, RootCanvas uiRoot)
        {
            _instantiator = instantiator;
            _uiRoot = uiRoot;
        }

        public T CreateUIElement<T>(T prefab) where T : Component
        {
            return _instantiator.InstantiatePrefabForComponent<T>(prefab, _uiRoot.transform);
        }
    }
}