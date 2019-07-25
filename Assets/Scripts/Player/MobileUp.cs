using UnityEngine;
using UnityEngine.UI;

namespace FlyTheCoop.Player
{
    public class MobileUp : MonoBehaviour
    {
        public Chicken chicken;

        void Update()
        {
            Vector2 buttonPos = Camera.main.WorldToScreenPoint(this.transform.position);
    Debug.Log(buttonPos);
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.WorldToScreenPoint(touch.position);
    Debug.Log(touchPos);
                if(touch.position == buttonPos)
                {
                    Debug.Log(gameObject.name + " touched.");
                }
            }
        }
    }
}
