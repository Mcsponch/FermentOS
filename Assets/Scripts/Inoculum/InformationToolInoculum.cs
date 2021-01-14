using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationToolInoculum : MonoBehaviour
{
    TMP_Text objTypeUI;
    string objTypeString;
    TMP_Text statusUI;
    TMP_Text objQtyUI;
    float objQtyFloat;
    TMP_Text text1UI;
    TMP_Text text2UI;
    TMP_Text text3UI;
    TMP_Text text4UI;
    TMP_Text text5UI;
    TMP_Text text6UI;
    TMP_Text text7UI;
    TMP_Text text8UI;
    TMP_Text text9UI;
    TMP_Text text10UI;
    TMP_Text text11UI;
    TMP_Text text12UI;
    TMP_Text text13UI;

    // Start is called before the first frame update
    void Start()
    {
        objTypeUI = GameObject.Find("Title").GetComponent<TMP_Text>();
        statusUI = GameObject.Find("StatusTextInfo").GetComponent<TMP_Text>();
        objQtyUI = GameObject.Find("Subtitle").GetComponent<TMP_Text>();
        text1UI = GameObject.Find("Text1").GetComponent<TMP_Text>();
        text2UI = GameObject.Find("Text2").GetComponent<TMP_Text>();
        text3UI = GameObject.Find("Text3").GetComponent<TMP_Text>();
        text4UI = GameObject.Find("Text4").GetComponent<TMP_Text>();
        text5UI = GameObject.Find("Text5").GetComponent<TMP_Text>();
        text6UI = GameObject.Find("Text6").GetComponent<TMP_Text>();
        text7UI = GameObject.Find("Text7").GetComponent<TMP_Text>();
        text8UI = GameObject.Find("Text8").GetComponent<TMP_Text>();
        text9UI = GameObject.Find("Text9").GetComponent<TMP_Text>();
        text10UI = GameObject.Find("Text10").GetComponent<TMP_Text>();
        text11UI = GameObject.Find("Text11").GetComponent<TMP_Text>();
        text12UI = GameObject.Find("Text12").GetComponent<TMP_Text>();
        text13UI = GameObject.Find("Text13").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        objTypeString = gameObject.GetComponent<InoculumContainer>().inoculumType;
        
        
        objTypeUI.text = objTypeString;
        objQtyUI.text = "";
        statusUI.text = "Viable";
        text1UI.text = "";
        text2UI.text = "";
        text3UI.text = "";
        text4UI.text = "";
        text5UI.text = "";
        text6UI.text = "";
        text7UI.text = "";
        text8UI.text = "";
        text9UI.text = "";
        text10UI.text = "";
        text11UI.text = "";
        text12UI.text = "";
        text13UI.text = "";
    }

}
