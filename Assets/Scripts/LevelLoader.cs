using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Core
{
    public class LevelLoader : MonoBehaviour
    {
// This class is a component of GameObject "System"
        public float levelLoadDelay = 2f;
        public bool newLevelLoad = false;
    
        [SerializeField] Button newGame;
        [SerializeField] Button controls;
        [SerializeField] Button version;

        public void Start()
        {
            newGame.onClick.AddListener(LoadFirstScene);
            controls.onClick.AddListener(ShowControls);
            version.onClick.AddListener(ShowVersionInfo);

            GetVersionNumber();
        }

        void Update()
        {
            HideVersionInfoOnClickAway();
        }

        void HideVersionInfoOnClickAway()
        {
            if(Input.GetMouseButtonDown(0)){
                Debug.Log("Mouse Click!");
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast( ray,out hit, 10000))
                {
                    string name = hit.collider.transform.name;

                    if(name != "VersionInfo"){
                        
                        HideVersionInfo();
                    }
                }
            }
        }

        private static void GetVersionNumber()
        {
            GameObject versionTextGo = GameObject.Find("VersionText");
            Text versionText = versionTextGo.GetComponent<Text>();
            string versionNumber = "v." + Application.version;
            versionText.text = versionNumber;
        }

        public void ShowVersionInfo()
        {
            GameObject versionInfoGo = GameObject.Find("VersionInfo");
            string name = versionInfoGo.name;
            float yStart = versionInfoGo.GetComponent<RectTransform>().localPosition.y;

            if(yStart == 1000)
            {
                float yEnd = 0;
               StartCoroutine(MoveObject(versionInfoGo, yStart, yEnd));
                
            }
        }

        public void HideVersionInfo()
        {
            GameObject versionInfoGo = GameObject.Find("VersionInfo");
            string name = versionInfoGo.name;
            float yStart = versionInfoGo.GetComponent<RectTransform>().localPosition.y;

            if(yStart == 0)
            {
                float yEnd = 1000;
               StartCoroutine(MoveObject(versionInfoGo, yStart, yEnd));
                
            }
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

        public void LoadFirstScene() // SceneLoader
        {
            Debug.Log("waiting to load first scene");
            Debug.Log("loading first scene");
            SceneManager.LoadScene(2);
          // GetComponent<EffectsFactory>().isTransitioning = false;
        }

        public void ShowControls()
        {
            SceneManager.LoadScene(1);
        }

        public IEnumerator LoadNextScene() // SceneLoader
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex;

            yield return new WaitForSeconds(levelLoadDelay);

            if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(2);
                GetComponent<EffectsFactory>().isTransitioning = false;
            }
            else 
            {
                nextSceneIndex = currentSceneIndex + 1;
                SceneManager.LoadScene(nextSceneIndex);
                GetComponent<EffectsFactory>().isTransitioning = false; 
            }
        }  
    }
}

