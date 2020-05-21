using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    protected SANDLSystem sandlSystem;
    [HideInInspector]
    public static int currentGameMode, currentLevel, currentLevelPrice;

    void Start(){
        sandlSystem = this.GetComponent<SANDLSystem>();
    }

    //Setting And Getting Current Game Mode and Level---------------------------------------------------------------------------------------

    public void SetGameMode(int mode){
        currentGameMode = mode;
    }
    public void SetGameLevel(int level){
        currentLevel = level;
    }
    public void SetLevelPrice(int price){
        currentLevelPrice = price;
    }

    public int GetGameMode() { return currentGameMode; }
    public int GetGameLevel() { return currentLevel; }

    //--------------------------------------------------------------------------------------------------------------------------------------

    public void SaveLevelData(int newData){

        sandlSystem.loadData();
        //If not bought buy
        if(sandlSystem._levelData.levels[ currentGameMode, currentLevel, 0] == 0){
            if(currentLevelPrice <= sandlSystem._coinData.currentCoin){
                sandlSystem._levelData.levels[ currentGameMode, currentLevel, 0] = newData;
                sandlSystem._coinData.currentCoin -= currentLevelPrice;
                sandlSystem.RunSANDL();
            }
            else { Debug.Log("Can't Buy Check You Coin !"); }
        }
        //If it's bought then change data
        else{
            sandlSystem.loadData();
            sandlSystem._levelData.levels[ currentGameMode, currentLevel, 0] = newData;
            sandlSystem.RunSANDL();
        }
    }

}
