using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject menu;

    public Text tLevel1;
    public Text tLevel2;
    public Text tLevel3;

    public int deneme = 0;
    
    void OnEnable()
    {
        Debug.Log(deneme);
    }
    public void onClickLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void onClickLevel2()
    {
        Debug.Log("sa");
    }
    public void onClickLevel3()
    {
        Debug.Log("as");
    }
    public void onClickBack()
    {
        gameObject.SetActive(false);
        menu.SetActive(true);
    }
}