using System;
using _Project.Scripts.Service;
using R3;
using Zenject;

namespace _Project.Scripts.UI
{
    public class ScoreViewModel : IInitializable, IDisposable
    {
        private readonly IDeadCounterService _deadCounterService;
        private readonly ScoreView _scoreView;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public ScoreViewModel(IDeadCounterService deadCounterService, ScoreView scoreView)
        {
            _deadCounterService = deadCounterService;
            _scoreView = scoreView;
        }

        public void Initialize()
        {
            _deadCounterService.PredatorScore
                .Subscribe(score =>
                {
                    _scoreView.ScorePredatorText.text = score.ToString();
                })
                .AddTo(_disposables);

            _deadCounterService.PreyScore
                .Subscribe(score =>
                {
                    _scoreView.ScorePreyText.text = score.ToString();
                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}