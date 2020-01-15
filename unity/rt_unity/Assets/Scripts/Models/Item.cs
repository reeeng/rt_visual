using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Item : BaseModel
    {
        
        [SerializeField] public Vector3 rotation;

        public Item(int id, Vector3 position, Vector3 rotation) : base(id, position)
        {
            this.rotation = rotation;
        }
    }

    [Serializable]
    public class ItemList
    {
        [SerializeField] public List<Item> items;
    }
}