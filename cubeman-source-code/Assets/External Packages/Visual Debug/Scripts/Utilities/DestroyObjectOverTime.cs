using System;
using UnityEngine;

namespace Core.Utilities
{
    public sealed class DestroyObjectOverTime : MonoBehaviour
    {
        public Action OnDestroy;

        public float DestroyDuration { set => m_destroyDuration = value; }

        private float m_destroyDuration;
        private float m_currentDestroyDuration;

        private void Update() => DestroyTimer();

        private void DestroyTimer()
        {
            m_currentDestroyDuration += Time.deltaTime;
            if (m_currentDestroyDuration >= m_destroyDuration)
            {
                OnDestroy?.Invoke();

                Destroy(gameObject);
            }
        }
    }
}
