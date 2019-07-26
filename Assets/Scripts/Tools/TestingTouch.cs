using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using FlyTheCoop.Player;

#if UNITY_ANDROID
public class TestingTouch : MonoBehaviour
{
    [SerializeField] Image upArrow;
    [SerializeField] Image rightArrow;
    [SerializeField] Image leftArrow;
    public Chicken chicken;

    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    void Start()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            pointerEventData = new PointerEventData(eventSystem);

            pointerEventData.position = Input.GetTouch(0).position; //only looking at touch number 1 \/\/\/\/\/\/

            List<RaycastResult> results = new List<RaycastResult>();

            graphicRaycaster.Raycast(pointerEventData, results);

            for(int i = 0; i < results.Count; i++)
            {
                if( results[i].gameObject.name == upArrow.name)
                {
                    Debug.Log("Hit " + results[i].gameObject.name);
                    chicken.ApplyThrust();
                }
                else
                {
                    chicken.StopApplyingThrust();
                }
                
                if(results[i].gameObject.name == leftArrow.name)
                {
                    chicken.RespondToRotateInput(1);
                }

                if(results[i].gameObject.name == rightArrow.name)
                {
                    chicken.RespondToRotateInput(2);
                }
            }
        }
    }
}
#endif