using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SANDLSystem : MonoBehaviour
{
    //Veriyi Değiştirmek İçin Altakileri Kullan !
    public coinData _coindData;
    public levelData _levelData;
    public characterData _characterData;
    //Veriyi Değiştirmek İçin Üstekileri Kullan !
    protected string filePath = @"Assets\Data\allData.json";

    [System.Serializable]
    public class coinData       //Veriyi Değiştirmek İçin Bunu Kullanma //Ama Yeni Veri Tutucuyu Buraya Ekle (Variable)
    {
        public int currentCoint;
    }

    [System.Serializable]
    public class levelData      //Veriyi Değiştirmek İçin Bunu Kullanma //Ama Yeni Veri Tutucuyu Buraya Ekle (Variable)
    {
        public int[][] levels;
    }

    [System.Serializable]
    public class characterData      //Veriyi Değiştirmek İçin Bunu Kullanma //Ama Yeni Veri Tutucuyu Buraya Ekle (Variable)
    {

    }

    public void saveData(){
        string[] data = new string[3];
        data[0] = JsonUtility.ToJson(_coindData);
        data[1] = JsonUtility.ToJson(_levelData);
        data[2] = JsonUtility.ToJson(_characterData);
        System.IO.File.WriteAllLines(filePath, data);
    }

    //Veri Masası
    //-----------------------------------------------
    //Coin Data Erişimi: data[0]
    //Level Data Erişimi: data[1]
    //Charater Data Erişimi: data[2]
    //-----------------------------------------------

    public void loadData(){
        string[] data = System.IO.File.ReadAllLines(filePath);
        _coindData = JsonUtility.FromJson<coinData>(data[0]);
        _levelData = JsonUtility.FromJson<levelData>(data[1]);
        _characterData = JsonUtility.FromJson<characterData>(data[2]);
    }

}

