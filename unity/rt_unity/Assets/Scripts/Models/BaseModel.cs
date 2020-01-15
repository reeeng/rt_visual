using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public abstract class BaseModel
    {
        protected BaseModel(int id, Vector3 position)
        {
            this.id = id;
            this.position = position;
        }

        [SerializeField] public int id;
        [SerializeField] public Vector3 position;
    }
}