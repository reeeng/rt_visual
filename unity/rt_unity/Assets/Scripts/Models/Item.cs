using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Item : BaseModel
    {
        [SerializeField] public Vector3 position;
        [SerializeField] public Vector3 size;

        public Item(int id, Vector3 position, Vector3 size) : base(id)
        {
            this.position = position;
            this.size = size;
        }
    }

    [Serializable]
    public class ItemList
    {
        [SerializeField] public List<Item> items;
    }
}