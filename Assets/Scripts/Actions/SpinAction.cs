using System;
using System.Collections;
using UnityEngine;

namespace Actions
{
    public class SpinAction : BaseAction
    {
        private const string SpinActionName = "Spin";
        
        private float spinAmount;
        
        protected override void Awake()
        {
            base.Awake();
            
            ActionNameValidation(SpinActionName);
        }

        private void Update()
        {
            if (!isActive) return;
            
            float spinAddAmount = 360 * Time.deltaTime;
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);

            spinAmount += spinAddAmount;

            if (spinAmount >= 360f)
            {
                isActive = false;
                onActionComplete();
            }
            
        }

        public void Spin(Action onSpinComplete)
        {
            onActionComplete = onSpinComplete;
            isActive = true;
            spinAmount = 0f;
        }
    }
}
