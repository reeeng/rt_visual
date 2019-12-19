using System.Collections.Generic;
using Models;
using UnityEngine;

namespace UnityTemplateProjects
{
    public interface IItemManager : IManager
    {
        void OnFetchItems(Dictionary<int, Item> newItems);
    }

    public class ItemManager : MonoBehaviour, IItemManager
    {
        public ManagerState State { get; private set; }
        [SerializeField]
        public GameObject itemPrefab;

        private readonly Dictionary<int, GameObject> _items = new Dictionary<int, GameObject>();

        public void Startup()
        {
            State = ManagerState.Active;
        }

        public void OnFetchItems(Dictionary<int, Item> newItems)
        {
            foreach (var e in newItems)
            {
                if (!_items.ContainsKey(e.Key))
                {
                    var newItemSpawned = Instantiate(itemPrefab);
                    newItemSpawned.GetComponent<Transform>().position = e.Value.position;
                    _items.Add(e.Key, newItemSpawned);
                }
            }
        }
    }
}