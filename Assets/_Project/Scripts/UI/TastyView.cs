using TMPro;
using UnityEngine;
using DG.Tweening;

namespace _Project.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TastyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tastyText;
        [SerializeField] private float _duration = 1.2f;
        [SerializeField] private float _jumpPower = 50f;
        [SerializeField] private float _scaleUp = 1.4f;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

        public void Play()
        {   
            transform.localScale = Vector3.zero;
            _canvasGroup.alpha = 0;

            Sequence seq = DOTween.Sequence();

            seq.Append(_canvasGroup.DOFade(1f, 0.15f));
            seq.Join(transform.DOScale(_scaleUp, 0.25f).SetEase(Ease.OutBack));
            seq.Append(transform.DOLocalMoveY(transform.localPosition.y + _jumpPower, _duration / 2)
                .SetEase(Ease.OutQuad));
            seq.AppendInterval(0.3f);
            seq.Append(_canvasGroup.DOFade(0f, 0.5f));
            seq.Join(transform.DOScale(0f, 0.5f).SetEase(Ease.InBack));

            seq.OnComplete(() => Destroy(gameObject));
        }
    }
}