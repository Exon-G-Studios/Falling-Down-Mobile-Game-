using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class LanguageSystem : MonoBehaviour
{
    //Declarations of variables
    protected string id_content = null, filePath = @"Assets\Localization\Languages\";
    protected string[] languageContent;
    protected int idIndex = -1;
    protected bool isIDFound;
    public DirectoryInfo DI = new DirectoryInfo(@"Assets\Localization\Languages");
    [HideInInspector]
    public string[] Languages, CurrentLanguages;
    [HideInInspector]
    public int currentLanguageIndex = -1;

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //At the start get all languages
    public void Start(){
        FileInfo[] Languages_Files = DI.GetFiles("*.csv");
        Languages = Languages_Files.Select(f => f.Name).ToArray();
        CurrentLanguages = Languages;
        for(int n = 0; n < CurrentLanguages.Length; n++){
            CurrentLanguages[n] = CurrentLanguages[n].Substring(0, CurrentLanguages[n].Length - 4);
        }
    }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Declarations of useful functions
    public string[] GetLanguages(){
        return CurrentLanguages;
    }

    public void SetCurrentLanguage(int LanguageIndex){
        if(LanguageIndex >= 0){
            currentLanguageIndex = LanguageIndex;
        }
    }

    public string GetLanguageData(string ID){
        try{
            if(currentLanguageIndex >= 0){
                languageContent = System.IO.File.ReadAllLines(filePath + CurrentLanguages[currentLanguageIndex] + ".csv");
                for(int line = 0; line < languageContent.Length; line++){
                    if(languageContent[line].Substring(0, languageContent[line].IndexOf(",")) == ID){
                        idIndex = line;
                        id_content = languageContent[idIndex].Substring(languageContent[idIndex].IndexOf(",") + 2);
                        isIDFound = true;
                        break;
                    }
                    else{ isIDFound = false; }
                }
            }
        }
        catch{}
        if(isIDFound != true){
            if(Application.isPlaying){ id_content = "error"; }
        }
        if(currentLanguageIndex == -1){ Debug.Log("Choose a language !"); }
        return id_content;
    }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------
//END
}
