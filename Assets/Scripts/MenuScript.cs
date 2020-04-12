using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject levelMenu;
    public GameObject settingsMenu;
    public void play()
    {
        gameObject.SetActive(false);
        levelMenu.SetActive(true);
    }
    public void settings()
    {
        gameObject.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("Oyundan cikildi.");
    }
}
