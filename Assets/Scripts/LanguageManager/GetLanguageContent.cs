using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetLanguageContent : MonoBehaviour
{
    protected GameObject thisGameObject;
    protected LanguageSystem LanguageSystem;
    protected Text text, text_button;
    protected TextMeshProUGUI text_TMPro;
    public string ID;

    void Awake(){}

    void Start()
    {
        LanguageSystem = GameObject.Find("LanguageSystem").GetComponent("LanguageSystem") as LanguageSystem;
        thisGameObject = this.gameObject;
        text = this.gameObject.GetComponent(typeof(Text)) as Text;
        text_TMPro = this.gameObject.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        text_button = this.GetComponentInChildren(typeof(Text)) as Text;
    }
    
    void Update()
    {
        if(text != null){
            text.text = LanguageSystem.GetLanguageData(ID);
        }
        if(text_TMPro != null){
            text_TMPro.text = LanguageSystem.GetLanguageData(ID);
        }
        if(text_button != null){
            text_button.text = LanguageSystem.GetLanguageData(ID);
        }
    }
}
