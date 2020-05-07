using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEditor;

public class LanguageManagerEditor : EditorWindow
{
    public string addLanguage, filePath = @"Assets\Scripts\Localization\Languages\";
    public DirectoryInfo DI = new DirectoryInfo(@"Assets\Scripts\Localization\Languages");
    protected int CurrentLanguageIndex, TranslationLanguageIndex = -1, EditLaguageIndex = -1, PreviousLanguageIndex, TotalLanguagesIndex = 0;
    public string translation_ID, translation_Content;

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    [MenuItem("Tools/Language Manager")]
    static void init(){
        LanguageManagerEditor window = (LanguageManagerEditor)EditorWindow.GetWindow(typeof(LanguageManagerEditor));
        window.Show();
    }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Asyncs a language id's to main id list
    protected void asyncDataTOMain(string asyncLanguage){
        string[] languageData = System.IO.File.ReadAllLines(filePath + asyncLanguage + ".csv");
        string[] mainData_lines = System.IO.File.ReadAllLines(@"Assets\Scripts\Localization\Editor\id_list.csv");
        using(System.IO.StreamWriter mainData = File.AppendText(@"Assets\Scripts\Localization\Editor\id_list.csv")){
            for(int lines = 1; lines < languageData.Length; lines++){
                mainData.WriteLine(languageData[lines].Substring(0, languageData[lines].IndexOf(", ")));
            }
        }
    }

    //Asyncs a language id's from main id list
    protected void asyncDataTOLanguage(string asyncLanguage){
        string[] mainData = System.IO.File.ReadAllLines(@"Assets\Scripts\Localization\Editor\id_list.csv");
        string[] languageData_lines = System.IO.File.ReadAllLines(filePath + asyncLanguage + ".csv");
        using(System.IO.StreamWriter languageData = new System.IO.StreamWriter(filePath + asyncLanguage + ".csv", true)){
            foreach(string line in languageData_lines){
                for(int lines = 1; lines < mainData.Length; lines++){
                    if(!line.Contains(mainData[lines])){
                        languageData.WriteLine(mainData[lines] + ", ");
                    }
                }
            }
        }
    }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void OnGUI(){
        //Get All Languages
        FileInfo[] Languages_File = DI.GetFiles("*.csv");
        string[] Languages = Languages_File.Select(f => f.Name).ToArray();
        string[] CurrentLanguages = Languages;
        for(int n = 0; n < CurrentLanguages.Length; n++){
            CurrentLanguages[n] = CurrentLanguages[n].Substring(0, CurrentLanguages[n].Length - 4);
        }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Choosing Current Language of Game
        GUILayout.BeginHorizontal();
        GUILayout.Label("Choose Current Language:", EditorStyles.boldLabel, GUILayout.Width(160));
        CurrentLanguageIndex = EditorGUILayout.Popup(CurrentLanguageIndex, CurrentLanguages, GUILayout.Width(141));
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        //Adding Language Section
        GUILayout.BeginHorizontal();
        GUILayout.Label("Add Language:", EditorStyles.boldLabel, GUILayout.Width(95));
        addLanguage = EditorGUILayout.TextField("", addLanguage, GUILayout.Width(100));
        //Adding new language that doesn't exist
        if(GUILayout.Button("Add", GUILayout.Width(50))){
            try{
                if(File.Exists(filePath + addLanguage + ".csv")) Debug.Log("You've already created this language.");
                else{
                    using(FileStream fs = File.Create(filePath + addLanguage + ".csv")){}
                    using(System.IO.StreamWriter file = new System.IO.StreamWriter(filePath + addLanguage + ".csv")){
                        file.WriteLine("ıd , content");
                    }
                    asyncDataTOLanguage(addLanguage);
                    Debug.Log("Language has been added !");
                }
            }
            catch(Exception ex){
                Debug.Log("An error occured:" + ex);
            }
        }

        //Deleting a existing language
        if(GUILayout.Button("Delete", GUILayout.Width(50))){
            try{
                if(File.Exists(filePath + addLanguage + ".csv")){
                    File.Delete(filePath + addLanguage + ".csv");
                    Debug.Log("Language has been deleted !");
                }
                else{
                    Debug.Log("There isn't a language exits that you want to delete !");
                }
            }
            catch(Exception ex){
                Debug.Log("An error occured:" + ex);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Translation Section
        //Choose a language to add translation
        GUILayout.Label("Traslation", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("Choose Language:", GUILayout.Width(110));
        TranslationLanguageIndex = EditorGUILayout.Popup(TranslationLanguageIndex, CurrentLanguages, GUILayout.Width(100));
        GUILayout.EndHorizontal();
        
        if(TranslationLanguageIndex >= 0){
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(40);
            GUILayout.Label("Id:", GUILayout.Width(55));
            GUILayout.Label("Translation:", GUILayout.Width(100));
            GUILayout.EndHorizontal();

            //Add id and translation
            GUILayout.BeginHorizontal();
            GUILayout.Space(40);
            translation_ID = EditorGUILayout.TextField("", translation_ID, GUILayout.Width(55));
            translation_Content = EditorGUILayout.TextField("", translation_Content, GUILayout.Width(210));
            if(GUILayout.Button("Add", GUILayout.Width(40)) && translation_ID != ""){
                PreviousLanguageIndex = TranslationLanguageIndex;
                string[] lines = System.IO.File.ReadAllLines(filePath + Languages[TranslationLanguageIndex] + ".csv");
                using(System.IO.StreamWriter file = new System.IO.StreamWriter(filePath + Languages[TranslationLanguageIndex] + ".csv", true)){
                    foreach(string line in lines){
                        if(!line.Contains(translation_ID)){
                            file.WriteLine(translation_ID + ", " + translation_Content);
                            Debug.Log("Translation has been added !");
                            translation_ID = ""; 
                            translation_Content = "";
                        }
                        else{
                            Debug.Log("You've already added this translation.");
                        }
                    }
                }
                asyncDataTOMain(Languages[TranslationLanguageIndex]);
            }


            //What you want to do in here is that you have to sync every language id to each other 
        }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Editing Translation Section
        //Chose a language to edit translation
    }

}
