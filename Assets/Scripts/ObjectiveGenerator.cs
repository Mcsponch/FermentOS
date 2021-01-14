using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ObjectiveGenerator : MonoBehaviour
{
    public GameObject pInfo;

    AudioSource sound;

    public TMP_Text objective1;
    public TMP_Text remainingTimeUI;

    public string targetMaterial;

    public int selector;
    public int prevSelector;

    float timer;
    public float remainingTime;
    public float timeToNewObj;
    public float materialValue;
    public float materialScore;

    static string LECHE = "Leche";
    static string YOGUR = "Yogur";
    static string YOGUR_F = "Yogur de fresa";
    static string S_FRESA = "Saborizante fresa";
    static string LECHE_F = "Leche de fresa";
    static string CEBADA = "Cebada";
    static string AGUA = "Agua";
    static string MALTA = "Malta";
    static string MALTA_M = "Malta molida";
    static string MEDIO_CERVEZA = "Medio para cerveza";
    static string CERVEZA_S = "Cerveza con sólidos";
    static string CERVEZA = "Cerveza";
    static string UVAS = "Uvas";
    static string UVAS_M = "Uvas molidas";
    static string JUGO_UVA = "Jugo de uva";
    static string VINO = "Vino";
    static string AGUA_M = "Agua de mar";
    static string ARTHROSPIRA = "Arthrospira";
    static string MEDIO_BCAA = "Medio para BCAA";
    static string AMINOACIDOS = "Aminoácidos";


    // Start is called before the first frame update
    void Start()
    {
        targetMaterial = YOGUR;
        materialScore = 1;
        materialValue = 35.5f;
        timeToNewObj = 5 * 60;
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        objective1.text = targetMaterial;
        remainingTimeUI.text = remainingTime.ToString() + " s";


        timer += Time.deltaTime;

        if (timer < timeToNewObj)
        {
            remainingTime = Mathf.Round(timeToNewObj - timer);
        }

        else if (timer > timeToNewObj)
        {
            RandomObjectiveSelector();
            timer = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Container"))
        {
            Debug.Log("Envasalo primero");
        }
        else if (collision.gameObject.CompareTag("Packed"))
        {
            if (collision.GetComponent<Container>().type == targetMaterial)
            {
                float collisionValue = collision.GetComponent<Container>().quantity;
                float scoreToAdd = collisionValue * materialScore;
                float moneyToAdd = collisionValue * materialValue;
                pInfo.GetComponent<PlayerInfo>().AddScore(scoreToAdd);
                pInfo.GetComponent<PlayerInfo>().AddMoney(moneyToAdd);
                Destroy(collision.gameObject);
                sound.Play();

            }
            else if (collision.GetComponent<Container>().type != targetMaterial)
            {
                Debug.Log("Ese material no es bato");
            }
        }

    }

    private void RandomObjectiveSelector()
    {
        
        selector = Mathf.RoundToInt(Random.Range(0, 7));

        while (selector == prevSelector)
        {
            selector = Mathf.RoundToInt(Random.Range(0, 7));
        }

        switch (selector)
        {
            
            case 0:
                targetMaterial = YOGUR;
                materialScore = 3;
                materialValue = 35.5f;
                break;
            case 1:
                targetMaterial = CERVEZA;
                materialScore = 6;
                materialValue = 34.1f;
                break;
            case 2:
                targetMaterial = VINO;
                materialScore = 4;
                materialValue = 56;
                break;
            case 3:
                targetMaterial = ARTHROSPIRA;
                materialScore = 4;
                materialValue = 621;
                break;
            case 4:
                targetMaterial = AMINOACIDOS;
                materialScore = 5;
                materialValue = 1200;
                break;
            case 5:
                targetMaterial = LECHE_F;
                materialScore = 1;
                materialValue = 15;
                break;
            case 6:
                targetMaterial = YOGUR_F;
                materialScore = 3;
                materialValue = 37f;
                break;

        }
        Debug.Log("Nuevo objetivo seleccionado");

        prevSelector = selector;
    }

}
