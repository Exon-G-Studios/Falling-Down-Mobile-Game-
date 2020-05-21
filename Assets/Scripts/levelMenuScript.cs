using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class levelMenuScript : MonoBehaviour
{
    //Menu sahnesi içinde bulunan; levelMenu script.

    public static playerDataInUnity playerData;
    coinScript coinScript;

    public GameObject menu;
    public GameObject characther;
    public GameObject buyLevelMenu2;
    public GameObject buyLevelMenu3;
    public Text tBuyLevelMenu2;
    public Text tBuyLevelMenu3;

    public void level1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void level2()
    {
        if (playerData.level2 == 1)
        {
            buyLevelMenu2.gameObject.SetActive(true);
            tBuyLevelMenu2.text = "Level2 için şu kadar para gidecek = 100";
        }
        else
        {
            SceneManager.LoadScene("Level2");
        }
    }
    public void level3()
    {
        if (playerData.level3 == 1)
        {
            buyLevelMenu3.gameObject.SetActive(true);
            tBuyLevelMenu3.text = "Level3 için şu kadar para gidecek = 300";
        }
        else
        {
            SceneManager.LoadScene("Level3");
        }
    }
    public void yepBuyTheLevel2()
    {
        if (coinScript.coin >= 100)
        {
            playerData.level2 = 0;

            coinScript.coin -= 100;
        }
        else if (coinScript.coin < 100)
        {
            Debug.Log("Yeterli paranız yok");
            buyLevelMenu2.gameObject.SetActive(false);
        }
    }
    public void yepBuyTheLevel3()
    {
        if (coinScript.coin >= 300)
        {
            playerData.level3 = 0;

            coinScript.coin -= 300;
        }
        else if (coinScript.coin < 300)
        {
            Debug.Log("Yeterli paranız yok");
            buyLevelMenu2.gameObject.SetActive(false);
        }
    }
    public void nopeDontBuyTheLevel()
    {
        buyLevelMenu2.gameObject.SetActive(false);
        buyLevelMenu3.gameObject.SetActive(false);
    }
    public void backToMenu()
    {
        gameObject.SetActive(false);
        characther.gameObject.SetActive(true);
        menu.gameObject.SetActive(true);
    }
}
