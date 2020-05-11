using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class GetLanguageContent : MonoBehaviour
{
    protected GameObject thisGameObject;
    protected LanguageSystem LanguageSystem;
    protected Text text;
    public string ID;

    void Awake(){}

    void Start()
    {
        thisGameObject = this.gameObject;
        text = this.gameObject.GetComponent(typeof(Text)) as Text;
        LanguageSystem = GameObject.Find("LanguageSystem").GetComponent("LanguageSystem") as LanguageSystem;
    }

    
    void Update()
    {
        if(text != null){
            text.text = LanguageSystem.GetLanguageData(ID);
        }
    }
}
