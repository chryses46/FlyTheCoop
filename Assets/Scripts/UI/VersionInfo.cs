using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlyTheCoop.UI
{
    public class VersionInfo : MonoBehaviour
    {
#region Startup
        void OnEnable()
        {
            GetVersionNumber();
        }
#endregion
        public void GetVersionNumber()
        {
            GameObject versionTextGo = GameObject.Find("VersionText");
            Text versionText = versionTextGo.GetComponent<Text>();
            string versionNumber = "v." + Application.version;
            versionText.text = versionNumber;
        }
    }
}
