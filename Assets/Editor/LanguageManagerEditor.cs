using System;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEditor;

public class LanguageManagerEditor : EditorWindow
{
    public string addLanguage = "", filePath = @"Assets\Localization\Languages\";
    public DirectoryInfo DI = new DirectoryInfo(@"Assets\Localization\Languages");
    protected int CurrentLanguageIndex, TranslationLanguageIndex = -1, EditLanguageIndex = -1, CompareLanguageIndex = -1, TotalLanguagesIndex = 0, editIndex, compareIndex;
    protected string translation_ID, translation_Content, editedTranslationID = null, editedTranslationContent = null;
    protected bool isEditLanguageChanged = false;
    public string[] EditLanguage, editTranslation_ID, editTranslation_Content, PreviousEditLanguage, CompareLanguage, compareTranslation_ID, compareTranslation_Content;

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    [MenuItem("Tools/Language Manager")]
    static void init(){
        LanguageManagerEditor window = (LanguageManagerEditor)EditorWindow.GetWindow(typeof(LanguageManagerEditor));
        window.Show();
    }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Syncs a language id's to main id list
    protected void syncDataTOMain(string syncLanguage){
        string[] languageData = System.IO.File.ReadAllLines(filePath + syncLanguage + ".csv");
        string[] mainData = System.IO.File.ReadAllLines(@"Assets\Editor\id_list.csv");
        Array.Resize(ref mainData, languageData.Length);
        for(int line = 1; line < languageData.Length; line++){
            if(mainData[line] != languageData[line].Substring(0, languageData[line].IndexOf(", "))){
                mainData[line] = languageData[line].Substring(0, languageData[line].IndexOf(", "));
            } 
        }
        System.IO.File.WriteAllLines(@"Assets\Editor\id_list.csv", mainData);
    }

    //Syncs a language id's from main id list
    protected void syncDataTOLanguage(string syncLanguage){
        string[] mainData = System.IO.File.ReadAllLines(@"Assets\Editor\id_list.csv");
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
        System.IO.File.WriteAllLines(@"Assets\Editor\id_list.csv", mainDataID);
    }

    //Sync all languages to main
    protected void syncDataTOAll(string[] Languages){
        for(int languageInOrder = 0; languageInOrder < Languages.Length; languageInOrder++){
            syncDataTOLanguage(Languages[languageInOrder]);
        }
    }

//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void OnGUI(){
        //Get All Languages
        FileInfo[] Languages_Files = DI.GetFiles("*.csv");
        string[] Languages = Languages_Files.Select(f => f.Name).ToArray();
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
        GUILayout.Label("All Available Languages:", EditorStyles.boldLabel, GUILayout.Width(160));
        GUILayout.Space(40);
        CurrentLanguageIndex = EditorGUILayout.Popup(CurrentLanguageIndex, CurrentLanguages, GUILayout.Width(151));
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        //Adding Language Section
        GUILayout.BeginHorizontal();
        GUILayout.Label("Add Language:", EditorStyles.boldLabel, GUILayout.Width(95));
        addLanguage = EditorGUILayout.TextField("", addLanguage, GUILayout.Width(150), GUILayout.Height(21));
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
        GUILayout.BeginHorizontal();
        GUILayout.Space(15);
        GUILayout.Label("Traslation", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Space(40);
        GUILayout.Label("Choose Language:", GUILayout.Width(110));
        TranslationLanguageIndex = EditorGUILayout.Popup(TranslationLanguageIndex, CurrentLanguages, GUILayout.Width(100));
        GUILayout.EndHorizontal();

        if(TranslationLanguageIndex >= 0){
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(60);
            GUILayout.Label("Id", GUILayout.Width(105));
            GUILayout.Label(":", GUILayout.Width(10));
            GUILayout.Label("Translation", GUILayout.Width(100));
            GUILayout.EndHorizontal();

            GUILayout.Space(3);

            //Add id and translation
            GUILayout.BeginHorizontal();
            GUILayout.Space(60);
            translation_ID = EditorGUILayout.TextField("", translation_ID, GUILayout.Width(105), GUILayout.Height(21));
            GUILayout.Label(":", GUILayout.Width(8));
            translation_Content = EditorGUILayout.TextField("", translation_Content, GUILayout.Width(350), GUILayout.Height(21));
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
                syncDataTOAll(Languages);
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(20);

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Editing Translation Section
        GUILayout.BeginHorizontal();
        GUILayout.Space(15);
        GUILayout.Label("Edit Traslation", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Space(40);
        GUILayout.Label("Choose Language:", GUILayout.Width(110));
        EditLanguageIndex = EditorGUILayout.Popup(EditLanguageIndex, CurrentLanguages, GUILayout.Width(100));
        GUILayout.EndHorizontal();
             
        if(EditLanguageIndex >= 0){
            //Get Id and Content of the choosen language for edit
            EditLanguage = System.IO.File.ReadAllLines(filePath + Languages[EditLanguageIndex] + ".csv");
            Array.Resize(ref PreviousEditLanguage, EditLanguage.Length);
            for(int line = 0; line < EditLanguage.Length; line++){
                if(EditLanguage[line] != PreviousEditLanguage[line]){
                    PreviousEditLanguage[line] = EditLanguage[line];
                    isEditLanguageChanged = true;
                }
            }

            if(isEditLanguageChanged != false){
                editTranslation_ID = new string[EditLanguage.Length];
                editTranslation_Content = new string[EditLanguage.Length];
                for(int line = 1; line < EditLanguage.Length; line++){
                    if(editTranslation_ID[line] == null && editTranslation_Content[line] == null){
                        editTranslation_ID[line] = EditLanguage[line].Substring(0, EditLanguage[line].IndexOf(", "));
                        editTranslation_Content[line] = EditLanguage[line].Substring(EditLanguage[line].IndexOf(",") + 2);
                    }
                }
                isEditLanguageChanged = false;
            }

            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUILayout.Space(60);
            GUILayout.Label("Choose", GUILayout.Width(100));
            GUILayout.Label("Id", GUILayout.Width(104));
            GUILayout.Label(":", GUILayout.Width(10));
            GUILayout.Label("Content", GUILayout.Width(300));
            GUILayout.EndHorizontal();

            GUILayout.Space(3);

            GUILayout.BeginHorizontal();
            GUILayout.Space(60);
            editIndex = EditorGUILayout.Popup(editIndex, editTranslation_ID, GUILayout.Width(100));
            editTranslation_ID[editIndex] = EditorGUILayout.TextField(editTranslation_ID[editIndex], GUILayout.Width(105), GUILayout.Height(21));
            editedTranslationID = editTranslation_ID[editIndex];
            GUILayout.Label(":", GUILayout.Width(8));
            editTranslation_Content[editIndex] = EditorGUILayout.TextField(editTranslation_Content[editIndex], GUILayout.Width(350), GUILayout.Height(21));
            editedTranslationContent = editTranslation_Content[editIndex];
            if(GUILayout.Button("Edit", GUILayout.Width(50))){
                if(editedTranslationID != null && editedTranslationContent != null){
                    EditLanguage[editIndex] = editedTranslationID + ", " + editedTranslationContent;
                    System.IO.File.WriteAllLines(filePath + Languages[EditLanguageIndex] + ".csv", EditLanguage);
                }
                editedTranslationID = null;
                editedTranslationContent = null;
                syncDataTOMain(Languages[EditLanguageIndex]);
                syncDataTOAll(Languages);
                isEditLanguageChanged = true;
                Debug.Log("Translation has been edited !");
            }
            if(GUILayout.Button("Delete", GUILayout.Width(60))){
                EditLanguage[editIndex] = null;
                EditLanguage = EditLanguage.Where(str_empty => !string.IsNullOrEmpty(str_empty)).ToArray();
                System.IO.File.WriteAllLines(filePath + Languages[EditLanguageIndex] + ".csv", EditLanguage);
                editedTranslationID = null;
                editedTranslationID = null;
                syncDataTOMain(Languages[EditLanguageIndex]);
                syncDataTOAll(Languages);
                isEditLanguageChanged = true;
                Debug.Log("Translation has been deleted !");
            }  
            GUILayout.EndHorizontal(); 
        }
        GUILayout.Space(20);

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Compare Langauge Section
        GUILayout.BeginHorizontal();
        GUILayout.Space(15);
        GUILayout.Label("Compare Traslation", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Space(40);
        GUILayout.Label("Choose Language:", GUILayout.Width(110));
        CompareLanguageIndex = EditorGUILayout.Popup(CompareLanguageIndex, CurrentLanguages, GUILayout.Width(100));
        GUILayout.EndHorizontal();

        //Get Id and Content of the choosen language for compare
        if(CompareLanguageIndex >= 0){
            CompareLanguage = System.IO.File.ReadAllLines(filePath + Languages[CompareLanguageIndex] + ".csv");
            compareTranslation_ID = new string[CompareLanguage.Length];
            compareTranslation_Content = new string[CompareLanguage.Length];
            for(int line = 1; line < CompareLanguage.Length; line++){
                if(compareTranslation_ID[line] == null && compareTranslation_Content[line] == null){
                compareTranslation_ID[line] = CompareLanguage[line].Substring(0, CompareLanguage[line].IndexOf(", "));
                    compareTranslation_Content[line] = CompareLanguage[line].Substring(CompareLanguage[line].IndexOf(",") + 2);
                }
            }
         
            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUILayout.Space(60);
            GUILayout.Label("Choose", GUILayout.Width(100));
            GUILayout.Label("Id", GUILayout.Width(104));
            GUILayout.Label(":", GUILayout.Width(10));
            GUILayout.Label("Content", GUILayout.Width(300));
            GUILayout.EndHorizontal();
            
            GUILayout.Space(3);

            GUILayout.BeginHorizontal();
            GUILayout.Space(60);
            compareIndex = EditorGUILayout.Popup(compareIndex, compareTranslation_ID, GUILayout.Width(100));
            EditorGUILayout.TextField(compareTranslation_ID[compareIndex], GUILayout.Width(105), GUILayout.Height(21));
            GUILayout.Label(":", GUILayout.Width(8));
            EditorGUILayout.TextField(compareTranslation_Content[compareIndex], GUILayout.Width(350), GUILayout.Height(21));
            GUILayout.EndHorizontal();
        }
    }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//END
}
