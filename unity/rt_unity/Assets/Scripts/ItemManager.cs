using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;

namespace UnityTemplateProjects
{
    public interface IItemManager : IManager
    {
        void OnFetchItems(Dictionary<int, Item> newItems);
        Vector3 AnchorPoint { get; set; }
    }

    public class ItemManager : MonoBehaviour, IItemManager
    {
        public ManagerState State { get; private set; }
        [SerializeField] public List<GameObject> itemPrefabs;

        private Dictionary<int, GameObject> itemPrefabDict = new Dictionary<int, GameObject>();

        private readonly Dictionary<int, GameObject> _items = new Dictionary<int, GameObject>();

        public void Startup()
        {
            State = ManagerState.Active;

            foreach (var item in itemPrefabs)
            {
                var itemPrefabComp = item.GetComponent<ItemPrefab>();
                itemPrefabDict.Add(itemPrefabComp.Id, item);
            }
        }

        public void OnFetchItems(Dictionary<int, Item> newItems)
        {
            var itemsInResp = new List<int>();

            foreach (var e in newItems)
            {
                itemsInResp.Add(e.Key);

                if (!_items.ContainsKey(e.Key))
                {
                    var itemType = e.Value.type;
                    var newItemSpawned = Instantiate(itemPrefabDict[itemType]);
                    _items.Add(e.Key, newItemSpawned);

                    Debug.Log(("Spawned item"));
                }
                else
                {
                    // Destroy and replace object with correct object of type
                    if (e.Value.type != _items[e.Key].GetComponent<ItemPrefab>().Id)
                    {
                        ReplaceItem(e.Key, itemPrefabDict[e.Value.type]);
                    }
                }

                _items[e.Key].GetComponent<Transform>().position = e.Value.position + AnchorPoint;
                _items[e.Key].GetComponent<Transform>().eulerAngles = e.Value.rotation;
            }


            // Remove removed items
            var itemsToRemove = _items.Keys.Where(key => !itemsInResp.Contains(key)).ToList();

            foreach (var key in itemsToRemove)
            {
                RemoveItem(key);
            }
        }

        private void ReplaceItem(int key, GameObject prefab)
        {
            var position = _items[key].transform.position;
            var rotation = _items[key].transform.rotation;

            RemoveItem(key);
            _items.Add(key, Instantiate(prefab, position, rotation));
        }

        private void RemoveItem(int key)
        {
            Destroy(_items[key]);
            _items.Remove(key);
        }

        public Vector3 AnchorPoint { get; set; } = Vector3.zero;
    }
}