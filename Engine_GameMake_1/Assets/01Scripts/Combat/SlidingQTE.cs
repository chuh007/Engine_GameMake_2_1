using System.Collections;
using _01Scripts.Entities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

namespace QTESystem
{
    public class SlidingQTE : MonoBehaviour, IEntityComponent
    {
        public enum QTEState { Inactive, Active, Success, Failed }

        [Header("Base QTE Settings")]
        [Tooltip("How long to wait until the QTE starts")]
        [SerializeField] private float delay = 1f;

        [Tooltip("After starting, how long should the QTE last")]
        [SerializeField] private float time = 3f;

        [Tooltip("Should the QTE start immediately?")]
        [SerializeField] private bool startOnAwake = true;

        [Header("Sliding QTE Settings")]
        [Tooltip("How many times to slide back and forth")]
        [SerializeField] private int numberOfTries = 5;

        [Tooltip("Show green indicator on success zone")]
        [SerializeField] private bool showSuccess = true;

        [Header("References")]
        [SerializeField] private GameObject slidingQTECanvas;
        [SerializeField] private Image input;
        [SerializeField] private RectTransform failure;
        [SerializeField] private Image success;
        [SerializeField] private TextMeshProUGUI text;

        [Header("Sprites")]
        [SerializeField] private Sprite successSprite;
        [SerializeField] private Sprite failureSprite;

        [Header("Events")]
        public UnityEvent onSuccess;
        public UnityEvent onFailure;

        public delegate void QTEActionEvent(float time);
        public delegate void QTEEvent();

        public event QTEActionEvent startEvent;
        public event QTEEvent cleanupEvent;
        public event QTEEvent resetEvent;

        private float minSuccessX;
        private float maxSuccessX;

        private QTEState state = QTEState.Inactive;
        private Tweener movementTween;
        private Coroutine qteCoroutine;

        private void OnEnable()
        {
            StopAllCoroutines();
            DOTween.Kill(input.rectTransform);

            input.color = Color.white;
            UpdateAlpha(success, 1f);
            UpdateAlpha(failure.GetComponent<Image>(), 1f);
            state = QTEState.Inactive;

            if (startOnAwake)
                BeginQTE();
        }
        
        
        public void Initialize(Entity entity)
        {
            
        }

        public void BeginQTE()
        {
            slidingQTECanvas.SetActive(true);

            if (qteCoroutine != null)
                StopCoroutine(qteCoroutine);

            qteCoroutine = StartCoroutine(QTEAction());
        }

        private IEnumerator QTEAction()
        {
            state = QTEState.Active;

            Vector3 startPos = failure.localPosition - new Vector3(failure.rect.width * failure.localScale.x / 2f, 0, 0);
            input.rectTransform.localPosition = startPos;

            float halfWidth = success.rectTransform.rect.width * success.rectTransform.lossyScale.x / 2f;
            float successX = success.rectTransform.anchoredPosition.x;
            minSuccessX = successX - halfWidth;
            maxSuccessX = successX + halfWidth;

            yield return new WaitForSeconds(delay);

            if (state != QTEState.Active)
                yield break;

            OnStartEvent();

            Vector2 origin = input.rectTransform.anchoredPosition;
            Vector2 target = origin;
            target.x = failure.anchoredPosition.x + (failure.rect.width / 2f);

            float singleMoveTime = time / numberOfTries / 2f;

            movementTween = input.rectTransform.DOAnchorPosX(target.x, singleMoveTime)
                .SetLoops(numberOfTries * 2, LoopType.Yoyo)
                .SetEase(Ease.Linear);

            yield return new WaitForSeconds(time);

            QTEFailure();
        }

        private void Update()
        {
            if (state != QTEState.Active) return;

            float xPos = input.rectTransform.anchoredPosition.x;

            if (showSuccess && InsideSuccess(xPos))
                input.color = Color.white;
            else
                input.color = Color.white;
        }

        public void HandleQTEPressed()
        {
            if (state != QTEState.Active) return;

            float xPos = input.rectTransform.anchoredPosition.x;

            if (InsideSuccess(xPos))
                QTESuccess();
            else
                QTEFailure();
        }

        private bool InsideSuccess(float xPos)
        {
            return xPos >= minSuccessX && xPos <= maxSuccessX;
        }

        private void QTESuccess()
        {
            state = QTEState.Success;
            movementTween?.Kill();
            if (input) input.sprite = successSprite;
            text.text = "Success";
            CleanUp();
            slidingQTECanvas.SetActive(false);
            onSuccess.Invoke();
        }

        private void QTEFailure()
        {
            state = QTEState.Failed;
            movementTween?.Kill();
            if (input) input.sprite = failureSprite;
            text.text = "Fail";
            CleanUp();
            slidingQTECanvas.SetActive(false);
            onFailure.Invoke();
        }

        private void CleanUp()
        {
            StopAllCoroutines();
            DOTween.Kill(input.rectTransform);

            OnCleanUpEvent();

            DOTween.To(() => 1f, x => UpdateAlpha(input, x), 0f, 0.25f)
                .SetDelay(0.5f);
        }

        public void ResetQTE()
        {
            movementTween?.Kill();
            input.color = Color.white;
            UpdateAlpha(success, 1f);
            UpdateAlpha(failure.GetComponent<Image>(), 1f);
            OnResetEvent();
            OnEnable(); // 리셋 시 자동 재설정
        }

        private void UpdateAlpha(Image img, float val)
        {
            if (!img) return;
            var c = img.color;
            c.a = val;
            img.color = c;
        }

        private void OnStartEvent()
        {
            startEvent?.Invoke(time);
        }

        private void OnCleanUpEvent()
        {
            cleanupEvent?.Invoke();
        }

        private void OnResetEvent()
        {
            resetEvent?.Invoke();
        }

    }
}
