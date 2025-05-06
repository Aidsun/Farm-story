using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace aidsun.Transition
{
    public class TransitionManager : MonoBehaviour
    {
        [SceneName]
        [Tooltip("Ĭ�ϼ��س���")]
        public string startSceneName = string.Empty;
        private CanvasGroup fadeCanvasGroup;
        private bool isFade;

        private void Start()
        {
            StartCoroutine(LoadSceneSetActive(startSceneName));
            fadeCanvasGroup = FindObjectOfType<CanvasGroup>();
        }

        private void OnEnable()
        {
            EventHandler.TransitionEvent += OnTransitionEvent;
        }

        private void OnDisable()
        {
            EventHandler.TransitionEvent -= OnTransitionEvent;
        }

        private void OnTransitionEvent(string sceneTarget, Vector3 positionTarget)
        {
            if (!isFade)
                StartCoroutine(Transition(sceneTarget, positionTarget));
        }

        private IEnumerator Transition(string sceneName, Vector3 targetPosition)
        {
            // ��һ�׶Σ�0.2�뵭�뵽ȫ��
            yield return StartCoroutine(Fade(1));

            // �ڶ��׶Σ�����ȫ��1.5��
            yield return new WaitForSeconds(Settings.SceneDuration);

            // �����л�ǰ��׼��
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            yield return LoadSceneSetActive(sceneName);
            EventHandler.CallMoveToPosition(targetPosition);
            EventHandler.CallAfterSceneUnloadEvent();

            // �����׶Σ�0.2�뵭����͸��
            yield return StartCoroutine(Fade(0));
        }

        private IEnumerator LoadSceneSetActive(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newScene);
        }

        /// <summary>
        /// �Ľ���ĵ��뵭��������֧���Զ���ʱ�䣩
        /// </summary>
        /// <param name="targetAlpha">Ŀ��͸���ȣ�1=ȫ�ڣ�0=͸����</param>
        private IEnumerator Fade(float targetAlpha)
        {
            isFade = true;
            fadeCanvasGroup.blocksRaycasts = true;
            

            float initialAlpha = fadeCanvasGroup.alpha;
            float timer = 0f;

            while (timer < Settings.EnterExistDuration)
            {
                fadeCanvasGroup.alpha = Mathf.Lerp(initialAlpha, targetAlpha, timer / Settings.EnterExistDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            fadeCanvasGroup.alpha = targetAlpha; // ȷ������ֵ׼ȷ
            fadeCanvasGroup.blocksRaycasts = false;
            isFade = false;
        }
    }
}