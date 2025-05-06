using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aidusnFarm.Inventory
{
    public class ItemManager : MonoBehaviour
    {
        public Item itemPrefab;

        private Transform itemParent;

        private void OnEnable()
        {
            EventHandler.InstantiateItemInScene += OnInstantiateItemInScene;
            EventHandler.AfterSceneUnloadEvent += OnAfterSceneLoadedEvent;
        }

        private void OnDisable()
        {
            EventHandler.InstantiateItemInScene -= OnInstantiateItemInScene;
            EventHandler.AfterSceneUnloadEvent -= OnAfterSceneLoadedEvent;
        }

        private void OnAfterSceneLoadedEvent()
        {
            itemParent = GameObject.FindWithTag("ItemParent").transform;
        }

        private void OnInstantiateItemInScene(int ID, Vector3 pos)
        {
            var item = Instantiate(itemPrefab, pos, Quaternion.identity, itemParent);
            item.itemID = ID;
        }
    }

}

