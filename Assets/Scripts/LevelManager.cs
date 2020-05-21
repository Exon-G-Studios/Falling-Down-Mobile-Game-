using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LevelManager : MonoBehaviour
{
    //Level sahneleri içerisinde gerçekleşecek olayları kontrol eden script.

    public static playerDataInUnity playerCoinData;

    public int coin;
    public int score = 0;
    public int winScore;
    public int heal;
    public Text tScore;
    public Text tHeal;
    public Text tCoin;
    public GameObject GameOver;
    public GameObject coinObject;
    string sceneName;

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        coin = playerCoinData.coinData;
    }
    public void ScoreArttır()
    {
        score += 10;
        tScore.text = "Score = " + score;
        if (score == winScore)
        {
            //dentifyTheLevelWin();

            coin += 100;

            tCoin.text = "Coin = " + coin;

            GameOver.gameObject.SetActive(true);
            coinObject.gameObject.SetActive(true);
        }
    }
    public void HealAzalt()
    {
        heal -= 1;
        tHeal.text = "Heal = " + heal;
        if (heal == 0)
        {
            //dentifyTheLevelLost();

            coin += 50;
            
            tCoin.text = "Coin = " + coin;

            GameOver.gameObject.SetActive(true);
            coinObject.gameObject.SetActive(true);
        }
    }
    //void dentifyTheLevelWin()
    //{
    //    if (sceneName == "Level1") levelData.Level1 = 2;
    //    else if (sceneName == "Level2") levelData.Level2 = 2;
    //    else if (sceneName == "Level3") levelData.Level3 = 2;
    //}
    //void dentifyTheLevelLost()
    //{
    //    if (sceneName == "Level1") levelData.Level1 = 3;
    //    else if (sceneName == "Level2") levelData.Level2 = 3;
    //    else if (sceneName == "Level3") levelData.Level3 = 3;
    //}
    public void backToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
