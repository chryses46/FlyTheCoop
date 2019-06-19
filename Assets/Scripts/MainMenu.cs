using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button mainMenu;
   
    public void Start()
    {
        mainMenu.onClick.AddListener(LoadMainMenu);
    }

    public void LoadMainMenu()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene(0);
    }
}
