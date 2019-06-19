using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusMonitor : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject statPercent;
    float wait;
    
    void Start()
    {
        statPercent = this.gameObject;

        StartCoroutine(IncreasePercet());
        wait = Random.Range(.25f, 1);
        

    }

    IEnumerator IncreasePercet()
    {
        decimal p = 0.0m;
        

        while(p < 99.9m)
        {   
            yield return new WaitForSeconds(wait);
            decimal increase = Random.Range(1, 10);
            p += increase;
            
            statPercent.GetComponent<Text>().text = p + "%";
        }

        
    }

    
}
