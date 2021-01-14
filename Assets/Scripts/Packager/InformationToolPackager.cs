using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationToolPackager : MonoBehaviour
{
    TMP_Text typeUI;
    TMP_Text statusUI;
    TMP_Text subtitleUI;
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
        typeUI = GameObject.Find("Title").GetComponent<TMP_Text>();
        statusUI = GameObject.Find("StatusTextInfo").GetComponent<TMP_Text>();
        subtitleUI = GameObject.Find("Subtitle").GetComponent<TMP_Text>();
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

    public void OnMouseDown()
    {
        string status = gameObject.GetComponent<Packager>().status;
        string maxCap = gameObject.GetComponent<Packager>().maxCapacity.ToString();
        float quantity = gameObject.GetComponent<Packager>().F1;
        string toPack = gameObject.GetComponent<Packager>().F1s;
        float timeLeft = Mathf.Round(gameObject.GetComponent<Packager>().timeRemaining);
        

        typeUI.text = "Envasadora";
        statusUI.text = status;
        subtitleUI.text = "Capacidad Maxima: " + maxCap + " kg";
        text1UI.text = "";
        text2UI.text = "Material a envasar: " + toPack;
        text3UI.text = "Cantidad: " + quantity + " kg";
        text4UI.text = "";
        text5UI.text = "";
        text6UI.text = "Tiempo restante: " + timeLeft.ToString() + " s";
        text7UI.text = "";
        text8UI.text = "";
        text9UI.text = "";
        text10UI.text = "";
        text11UI.text = "";
        text12UI.text = "";
        text13UI.text = "";
    }
}
