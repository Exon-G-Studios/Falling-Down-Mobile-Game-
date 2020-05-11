using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LanguageSystem))]
public class LanguageSystemEditor : Editor
{
    protected int currentLanguageIndex;

    public override void OnInspectorGUI(){
        DrawDefaultInspector();
        LanguageSystem ls_script = (LanguageSystem)target;
        
        ls_script.Start();

        EditorGUI.BeginChangeCheck();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Current Langauge:", GUILayout.Width(110));
        ls_script.currentLanguageIndex = EditorGUILayout.Popup(currentLanguageIndex, ls_script.CurrentLanguages, GUILayout.Width(150));
        GUILayout.EndHorizontal();
        EditorGUI.EndChangeCheck();
    }
}
