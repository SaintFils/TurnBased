using System;
using UnityEngine;

namespace Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        [SerializeField] protected string actionName;
        
        protected bool isActive;
        protected Unit unit;
        protected Action onActionComplete;

        public string ActionName => actionName;

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
        }

        protected void ActionNameValidation(string nameText)
        {
            if (string.IsNullOrEmpty(actionName))
            {
                actionName = nameText;
            }
        }

        //public abstract string GetActionName();
    }
}
