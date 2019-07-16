using System.Collections;
using UnityEngine;

namespace FlyTheCoop.Aesthetics
{
    public class Conveyor : MonoBehaviour
    {
        public GameObject capturedChicken;
        public GameObject startPos;
        public GameObject endPos;

        private string myName;

        void Start()
        {
            myName = this.gameObject.name;
            StartCoroutine(MakeChicken());
        }

        IEnumerator MakeChicken()
        {
            for(int i=0; i<10; i++)
            {
                var chick = Instantiate(capturedChicken, startPos.transform.localPosition, Quaternion.identity, gameObject.transform);
                chick.name = myName + "_chick_" + i;
                MoveChicken mc = GameObject.Find(chick.name).GetComponent<MoveChicken>();
                mc.StartMoving(chick, startPos.transform.localPosition.z, endPos.transform.localPosition.z);
                yield return new WaitForSeconds(.5f);
            }
        }

    }
}
