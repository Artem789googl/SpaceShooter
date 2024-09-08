using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


namespace Common
{
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool m_Hold;
        public bool IsHold => m_Hold;

        public void OnPointerDown(PointerEventData eventData)
        {
           
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
        }
    }
}
