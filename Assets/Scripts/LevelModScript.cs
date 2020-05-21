using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModScript : MonoBehaviour
{
    public GameObject levelmod1menu;
    public GameObject levelmod2menu;
    public GameObject levelmod3menu;
    public GameObject extrasMenu;
    public GameObject bossMenu; //bu muhtemelen değişecek
    public GameObject mainMenu;
    public GameObject levelMenu;
    public void LevelMod1()
    {
        gameObject.SetActive(false);
        levelmod1menu.gameObject.SetActive(true);
    }
    public void LevelMod2()
    {
        gameObject.SetActive(false);
        levelmod2menu.gameObject.SetActive(true);
    }
    public void LevelMod3()
    {
        gameObject.SetActive(false);
        levelmod3menu.gameObject.SetActive(true);
    }
    public void Ekstra()
    {
        gameObject.SetActive(false);
        extrasMenu.gameObject.SetActive(true);
    }
    public void Boss()
    {

    }
    public void Back()
    {
        levelMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }
}
