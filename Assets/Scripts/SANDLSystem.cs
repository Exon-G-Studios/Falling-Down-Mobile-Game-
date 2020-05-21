using System;
using System.Reflection;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SANDL (Save and Load System Nasıl Kullanılır ?)
//Veri oluşturma bölümünde istediğiniz veriyi class olarak ekledikten sonra veri etiketi bölümünden class adının önüne "_" işareti koyaraktan belirtin.
//Veri Etiketini saveData fonksiyonuna ekleyin.
//Veri Etiketini loadData fonsksiyonuna ekleyin.

//Bir veriyi farklı bir ortamda ya da script'te erişmek ve değiştirmek için 
//Her zaman SANDL Sisteminden veriye ulaşmadan önce loadData()'yı çağırın.
//Sonra veri etiketine ulaşaraktan istediğiniz değeri değiştrin.
//Veri değişikliğini kaydetmek için RunSANDL() fonskiyonu çağırın.

public class SANDLSystem : MonoBehaviour
{
    //Veri Etiketi Oluşturma Bölümü----------------------------------------------------------------------------------------------------------
    //Veriyi Değiştirmek İçin Altakileri Kullan !
    public coinData _coinData;
    public levelData _levelData;
    public characterData _characterData;
    public levelDataCopy _levelDataCopy;
    //Veriyi Değiştirmek İçin Üstekileri Kullan !
    //---------------------------------------------------------------------------------------------------------------------------------------
    
    protected string filePath = @"Assets\Data\allData.json";

    //Veri Oluşturma Bölümü------------------------------------------------------------------------------------------------------------------

    [System.Serializable]
    public class coinData       //Veriyi Değiştirmek İçin Bunu Kullanma //Ama Yeni Veri Tutucuyu Buraya Ekle (Variable)
    {
        public int currentCoin;
    }

    [System.Serializable]
    public class levelData      //Veriyi Değiştirmek İçin Bunu Kullanma //Ama Yeni Veri Tutucuyu Buraya Ekle (Variable)
    {
        //İlk [] değer oyun modunu göstermektedir.
        //İkinci [] değer leveli göstermektedir.
        //Üçüncü [] değer o levelin durumunu göstermektedir.
        //1 Levelin açık olduğunu, 0 Levelin kapalı olduğunu söylemekte
        public int[ , , ] levels = new int[3,3,1] {
                                                    { { 1 }, { 0 }, { 0 } },    //GameMode 1
                                                    { { 1 }, { 0 }, { 0 } },    //GameMode 2
                                                    { { 1 }, { 0 }, { 0 } }     //GameMode 3
                                                  };
        //GameMode'a level eklendiğinde burdan o level eklenmelidir !!
    }

    [System.Serializable]
    public class characterData      //Veriyi Değiştirmek İçin Bunu Kullanma //Ama Yeni Veri Tutucuyu Buraya Ekle (Variable)
    {

    }

    [System.Serializable]
    public class levelDataCopy
    {
        //private int[,] meow = new int[2,2] { {1,2}, {1,2} };

        //public int[,] Meow { get{ return meow; } }

        public int[,] numbers = new int[2,2] { {1,2}, {3,4} };
    }

    //Veri Oluşturma Örneği
    //[System.Serializable]
    //public class istediğin Data
    //{
        //İstenilen her veri
    //}

    //---------------------------------------------------------------------------------------------------------------------------------------

    void Start(){
        RunSANDL();
    }

    //---------------------------------------------------------------------------------------------------------------------------------------

    public void saveData(){
        string[] data = new string[4];                  //Yazılacak Toplam Veri Miktarının Sayısını Burdan Değiştir !
        data[0] = JsonUtility.ToJson(_coinData);
        data[1] = JsonUtility.ToJson(_levelData);
        data[2] = JsonUtility.ToJson(_characterData);
        data[3] = JsonUtility.ToJson(_levelDataCopy);
        //Yeni Data Girme Örneği
        //data[3] = JsonUtility.ToJson(istediğin data (fakat bu dataya ulaşmak için |veri etiketi| oluşturmalısın ! ));
        System.IO.File.WriteAllLines(filePath, data);
    }

    //Veri Masası'na her eklediğin veri etiketi konumunueklemeyi unutma !

    //Veri Masası
    //-----------------------------------------------
    //Coin Data Erişimi: data[0]
    //Level Data Erişimi: data[1]
    //Charater Data Erişimi: data[2]
    //-----------------------------------------------

    public void loadData(){
        string[] data = System.IO.File.ReadAllLines(filePath);
        _coinData = JsonUtility.FromJson<coinData>(data[0]);
        _levelData = JsonUtility.FromJson<levelData>(data[1]);
        _characterData = JsonUtility.FromJson<characterData>(data[2]);
        _levelDataCopy = JsonUtility.FromJson<levelDataCopy>(data[3]);
        //Data Alma Örneği
        //veri etiketi = JsonUtility.FromJson<veri oluşturma class'ı>(veri etiketinin bulunduğu veri masası değeri);
    }

    public void RunSANDL(){
        saveData();
        loadData();
    }

    public void doThis(){
        JSONMulti.ToJson(_levelDataCopy);
    }

}


//Burdan sonrası JsonUtility Multi-Array'leri halledemediğinden kendi json utility'imi yazdım.
//Burdan sonrası baya karmaşık.
//Custom JSON Parser Named "JSONMulti" ------------------------------------------------------------------------------------------------------

public class JSONMulti
{
}

