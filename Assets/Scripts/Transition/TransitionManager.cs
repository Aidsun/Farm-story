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
        [Tooltip("Ĭ�ϼ��س���")]
        public string startSceneName = string.Empty;

        private void Start()
        {
            StartCoroutine(LoadSceneSetActive(startSceneName));
        }
        //ע���¼�
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
        /// ж�ص�ǰ��������л���Ŀ�곡�������Ҵ��͵�Ŀ��λ��
        /// </summary>
        /// <param name="Ŀ�곡��"></param>
        /// <param name="Ŀ��λ��"></param>
        /// <returns></returns>
        private IEnumerator Transition(string sceneName, Vector3 targetPosition)
        {
            //�л�����֮ǰ�������ݱ���
            EventHandler.CallBeforeSceneUnloadEvent();
            //�첽ж�ص�ǰ�����
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            //�첽����Ŀ�곡��
            yield return LoadSceneSetActive(sceneName);
            //�ƶ���������
            EventHandler.CallMoveToPosition(targetPosition);
            //�л�����֮��������ݼ���
            EventHandler.CallAfterSceneUnloadEvent();
        }

        /// <summary>
        /// ���س���������Ϊ����״̬
        /// </summary>
        /// <param name="������"></param>
        /// <returns></returns>
        private IEnumerator LoadSceneSetActive(string sceneName)
        {
            //�첽����
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            //��ȡ�����б�
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            //�����
            SceneManager.SetActiveScene(newScene);
        }
    }
}