using System;
using System.Collections;
using UnityEngine;

namespace Actions
{
    public class SpinAction : BaseAction
    {
        private float spinAmount;

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
