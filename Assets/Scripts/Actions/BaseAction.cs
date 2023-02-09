using System;
using UnityEngine;

namespace Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected bool isActive;
        protected Unit unit;
        protected Action onActionComplete;

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
        }
    }
}
