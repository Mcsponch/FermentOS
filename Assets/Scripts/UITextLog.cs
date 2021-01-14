using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITextLog : MonoBehaviour
{
    float timer;
    float timeRem;

    string currentMsg;

    // Start is called before the first frame update
    void Start()
    {
        timeRem = 5;
    }

    // Update is called once per frame
    void Update()
    {

        currentMsg = gameObject.GetComponent<TMP_Text>().text;

        if (gameObject.GetComponent<TMP_Text>().text != "")
        {
            timer += Time.deltaTime;
            
            if (timer >= timeRem)
            {
                gameObject.GetComponent<TMP_Text>().text = "";
                timer = 0;
            }

        }
    }
}
