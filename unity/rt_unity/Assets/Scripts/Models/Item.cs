using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Item : BaseModel
    {
        
        [SerializeField] public Vector3 rotation;
        [SerializeField] public int type;

        public Item(int id, Vector3 position, Vector3 rotation, int type) : base(id, position)
        {
            this.rotation = rotation;
            this.type = type;
        }
    }

    [Serializable]
    public class ItemList
    {
        [SerializeField] public List<Item> items;
    }
}