using _Project.Scripts.Gameplay.Interfaces;
using R3;

namespace _Project.Scripts.Service
{
    public interface IDeadCounterService
    {
        void RegisterAnimal(IAnimal animal);
        ReactiveProperty<int> PredatorScore { get; set; }
        ReactiveProperty<int> PreyScore { get;  set; }
    }
}