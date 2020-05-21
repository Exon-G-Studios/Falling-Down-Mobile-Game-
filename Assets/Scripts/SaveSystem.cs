//using UnityEngine;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;

//public static class SaveSystem 
//{
//    public static void SavePlayerData(playerDataInUnity playerData)
//    {
//        BinaryFormatter formatter = new BinaryFormatter();
//        string path = Application.persistentDataPath + "/player.json";
//        FileStream stream = new FileStream(path, FileMode.Create);

//        playerDataInUnity data = new playerDataInUnity(playerData);
//    }   
//}
