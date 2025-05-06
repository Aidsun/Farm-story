using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aidsun.Transition
{

    public class Teleport : MonoBehaviour
    {
        [SceneName]
        [Tooltip("���͵�Ŀ�곡����")]
        public string sceneTarget;
        [Tooltip("���͵�Ŀ��λ��")]
        public Vector3 positionTarget;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                EventHandler.CallTransitionEvent(sceneTarget,positionTarget);
            }   
        }
    }

}