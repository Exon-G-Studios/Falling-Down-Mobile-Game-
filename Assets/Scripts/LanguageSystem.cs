using System;
using System.IO;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class LanguageSystem : MonoBehaviour
{
    protected string id_content;
    public DirectoryInfo DI = new DirectoryInfo(@"Assets\Localization\Languages");

    public string[] Languages, CurrentLanguages;
    [HideInInspector]
    public int currentLanguageIndex = -1;

    //At the start get all languages
    void Awake(){
    }

    void Update(){
        Debug.Log("I'm fucking working");
    }

    void Start(){
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

        }
        else{
            Debug.Log("Choose a language !");
        }
        return id_content;
    }
}
