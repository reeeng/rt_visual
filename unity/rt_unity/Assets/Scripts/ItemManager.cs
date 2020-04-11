using System.Collections.Generic;
using System.Linq;
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
        public List<GameObject> itemPrefabs;

        private Dictionary<int, GameObject> itemPrefabDict = new Dictionary<int, GameObject>();

        private readonly Dictionary<int, GameObject> _items = new Dictionary<int, GameObject>();

        public void Startup()
        {
            State = ManagerState.Active;
            
            foreach(var item in itemPrefabs)
            {
                var itemPrefabComp = item.GetComponent<ItemPrefab>();
                itemPrefabDict.Add(itemPrefabComp.Id, item);
            }
        }

        public void OnFetchItems(Dictionary<int, Item> newItems)
        {
            var itemsInResp = new int[]{};

            foreach (var e in newItems)
            {
                itemsInResp.Append(e.Key);

                if (!_items.ContainsKey(e.Key))
                {
                    var itemType = e.Value.type;
                    var newItemSpawned = Instantiate(itemPrefabDict[itemType]);
                    _items.Add(e.Key, newItemSpawned);
                }

                _items[e.Key].GetComponent<Transform>().position = e.Value.position;
                _items[e.Key].GetComponent<Transform>().eulerAngles = e.Value.rotation;
            }

            var itemsToRemove = _items.Keys.Where(key => !itemsInResp.Contains(key));

            foreach (var key in itemsToRemove)
            {
                Destroy(_items[key]);
                _items.Remove(key);
            }
        }
    }
}