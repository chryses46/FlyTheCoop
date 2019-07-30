using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using FlyTheCoop.Player;

/*
This script controls mobile input for Fly The Coop. Currently, mobile plans have been suspended.
This was decided after determining that the graphical performance for the game is far overreaching for a mobile platform.

For example, Unity recommends keeping Vertices count lower than 10,000 per 30 frames on mobile devices.
Currently, the first level of Fly The Coop renders 1,095,047 verticies every 30 frames.

Development for mobile will continue once the game is at a place where performance is no longer a detriment.
*/


#if UNITY_ANDROID
public class TouchManager : MonoBehaviour
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
        {//only looking at touch number 1 \/\/\/\/\/\/
    
            for(int i = 0; i < Input.touchCount; i++)
            {
                pointerEventData = new PointerEventData(eventSystem);

                pointerEventData.position = Input.GetTouch(i).position; 

                List<RaycastResult> results = new List<RaycastResult>();

                graphicRaycaster.Raycast(pointerEventData, results);

                for(int j = 0; j < results.Count; j++)
                {
                    if( results[j].gameObject.name == upArrow.name)
                    {
                        Debug.Log("Hit " + results[j].gameObject.name);
                        chicken.ApplyThrust();
                    }
                    else
                    {
                        chicken.StopApplyingThrust();
                    }

                    if(results[j].gameObject.name == leftArrow.name)
                    {
                        chicken.RespondToRotateInput(1);
                    }

                    if(results[j].gameObject.name == rightArrow.name)
                    {
                        chicken.RespondToRotateInput(2);
                    }
                }

            }
            
        }
    }
}
#endif