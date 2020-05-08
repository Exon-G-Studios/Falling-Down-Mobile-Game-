using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEditor;

public class LanguageManagerEditor : EditorWindow
{
    public string addLanguage = "", filePath = @"Assets\Scripts\Localization\Languages\";
    public DirectoryInfo DI = new DirectoryInfo(@"Assets\Scripts\Localization\Languages");
    protected int CurrentLanguageIndex, TranslationLanguageIndex = -1, EditLaguageIndex = -1, TotalLanguagesIndex = 0, editIdIndex, editContentIndex;
    protected string translation_ID, translation_Content;

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    [MenuItem("Tools/Language Manager")]
    static void init(){
        LanguageManagerEditor window = (LanguageManagerEditor)EditorWindow.GetWindow(typeof(LanguageManagerEditor));
        window.Show();
    }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Asyncs a language id's to main id list
    protected void syncDataTOMain(string syncLanguage){
        string[] languageData = System.IO.File.ReadAllLines(filePath + syncLanguage + ".csv");
        string[] mainData = System.IO.File.ReadAllLines(@"Assets\Scripts\Localization\Editor\id_list.csv");
        Array.Resize(ref mainData, languageData.Length);
        for(int line = 1; line < languageData.Length; line++){
            if(mainData[line] != languageData[line].Substring(0, languageData[line].IndexOf(", "))){
                mainData[line] = languageData[line].Substring(0, languageData[line].IndexOf(", "));
            } 
        }
        System.IO.File.WriteAllLines(@"Assets\Scripts\Localization\Editor\id_list.csv", mainData);
    }

    //Asyncs a language id's from main id list
    protected void syncDataTOLanguage(string syncLanguage){
        string[] mainData = System.IO.File.ReadAllLines(@"Assets\Scripts\Localization\Editor\id_list.csv");
        string[] languageData = System.IO.File.ReadAllLines(filePath + syncLanguage + ".csv");
        Array.Resize(ref languageData, mainData.Length);
        for(int line = 1; line < mainData.Length; line++){
            if(languageData[line] == null || languageData[line].Substring(0, languageData[line].IndexOf(", ")) != mainData[line]){
                languageData[line] = mainData[line] + ", ";
            }
        }
        System.IO.File.WriteAllLines(filePath + syncLanguage + ".csv", languageData);
    }

    //Resets main id list
    protected void resetDataMain(){
        string[] mainDataID = {"id"};
        System.IO.File.WriteAllLines(@"Assets\Scripts\Localization\Editor\id_list.csv", mainDataID);
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

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Reset Main Data List If No Language Availabel
        if(Languages.Length == 0) resetDataMain();

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

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
            if(addLanguage != ""){
                try{
                    if(File.Exists(filePath + addLanguage + ".csv")) Debug.Log("You've already created this language.");
                    else{
                        using(FileStream fs = File.Create(filePath + addLanguage + ".csv")){}
                        using(System.IO.StreamWriter file = new System.IO.StreamWriter(filePath + addLanguage + ".csv")){
                            file.WriteLine("id , content");
                        }
                        syncDataTOLanguage(addLanguage);
                        Debug.Log("Language has been added !");
                        addLanguage = "";
                    }
                }
                catch(Exception ex){
                    Debug.Log("An error occured:" + ex);
                }
            }
        }

        //Deleting a existing language
        if(GUILayout.Button("Delete", GUILayout.Width(50))){
            try{
                if(File.Exists(filePath + addLanguage + ".csv")){
                    File.Delete(filePath + addLanguage + ".csv");
                    Debug.Log("Language has been deleted !");
                    addLanguage = "";
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
        
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

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
                string[] lines = System.IO.File.ReadAllLines(filePath + Languages[TranslationLanguageIndex] + ".csv");
                using(System.IO.StreamWriter file = new System.IO.StreamWriter(filePath + Languages[TranslationLanguageIndex] + ".csv", true)){
                    bool contains_id = false;
                    foreach(string line in lines){
                        if(line.Contains(translation_ID)){
                            Debug.Log("You've already added this translation.");
                            contains_id = true;
                        }
                    }
                    if(contains_id != true){
                        file.WriteLine(translation_ID + ", " + translation_Content);
                        Debug.Log("Translation has been added !");
                        translation_ID = "";
                        translation_Content = "";
                    }
                }
                syncDataTOMain(Languages[TranslationLanguageIndex]);
                for(int languageInOrder = 0; languageInOrder < Languages.Length; languageInOrder++){
                    if(Languages[languageInOrder] != Languages[TranslationLanguageIndex]) syncDataTOLanguage(Languages[languageInOrder]);
                }

            }
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(20);

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Editing Translation Section
        GUILayout.Label("Edit Traslation", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        GUILayout.Label("Choose Language:", GUILayout.Width(110));
        EditLaguageIndex = EditorGUILayout.Popup(EditLaguageIndex, CurrentLanguages, GUILayout.Width(100));
        GUILayout.EndHorizontal();
             
        if(EditLaguageIndex >= 0){
            //Get Id and Content of the choosen language for edit
            string[] EditLanguage = System.IO.File.ReadAllLines(filePath + Languages[EditLaguageIndex] + ".csv");
            string[] editTranslation_ID = new string[EditLanguage.Length], editTranslation_Content = new string[EditLanguage.Length];
            for(int line = 0; line < EditLanguage.Length; line++){
                if(editTranslation_ID[line] == null && editTranslation_Content[line] == null){
                    editTranslation_ID[line] = EditLanguage[line].Substring(0, EditLanguage[line].IndexOf(", "));
                    editTranslation_Content[line] = EditLanguage[line].Substring(EditLanguage[line].IndexOf(", "), EditLanguage[line].Length);
                }
            }

            GUILayout.BeginHorizontal();
            GUILayout.Space(40);
            editIdIndex = EditorGUILayout.Popup(editIdIndex, editTranslation_ID, GUILayout.Width(55));
            editTranslation_Content[editIdIndex] = GUILayout.TextField(editTranslation_Content[editIdIndex], editTranslation_Content[editIdIndex], GUILayout.Width(210));
        }



        //Chose a language to edit translation
    }

}
