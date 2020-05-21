using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DynamicLevel : MonoBehaviour
{
    protected Text text_level;
    protected SANDLSystem sandlSystem;
    protected GameModeManager gameModeManager;
    protected int currentLockStatus, currentGameMode;
    public GameObject BuyLevelPopup;
    public string levelName = "enter name", SceneToLoad;
    public int levelNumber, levelPrice;

    void Start()
    {
        text_level = this.GetComponentInChildren(typeof(Text)) as Text;     //Olası LanguageManager İle Çakışma Gözlemleniyor !!! //Çakışma Var Fakat LanguageManager Öncelikli !
        text_level.text = levelName;
        sandlSystem = GameObject.Find("DataSystem").GetComponent<SANDLSystem>();
        gameModeManager = GameObject.Find("DataSystem").GetComponent<GameModeManager>();
    }

    void Update(){
        currentGameMode = gameModeManager.GetGameMode();
        sandlSystem.loadData();
        currentLockStatus = sandlSystem._levelData.levels[currentGameMode, levelNumber, 0];
    }

    public void checkStatus(){
        Debug.Log("Lock Status:" + currentLockStatus);
        Debug.Log("Game Mode:" + currentGameMode);
        gameModeManager.SetGameLevel(levelNumber);
        gameModeManager.SetLevelPrice(levelPrice);
        if(currentLockStatus != 1) { BuyLevelPopup.gameObject.SetActive(true); }

        else{ SceneManager.LoadScene(SceneToLoad); }
    }

}
