using _Project.Scripts.Gameplay.ConfigScript;
using _Project.Scripts.Gameplay.Core;
using _Project.Scripts.Infrastructure.Factorys;
using _Project.Scripts.Service;
using _Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Infrastructure.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private FrogConfig _frogConfig;
        [SerializeField] private SnakeConfig _snakeConfig;
        [SerializeField] private SpawnerConfig _spawnerConfig;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private TastyView _tastyViewPrefab;
        [SerializeField] private RootCanvas _rootCanvas;

        public override void InstallBindings()
        {
            BindCoreServices();
            BindConfigs();
            BindFactories();
            BindUI();
        }

        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<ScoreViewModel>().AsSingle().NonLazy();
            Container.Bind<ScoreView>().FromComponentInNewPrefab(_scoreView).AsSingle();
            Container.Bind<TastyView>().FromInstance(_tastyViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<UIController>().AsSingle().NonLazy();
            Container.Bind<RootCanvas>().FromComponentInNewPrefab(_rootCanvas).AsSingle();
        }

        private void BindConfigs()
        {
            Container.Bind<FrogConfig>().FromInstance(_frogConfig).AsSingle();
            Container.Bind<SnakeConfig>().FromInstance(_snakeConfig).AsSingle();
            Container.Bind<SpawnerConfig>().FromInstance(_spawnerConfig).AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<AnimalFactory>().AsSingle();
            Container.Bind<UIFactory>().AsSingle();
        }

        private void BindCoreServices()
        {
            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
            Container.BindInterfacesAndSelfTo<CameraService>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeadCounter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Spawner>().AsSingle().NonLazy();
        }
    }
}