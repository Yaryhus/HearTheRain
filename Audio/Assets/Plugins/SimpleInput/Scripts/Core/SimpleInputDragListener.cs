﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleInputNamespace
{
	public class SimpleInputDragListener : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IEndDragHandler
	{
		public ISimpleInputDraggable Listener { get; set; }

		private int pointerId = SimpleInputUtils.NON_EXISTING_TOUCH;

		private void Awake()
		{
			Graphic graphic = GetComponent<Graphic>();
			if( graphic == null )
			{
				graphic = gameObject.AddComponent<Image>();
				graphic.color = Color.clear;
			}

			graphic.raycastTarget = true;
		}

		public void OnPointerDown( PointerEventData eventData )
		{
			Listener.OnPointerDown( eventData );
			pointerId = eventData.pointerId;
		}

		public void OnDrag( PointerEventData eventData )
		{
			if( pointerId != eventData.pointerId )
				return;

			Listener.OnDrag( eventData );
		}

		public void OnPointerUp( PointerEventData eventData )
		{
			if( pointerId != eventData.pointerId )
				return;

			Listener.OnPointerUp( eventData );
			pointerId = SimpleInputUtils.NON_EXISTING_TOUCH;
		}

		public void Stop()
		{
			pointerId = SimpleInputUtils.NON_EXISTING_TOUCH;
		}

        public void OnEndDrag(PointerEventData eventData)
        {
            pointerId = SimpleInputUtils.NON_EXISTING_TOUCH;
        }
    }
}