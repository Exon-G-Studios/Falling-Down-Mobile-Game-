using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    LanguageMode languagemode;

    public Text tMusics;

    public GameObject musicsObject;
    public GameObject menu;

    public void music()
    {
        languagemode = new LanguageMode();
        languagemode = JsonUtility.FromJson<LanguageMode>(File.ReadAllText(Application.persistentDataPath + "/language.json"));

        if (musicsObject.activeInHierarchy)
        {
            musicsObject.SetActive(false);

            if (languagemode.languageNum == 0) tMusics.text = "Music = Off";
            else if (languagemode.languageNum == 1) tMusics.text = "Muzik = Kapali";
        }
        else if (!musicsObject.activeInHierarchy)
        {
            musicsObject.SetActive(true);

            if (languagemode.languageNum == 0) tMusics.text = "Music = On";
            else if (languagemode.languageNum == 1) tMusics.text = "Muzik = Acik";
        }
    }
    public void back()
    {
        gameObject.SetActive(false);
        menu.SetActive(true);
    }
}
