﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationToolEquipmentBiorreactor : MonoBehaviour
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

    TMP_Text errorMsg;

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

        errorMsg = GameObject.Find("EventLogText").GetComponent<TMP_Text>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        ShowInfo();
    }

    public void ShowInfo()
    {
        string maxCap = gameObject.GetComponent<Biorreactor>().maxCapacity.ToString();
        string status = gameObject.GetComponent<Biorreactor>().status;
        string sterilizedStatus = gameObject.GetComponent<Biorreactor>().esterilizationStatus;
        string gMedia = gameObject.GetComponent<Biorreactor>().growthMediaType;
        string gMediaQty = gameObject.GetComponent<Biorreactor>().growthMediaQty.ToString();
        string inocType = gameObject.GetComponent<Biorreactor>().inoculum;
        string remTime = gameObject.GetComponent<Biorreactor>().timeRemaining.ToString();

        typeUI.text = "Biorreactor";
        statusUI.text = status;
        subtitleUI.text = "Capacidad máxima: " + maxCap + " kg";
        text1UI.text = sterilizedStatus;
        text2UI.text = "Medio de cultivo: " + gMedia;
        text3UI.text = gMediaQty + " kg";
        text4UI.text = "Inoculo: ";
        text5UI.text = inocType;
        text6UI.text = "Tiempo restante: " + remTime + " s";
        text7UI.text = "";
        text8UI.text = "";
        text9UI.text = "";
        text10UI.text = "";
        text11UI.text = "";
        text12UI.text = "";
        text13UI.text = "";

        string error = gameObject.GetComponent<Biorreactor>().errorMessage;

        errorMsg.text = error;

    }
}
