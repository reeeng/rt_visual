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

        public void Startup()
        {
            State = ManagerState.Active;
        }

        public void OnFetchItems(Dictionary<int, Item> newItems)
        {
            throw new System.NotImplementedException();
        }
    }
}