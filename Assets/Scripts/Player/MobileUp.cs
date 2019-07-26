using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FlyTheCoop.Player
{
    public class MobileUp : MonoBehaviour
    {
        public Chicken chicken;
        [SerializeField] GraphicRaycaster graphicRaycaster;

        EventSystem eventSystem;
        PointerEventData pointerEventData;

        void Start()
        {
            eventSystem = GetComponent<EventSystem>();
        }

        void Update()
        {
            CheckForTouch();
        }

        void CheckForTouch()
        {
            if(Input.touchCount > 0 )
            {
                pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = Input.GetTouch(0).position;
                List<RaycastResult> results = new List<RaycastResult>();
                graphicRaycaster.Raycast(pointerEventData,results);

                foreach (RaycastResult result in results)
                {
                    Debug.Log("Hit " + result.gameObject.name);
                }
            }
        }
    }
}
