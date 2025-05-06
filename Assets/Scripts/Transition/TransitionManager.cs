using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace aidsun.Transition
{
    public class TransitionManager : MonoBehaviour
    {
        [Tooltip("默认加载场景")]
        public string startSceneName = string.Empty;

        private void Start()
        {
            StartCoroutine(LoadSceneSetActive(startSceneName));
        }
        //注册事件
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
            StartCoroutine(Transition(sceneTarget, positionTarget));
        }

        /// <summary>
        /// 卸载当前激活场景，切换到目标场景，并且传送到目标位置
        /// </summary>
        /// <param name="目标场景"></param>
        /// <param name="目标位置"></param>
        /// <returns></returns>
        private IEnumerator Transition(string sceneName, Vector3 targetPosition)
        {
            //切换场景之前进行数据保存
            EventHandler.CallBeforeSceneUnloadEvent();
            //异步卸载当前激活场景
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            //异步加载目标场景
            yield return LoadSceneSetActive(sceneName);
            //移动人物坐标
            EventHandler.CallMoveToPosition(targetPosition);
            //切换场景之后进行数据加载
            EventHandler.CallAfterSceneUnloadEvent();
        }

        /// <summary>
        /// 加载场景并设置为激活状态
        /// </summary>
        /// <param name="场景名"></param>
        /// <returns></returns>
        private IEnumerator LoadSceneSetActive(string sceneName)
        {
            //异步加载
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            //获取场景列表
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            //激活场景
            SceneManager.SetActiveScene(newScene);
        }
    }
}