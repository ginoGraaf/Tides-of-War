using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject startScreen,loadAre;
    [SerializeField]
    SaveLoaderController loaderController;
    public void StartGame()
    {
        startScreen.SetActive(false);
        loadAre.SetActive(true);
        loaderController.ShowsOnScreen();
    }

    public void LoadMapMaker()
    {
        Application.LoadLevel(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
