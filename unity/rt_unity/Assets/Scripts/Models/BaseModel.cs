using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public abstract class BaseModel
    {
        protected BaseModel(int id)
        {
            Id = id;
        }

        [SerializeField] public int? Id;
    }
}