using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject levelMenu;
    public GameObject settingsMenu;
    public GameObject quitMenu;
    public GameObject charachter;
    public GameObject charachterMenu;

    public void openCharachterMenu()
    {
        gameObject.SetActive(false);
        charachter.gameObject.SetActive(false);
        charachterMenu.gameObject.SetActive(true);
    }
    public void play()
    {
        gameObject.SetActive(false);
        charachter.gameObject.SetActive(false);
        levelMenu.SetActive(true);
    }
    public void settings()
    {
        gameObject.SetActive(false);
        charachter.gameObject.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void openquitMenu()
    {
        gameObject.SetActive(false);
        quitMenu.gameObject.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("Oyundan çıkıldı");
    }
    public void closeQuitmenu()
    {
        quitMenu.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
