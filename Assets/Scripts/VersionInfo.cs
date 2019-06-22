using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionInfo : MonoBehaviour
{
    void OnEnable()
    {
        GetVersionNumber();
    }

    public void GetVersionNumber()
    {
        GameObject versionTextGo = GameObject.Find("VersionText");
        Text versionText = versionTextGo.GetComponent<Text>();
        string versionNumber = "v." + Application.version;
        versionText.text = versionNumber;
    }
}
