using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlyTheCoop
{
    public class MoveChicken : MonoBehaviour
    {
        
        float start;
        float end;
        GameObject me;

        void Awake()
        {
            Transform parent = gameObject.transform.parent;
            Conveyor conveyor = parent.gameObject.GetComponent<Conveyor>();
            me = gameObject;
        }

        public void StartMoving(GameObject go, float start, float end)
        {
            StartCoroutine(Move(go, start, end));
        }

        IEnumerator Move(GameObject go, float start, float end)
        {
            float t = 0;
            var goY = go.transform.localPosition.y;
            var top = goY + 1.1;
            var bot = goY;
            bool topReached = false;
            bool botReached = false;
            bool movingUp = true;
            bool movingDown = false;

            while(t < 1)
            {
                if(goY >= top)
                {
                    movingDown = true;
                    topReached = true;
                    movingUp = false;

                }else if(goY <= bot)
                {
                    movingUp = true;
                    botReached = true;
                    movingDown = false;
                    
                }
                if(movingUp & !topReached)
                {
                    botReached = false;
                    goY += 1f;
        
                }else if(movingDown & !botReached)
                {
                    topReached = false;
                    goY -= 1f;
                }
                t += .1f;

                var goX = go.transform.localPosition.x;
                
                go.transform.localPosition = new Vector3(goX,goY,Mathf.Lerp(start,end,t));
                yield return new WaitForSeconds(.2f);
               
            }

            Destroy(me);
        }

    } 
}

