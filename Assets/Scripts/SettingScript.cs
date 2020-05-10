using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingScript : MonoBehaviour
{
    public GameObject musicsObject;
    public GameObject menu;
    public GameObject charachter;
    public TextMeshProUGUI tMusic;

    public void music()
    {
        if (musicsObject.activeInHierarchy)
        {
            musicsObject.gameObject.SetActive(false);
            tMusic.text = "Music = Off";
        }
        else if (!musicsObject.activeInHierarchy)
        {
            musicsObject.gameObject.SetActive(true);
            tMusic.text = "Music = On";
        }
    }
    public void back()
    {
        gameObject.SetActive(false);
        charachter.gameObject.SetActive(true);
        menu.SetActive(true);
    }
}
