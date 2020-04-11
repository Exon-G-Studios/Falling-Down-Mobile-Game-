using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject levelObject;
    public GameObject settingsObject;
    public void play()
    {
        gameObject.SetActive(false);
        levelObject.SetActive(true);
    }
    public void settings()
    {
        gameObject.SetActive(false);
        settingsObject.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("Oyundan cikildi.");
    }
}
