using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aidsun.Transition
{

    public class Teleport : MonoBehaviour
    {
        [SceneName]
        [Tooltip("传送的目标场景名")]
        public string sceneTarget;
        [Tooltip("传送的目标位置")]
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