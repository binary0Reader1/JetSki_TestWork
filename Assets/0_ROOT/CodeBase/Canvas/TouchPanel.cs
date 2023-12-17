using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Canvas
{
    public class TouchPanel : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        public event Action<PointerEventData> OnDragActions;
        public event Action<PointerEventData> OnEndDragActions;
        
        public void OnDrag(PointerEventData eventData) => 
            OnDragActions?.Invoke(eventData);

        public void OnEndDrag(PointerEventData eventData) => 
            OnEndDragActions?.Invoke(eventData);
    }
}
