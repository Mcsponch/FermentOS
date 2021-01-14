using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Marmita : MonoBehaviour
{

    public GameObject container;
    public Transform deliveryPos;

    private Vector3 mOffset;
    private float mZCoord;

    //
    public float quantity;
    public float quantityM1;
    public float quantityM2;
    public float quantityOut;

    public float xC1;
    public float xC2;
    public float xC3;

    public float xP1;
    public float xP2;
    public float xP3;

    public float xF1;
    public float xF2;
    public float xF3;

    public float xW1;
    public float xW2;
    public float xW3;

    public float xI1;
    public float xI2;
    public float xI3;

    public string interestP1;
    public string interestP2;
    public string interestP3;

    public float maxCapacity;
    public float rate;
    public float timer;
    public float timeToComplete;
    public float timeRemaining;

    public string materialInside1 = "";
    public string materialInside2 = "";
    public string materialOut = "";
    public string status = "No instalada";

    string NO_INSTALADA = "No instalada";
    string LISTO_PARA_OPERAR = "Listo para operar";
    string PROCESO_INICIADO = "Proceso iniciado";

    public bool canAddQty = true;
    public bool canAddMat = true;
    public bool canMove = true;
    public bool processStarted = false;
    public bool canInstall = true;
    public bool goodResult = false;

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

    Animator animator;
    AudioSource sound;

    const string UNINSTALLED = "MarmitaIdle";
    const string INSTALLED = "MarmitaInstall";
    const string WORKING = "MarmitaWorking";


    void Start()
    {
        maxCapacity = 1000;
        rate = 100; // kg / h
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckStatus();
        MaterialOutChecker();

        if (processStarted == true)
        {
            timer += Time.deltaTime;
            
            if (timer < timeToComplete)
            {
                //update timer
                timeRemaining = timeToComplete - timer;
                
            }
            
            else if (timer >= timeToComplete)
            {

                if (goodResult == true)
                {
                    //spawn fnished product
                    GameObject containerClone = Instantiate(container, deliveryPos);
                    containerClone.transform.localPosition = Vector3.zero;
                    containerClone.GetComponent<Container>().SetType(materialOut);
                    containerClone.GetComponent<Container>().SetQuantity(quantityOut);
                    containerClone.GetComponent<Container>().SetCarbs(xC3);
                    //containerClone.GetComponent<Container>().SetFat(xF3);
                    //containerClone.GetComponent<Container>().SetProt(xP3);
                    containerClone.GetComponent<Container>().SetWater(xW3);
                    containerClone.GetComponent<Container>().SetInterestProd(interestP3);
                    //containerClone.GetComponent<Container>().SetInterestPer(xI3);


                    timer = 0;
                    processStarted = false;
                    DrainOnClick();
                    gameObject.GetComponent<InformationToolEquipmentMarmita>().ShowInfo();

                }
                else
                {
                    //Spawn finished bad product
                    GameObject containerClone = Instantiate(container, deliveryPos);
                    containerClone.transform.localPosition = Vector3.zero;
                    containerClone.GetComponent<Container>().SetType("Material inutil");
                    containerClone.GetComponent<Container>().SetQuantity(quantityOut);
                    containerClone.GetComponent<Container>().SetCarbs(xC3);
                    //containerClone.GetComponent<Container>().SetFat(xF3);
                    //containerClone.GetComponent<Container>().SetProt(xP3);
                    containerClone.GetComponent<Container>().SetWater(xW3);
                    containerClone.GetComponent<Container>().SetInterestProd(interestP3);
                    containerClone.GetComponent<Container>().SetInterestPer(xI3);


                    timer = 0;
                    processStarted = false;
                    DrainOnClick();
                    gameObject.GetComponent<InformationToolEquipmentMarmita>().ShowInfo();
                }

            }
        }
        
    }

    public void InstallOnClick()
    {
        if (canMove == true && canInstall == true)
        {
            canMove = false;
            gameObject.GetComponent<InformationToolEquipmentMarmita>().ShowInfo();
        }
        else
        {
            canMove = true;
            gameObject.GetComponent<InformationToolEquipmentMarmita>().ShowInfo();
        }


    }

    public void DrainOnClick()
    {
        if (quantity != 0)
        {
            quantity = 0;
            quantityM1 = 0;
            quantityM2 = 0;
            materialInside1 = "";
            materialInside2 = "";
            materialOut = "";
            canAddMat = true;
            canAddQty = true;
            goodResult = false;
            sound.Stop();

        }


        gameObject.GetComponent<InformationToolEquipmentMarmita>().ShowInfo();
    }

    public void StartProcessOnClick()
    {
        
        if (processStarted == false && canMove == false && quantity > 0)
        {
            sound.Play();
            processStarted = true;
            canAddMat = false;
            canAddQty = false;
            timeToComplete = quantity / rate;
            
        }

        gameObject.GetComponent<InformationToolEquipmentMarmita>().ShowInfo();
    }

    void OnMouseDown()
    {
        // Drag and drop
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        //Store offset
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

    }

    private Vector3 GetMouseAsWorldPoint()
    {
        //Pixel coordinates of mouse
        Vector3 mousePoint = Input.mousePosition;

        //z coordinate
        mousePoint.z = mZCoord;

        //Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    private void OnMouseDrag()
    {
        if (canMove == true)
        {
            transform.position = GetMouseAsWorldPoint() + mOffset;
        }
        else
        {
            transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Container"))
        {
            //Get container properties            
            float collisionvalue = collision.GetComponent<Container>().quantity;
            string collisionType = collision.GetComponent<Container>().type;

            //Set Material types after equipment installation 
            if (canAddMat == true && canMove == false)
            {
                SetMatType(collision);
            }

            //Verify it doesn't exceed max capacity
            if (quantity + collisionvalue < maxCapacity)
            {
                canAddQty = true;
            }
            else if (quantity + collisionvalue > maxCapacity)
            {
                canAddQty = false;
            }



            if (canAddQty == true)
            {
                if (collisionType == materialInside1)
                {
                    quantityM1 += collisionvalue;
                    quantity += collisionvalue;
                    Destroy(collision.gameObject);
                }
                else if (collisionType == materialInside2)
                {
                    quantityM2 += collisionvalue;
                    quantity += collisionvalue;
                    Destroy(collision.gameObject);
                }

            }

            quantityOut = quantity;

            ResultCalculations();

            gameObject.GetComponent<InformationToolEquipmentMarmita>().ShowInfo();

        }



    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        canInstall = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canInstall = true;
    }

    private void SetMatType(Collider2D collision)
    {
        if (materialInside1 == "")
        {
            string collisionType1 = collision.GetComponent<Container>().type;
            materialInside1 = collisionType1;

            float xCm1 = collision.GetComponent<Container>().carbs;
            xC1 = xCm1;

            float xPm1 = collision.GetComponent<Container>().prot;
            xP1 = xPm1;

            float xFm1 = collision.GetComponent<Container>().fat;
            xF1 = xFm1;

            float xWm1 = collision.GetComponent<Container>().water;
            xW1 = xWm1;

            float xIm1 = collision.GetComponent<Container>().interestPer;
            xI1 = xIm1;



        }
        else if (materialInside2 == "")
        {
            string collisionType2 = collision.GetComponent<Container>().type;
            materialInside2 = collisionType2;

            float xCm2 = collision.GetComponent<Container>().carbs;
            xC2 = xCm2;

            float xPm2 = collision.GetComponent<Container>().prot;
            xP2 = xPm2;

            float xFm2 = collision.GetComponent<Container>().fat;
            xF2 = xFm2;

            float xWm2 = collision.GetComponent<Container>().water;
            xW2 = xWm2;

            float xIm2 = collision.GetComponent<Container>().interestPer;
            xI2 = xIm2;

        }
        else if (materialInside1 != "" && materialInside2 != "")
        {
            canAddMat = false;
        }
        MaterialOutChecker();
        
    }
    
    private void ResultCalculations()
    {
        xC3 = ((quantityM1 * xC1) + (quantityM2 * xC2)) / quantity;
        //xP3 = ((quantityM1 * xP1) + (quantityM2 * xP2)) / quantity;
        //xF3 = ((quantityM1 * xF1) + (quantityM2 * xF2)) / quantity;
        xW3 = ((quantityM1 * xW1) + (quantityM2 * xW2)) / quantity;
        //xI3 = ((quantityM1 * xI1) + (quantityM2 * xI2)) / quantity;

        if (interestP1 != "")
        {
            interestP3 = interestP1;
        }
        else if (interestP1 == "" && interestP2 != "")
        {
            interestP3 = interestP2;
        }
        else if (interestP1 != "" && interestP2 != "")
        {
            interestP3 = interestP1;
        }


    }

    private void CheckStatus()
    {
        if (canMove == true)
        {
            status = NO_INSTALADA;
            ChangeAnimationState(UNINSTALLED);
        }
        else if (canMove == false)
        {
            status = LISTO_PARA_OPERAR;
            ChangeAnimationState(INSTALLED);
        }
        
        if (processStarted == true)
        {
            status = PROCESO_INICIADO;
            ChangeAnimationState(WORKING);
        }
    }

    public void SetMaxCapacity(float newMaxCapacity)
    {
        maxCapacity = newMaxCapacity;
    }

    private void MaterialOutChecker()
    {
        string materialSum = materialInside1 + materialInside2;
        switch (materialSum)
        {
            case "Leche":
                materialOut = LECHE;
                goodResult = true;
                break;
            case "LecheLeche":
                materialOut = LECHE;
                goodResult = true;
                break;
            case "LecheSaborizante de fresa":
                materialOut = LECHE_F;
                goodResult = true;
                break;
            case "Saborizante de fresaLeche":
                materialOut = LECHE_F;
                goodResult = true;
                break;
            case "LecheAgua":
                materialOut = LECHE;
                goodResult = true;
                break;
            case "AguaLeche":
                materialOut = LECHE;
                goodResult = true;
                break;

            case "Yogur":
                materialOut = YOGUR;
                goodResult = true;
                break;
            case "YogurYogur":
                materialOut = YOGUR;
                goodResult = true;
                break;
            case "YogurSaborizante de fresa":
                materialOut = YOGUR_F;
                goodResult = true;
                break;
            case "Saborizante de fresaYogur":
                materialOut = YOGUR_F;
                goodResult = true;
                break;

            case "Saborizante de fresa":
                materialOut = S_FRESA;
                goodResult = true;
                break;
            case "Saborizante de fresaSaborizante de fresa":
                materialOut = S_FRESA;
                goodResult = true;
                break;

            case "Yogur de fresa":
                materialOut = YOGUR_F;
                goodResult = true;
                break;
            case "Yogur de fresaYogur de fresa":
                materialOut = YOGUR_F;
                goodResult = true;
                break;

            case "Cebada":
                materialOut = CEBADA;
                goodResult = true;
                break;
            case "CebadaCebada":
                materialOut = CEBADA;
                goodResult = true;
                break;
            case "CebadaAgua":
                materialOut = MALTA;
                goodResult = true;
                break;
            case "AguaCebada":
                materialOut = MALTA;
                goodResult = true;
                break;

            case "Agua":
                materialOut = AGUA;
                goodResult = true;
                break;
            case "AguaAgua":
                materialOut = AGUA;
                goodResult = true;
                break;
            case "AguaMalta":
                materialOut = MALTA;
                goodResult = true;
                break;
            case "MaltaAgua":
                materialOut = MALTA;
                goodResult = true;
                break;
            case "Malta molidaAgua":
                materialOut = MEDIO_CERVEZA;
                goodResult = true;
                break;
            case "AguaMalta molida":
                materialOut = MEDIO_CERVEZA;
                goodResult = true;
                break;
            case "AguaMedio para cerveza":
                materialOut = MEDIO_CERVEZA;
                goodResult = true;
                break;
            case "Medio para cervezaAgua":
                materialOut = MEDIO_CERVEZA;
                goodResult = true;
                break;
            case "Medio para BCAAAgua":
                materialOut = MEDIO_BCAA;
                goodResult = true;
                break;
            case "AguaMedio para BCAA":
                materialOut = MEDIO_BCAA;
                goodResult = true;
                break;

            case "Malta":
                materialOut = MALTA;
                goodResult = true;
                break;
            case "MaltaMalta":
                materialOut = MALTA;
                goodResult = true;
                break;

            case "Malta molida":
                materialOut = MALTA_M;
                goodResult = true;
                break;
            case "Malta molidaMalta molida":
                materialOut = MALTA_M;
                goodResult = true;
                break;

            case "Medio para cerveza":
                materialOut = MEDIO_CERVEZA;
                goodResult = true;
                break;
            case "Medio para cervezaMedio para cerveza":
                materialOut = MEDIO_CERVEZA;
                goodResult = true;
                break;

            case "Cerveza con solidos":
                materialOut = CERVEZA_S;
                goodResult = true;
                break;
            case "Cerveza con solidosCerveza con solidos":
                materialOut = CERVEZA_S;
                goodResult = true;
                break;

            case "Cerveza":
                materialOut = CERVEZA;
                goodResult = true;
                break;
            case "CervezaCerveza":
                materialOut = CERVEZA;
                goodResult = true;
                break;

            case "Jugo de uva":
                materialOut = JUGO_UVA;
                goodResult = true;
                break;
            case "Jugo de uvaJugo de uva":
                materialOut = JUGO_UVA;
                goodResult = true;
                break;

            case "Vino":
                materialOut = VINO;
                goodResult = true;
                break;
            case "VinoVino":
                materialOut = VINO;
                goodResult = true;
                break;

            case "Agua de mar":
                materialOut = AGUA_M;
                goodResult = true;
                break;
            case "Agua de marAgua de mar":
                materialOut = AGUA_M;
                goodResult = true;
                break;

            case "Arthrospira":
                materialOut = ARTHROSPIRA;
                goodResult = true;
                break;
            case "ArthrospiraArthrospira":
                materialOut = ARTHROSPIRA;
                goodResult = true;
                break;

            case "Medio para BCAA":
                materialOut = MEDIO_BCAA;
                goodResult = true;
                break;
            case "Medio para BCAAMedio para BCAA":
                materialOut = MEDIO_BCAA;
                goodResult = true;
                break;

            case "Aminoácidos":
                materialOut = AMINOACIDOS;
                goodResult = true;
                break;
            case "AminoácidosAminoácidos":
                materialOut = AMINOACIDOS;
                goodResult = true;
                break;
        }

    }

    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }

}
