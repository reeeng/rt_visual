using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public abstract class BaseModel
    {
        protected BaseModel(int id)
        {
            id = id;
        }

        [SerializeField] public int? id;
    }
}