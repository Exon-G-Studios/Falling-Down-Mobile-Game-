using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour
{
    LanguageMode languagemode;

    public Text tPlay;
    public Text tSettings;
    public Text tQuit;
    public GameObject languageEn;
    public GameObject languageTr;
    public Text tMusic;
    public Text tBack;

    void Start()
    {
        languagemode = new LanguageMode();
        languagemode = JsonUtility.FromJson<LanguageMode>(File.ReadAllText(Application.persistentDataPath + "/language.json"));

        if (languagemode.languageNum == 0) onClickEn();
        else if (languagemode.languageNum == 1) onClickTr();
    }
    public void onClickEn()
    {
        languagemode = new LanguageMode();
        languagemode.languageNum = 0;

        string jsonData = JsonUtility.ToJson(languagemode, true);
        File.WriteAllText(Application.persistentDataPath + "/language.json", jsonData);

        tPlay.text = "PLAY";
        tSettings.text = "SETTINGS";
        tQuit.text = "QUIT";
        languageTr.SetActive(false);
        languageEn.SetActive(true);
        if (tMusic.text == "Muzik = Kapali") tMusic.text = "Music = Off";
        else if (tMusic.text == "Muzik = Acik") tMusic.text = "Music = On";
        tBack.text = "Back";
    }
    public void onClickTr()
    {
        languagemode = new LanguageMode();
        languagemode.languageNum = 1;

        string jsonData = JsonUtility.ToJson(languagemode, true);
        File.WriteAllText(Application.persistentDataPath + "/language.json", jsonData);

        tPlay.text = "OYNA";
        tSettings.text = "AYARLAR";
        tQuit.text = "CIKIS";
        languageEn.SetActive(false);
        languageTr.SetActive(true);
        if (tMusic.text == "Music = Off") tMusic.text = "Muzik = Kapali";
        else if (tMusic.text == "Music = On") tMusic.text = "Muzik = Acik";
        tBack.text = "Geri";
    }
}
