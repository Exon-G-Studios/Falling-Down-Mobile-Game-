using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class LanguageSystem : MonoBehaviour
{
    protected string id_content = null, filePath = @"Assets\Localization\Languages\";
    protected string[] languageContent;
    protected int idIndex = -1;
    protected bool isIDFound;
    public DirectoryInfo DI = new DirectoryInfo(@"Assets\Localization\Languages");
    [HideInInspector]
    public string[] Languages, CurrentLanguages;
    [HideInInspector]
    public int currentLanguageIndex = -1;

    //At the start get all languages
    public void Start(){
        FileInfo[] Languages_Files = DI.GetFiles("*.csv");
        Languages = Languages_Files.Select(f => f.Name).ToArray();
        CurrentLanguages = Languages;
        for(int n = 0; n < CurrentLanguages.Length; n++){
            CurrentLanguages[n] = CurrentLanguages[n].Substring(0, CurrentLanguages[n].Length - 4);
        }
    }

    public string[] GetLanguages(){
        return CurrentLanguages;
    }

    public void SetCurrentLanguage(int LanguageIndex){
        if(LanguageIndex >= 0){
            currentLanguageIndex = LanguageIndex;
        }
    }

    public string GetLanguageData(string ID){
        if(currentLanguageIndex >= 0){
            languageContent = System.IO.File.ReadAllLines(filePath + CurrentLanguages[currentLanguageIndex] + ".csv");
            for(int line = 0; line < languageContent.Length; line++){
                if(languageContent[line].Substring(0, languageContent[line].IndexOf(",") - 1) == ID){
                    idIndex = line;
                    id_content = languageContent[idIndex].Substring(languageContent[idIndex].IndexOf(",") + 2);
                    isIDFound = true;
                    break;
                }
                else{ isIDFound = false; }
            }
        }
        if(isIDFound != true){
            Debug.Log("There isn't a ID that you're looking for. Be sure you've added ID.");
            id_content = "error";
        }
        else{
            Debug.Log("Choose a language !");
        }
        return id_content;
    }
}
