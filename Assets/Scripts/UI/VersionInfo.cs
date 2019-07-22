using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FlyTheCoop.UI
{
    public class VersionInfo : MonoBehaviour
    {
#region PublicProperties
        [SerializeField] GameObject versionInfo;
        [SerializeField] Button version;
        [SerializeField] Text versionText;
#endregion
#region PrivateProperties
        bool shown;
#endregion

#region Startup
        void OnEnable()
        {
            version.onClick.AddListener(ShowVersionInfo);
            GetVersionNumber();
        }
        void Update()
        {
            HideVersionInfoOnClickAway();
        }
#endregion
#region VersionInfo
        void HideVersionInfoOnClickAway()
        {
            if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
            {
                if(shown)
                {
                    HideVersionInfo();
                }
            }

        }
        private void ShowVersionInfo()
        {
            float yStart = versionInfo.GetComponent<RectTransform>().localPosition.y;

            if(yStart == 1000)
            {
                float yEnd = 0;
                StartCoroutine(MoveObject(versionInfo, yStart, yEnd));
                
            }

            shown = true;
        }
        private void HideVersionInfo()
        {
            float yStart = versionInfo.GetComponent<RectTransform>().localPosition.y;

            if(yStart == 0)
            {
                float yEnd = 1000;
                StartCoroutine(MoveObject(versionInfo, yStart, yEnd));
                
            }

            shown = false;
        }
        IEnumerator MoveObject(GameObject go, float start, float end)
        {
            RectTransform goRect = go.GetComponent<RectTransform>();
            float t = 0.0f;

            while(t < 1)
            {
                t += Time.deltaTime / .5f;
                goRect.localPosition = new Vector2(0,Mathf.Lerp(start,end,t));
                yield return null;
            }
        }
        public void GetVersionNumber()
        {
            string versionNumber = "v." + Application.version;
            versionText.text = versionNumber;
        }
#endregion
    }
}
