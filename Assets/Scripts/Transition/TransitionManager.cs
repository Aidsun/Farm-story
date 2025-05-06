using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace aidsun.Transition
{
    public class TransitionManager : MonoBehaviour
    {
        [SceneName]
        [Tooltip("默认加载场景")]
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
            // 第一阶段：0.2秒淡入到全黑
            yield return StartCoroutine(Fade(1));

            // 第二阶段：保持全黑1.5秒
            yield return new WaitForSeconds(Settings.SceneDuration);

            // 场景切换前的准备
            EventHandler.CallBeforeSceneUnloadEvent();
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            yield return LoadSceneSetActive(sceneName);
            EventHandler.CallMoveToPosition(targetPosition);
            EventHandler.CallAfterSceneUnloadEvent();

            // 第三阶段：0.2秒淡出到透明
            yield return StartCoroutine(Fade(0));
        }

        private IEnumerator LoadSceneSetActive(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newScene);
        }

        /// <summary>
        /// 改进后的淡入淡出方法（支持自定义时间）
        /// </summary>
        /// <param name="targetAlpha">目标透明度（1=全黑，0=透明）</param>
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

            fadeCanvasGroup.alpha = targetAlpha; // 确保最终值准确
            fadeCanvasGroup.blocksRaycasts = false;
            isFade = false;
        }
    }
}