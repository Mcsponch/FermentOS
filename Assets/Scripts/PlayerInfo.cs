using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public TMP_Text moneyUI;
    public TMP_Text scoreUI;

    public float money;
    public float score;
    
    // Start is called before the first frame update
    void Start()
    {
        money = 1000;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.text = "Dinero: " + money + " $";
        scoreUI.text = "Puntaje: " + score;

        if (score >= 1500)
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    public void RestMoney(float cost)
    {
        money = money - cost;
    }

    public void AddScore(float scoreToAdd)
    {
        score = score + scoreToAdd;
    }

    public void AddMoney(float moneyToAdd)
    {
        money = money + moneyToAdd;
    }

}
