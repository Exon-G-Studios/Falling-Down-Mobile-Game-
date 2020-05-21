using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinScript : MonoBehaviour
{
    protected SANDLSystem sandlSystem;
    protected int currentCoin ,growValue; //kaçar kaçar artıcağını alıyoruz.
    protected string secret0_1, secret0_2;
    public Text tCoin;

    void Start(){
        sandlSystem = GameObject.Find("DataSystem").GetComponent<SANDLSystem>();
        sandlSystem.loadData();
        currentCoin = sandlSystem._coinData.currentCoin;

        //tCoin.text = (secret0_1 + secret0_2 + currentCoin);
        //if (currentCoin < 999) secret0_2 = "0";
        ////else secret0_2 = "";
        //if (currentCoin < 9999) secret0_1 = "0";
        //else secret0_1 = "";
    }

    void Update(){
        sandlSystem.loadData();
        currentCoin = sandlSystem._coinData.currentCoin;
        tCoin.text = "" + currentCoin;
    }
}
