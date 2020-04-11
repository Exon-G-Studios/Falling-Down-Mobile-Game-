using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    //not: farkettim ki kodu fazla uzatmışım kafaya takma bu kod hertürlü baştan yazılacak çünkü
    bool pauseMode = false;
    LevelManager levelManager;
    int health = 5;
    int score = 0;
    public Text tHealth;
    public Text tScore;
    public GameObject GameOver;
    public GameObject home;
    public GameObject restart;
    public GameObject YouWin;
    public GameObject getHurtObject;
    public GameObject getScoreObject;
    public string levelMode;

    void Update()
    {
        Debug.Log(Time.deltaTime);

        if (health == 0)
        {
            getHurtObject.SetActive(false);
            getScoreObject.SetActive(false);
            home.SetActive(true);
            restart.SetActive(true);
            pauseMode = true;
            GameOver.SetActive(true);
            levelManager.deneme = 1;
            //levelManager.tLevel1.text = "Level1 = Kaybedildi";
        }
        if (score == 5)
        {
            getHurtObject.SetActive(false);
            getScoreObject.SetActive(false);
            home.SetActive(true);
            restart.SetActive(true);
            pauseMode = true;
            YouWin.SetActive(true);
            //levelManager.tLevel1.text = "Level1 = Kazanıldı";
        }
    }
    public void Pause() //oyunu durdurur-devam ettirir
    {
        if (!pauseMode)
        {
            Time.timeScale = 0;
            getHurtObject.SetActive(false);
            getScoreObject.SetActive(false);
            home.SetActive(true);
            restart.SetActive(true);
            pauseMode = true;

        }
        else if (pauseMode)
        {
            Time.timeScale = 1;
            getHurtObject.SetActive(true);
            getScoreObject.SetActive(true);
            home.SetActive(false);
            restart.SetActive(false);
            pauseMode = false;
        }
    }
    public void getScore() //score kazanırsın
    {
        score += 1;
        tScore.text = "Score = " + score;
    }
    public void getHurt() //can azalır ve ölümü kontrol eder
    {
        health -= 1;
        tHealth.text = "Health = " + health;
    }
    public void goHome() //menuye doner
    {
        SceneManager.LoadScene("menu");
    }
    public void restartGame() //Oyunu yeniden başlatır (kazanma veya kaybetme durumunda)
    {
        score = 0;
        health = 5;
        tScore.text = "Score = " + score;
        tHealth.text = "Health = " + health;
        getScoreObject.SetActive(true);
        getHurtObject.SetActive(true);
        GameOver.SetActive(false);
        YouWin.SetActive(false);
        pauseMode = false;
        Time.timeScale = 1;
        home.SetActive(false);
        restart.SetActive(false);
    }
    public void ResetTheLevel()
    {

    }
}
