using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class CollisionEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_Event;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            m_Event?.Invoke();
        }

    }
}
