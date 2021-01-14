using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biorreactor : MonoBehaviour
{
    public GameObject container;
    public Transform deliveryPos;

    private Vector3 mOffset;
    private float mZCoord;

    public float quantityOut;
    public float maxCapacity;
    public float growthMediaQty;
    public float rate;
    public float timeToComplete;
    public float timer;
    public float timeRemaining;

    public float F1;
    public float F2;
    public float F3;

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

    public string growthMediaType;
    public string inoculum;
    public string product;
    public string errorMessage;

    static string NO_INSTALADA = "No instalada";
    static string LISTO_PARA_OPERAR = "Listo para operar";
    static string PROCESO_INICIADO = "Proceso iniciado";
    static string NO_ESTERILIZADO = "No esterilizado";
    static string ESTERILIZADO = "Esterilizado";

    public string status = NO_INSTALADA;
    public string esterilizationStatus = NO_ESTERILIZADO;

    public bool canAddQty = true;
    public bool canAddMat = true;
    public bool canAddInoculum = true;
    public bool canMove = true;
    public bool processStarted = false;
    public bool canInstall = true;
    public bool hasProduct = false;

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
    static string MEDIO_BCAA = "Medio para BCAA";
    static string AMINOACIDOS = "Aminoácidos";

    static string LACTO_STREPTO = "Lactobacillus + Streptococcus";
    static string SACCHAROMYCES = "Saccharomyces cerevisiae";
    static string ARTHROSPIRA = "Arthrospira";
    static string CORYNEBACTERIUM = "Corynebacterium glutamicum";

    Animator animator;
    AudioSource sound;
    

    const string UNINSTALLED = "BiorreactorIdle";
    const string INSTALLED = "BiorreactorInstalled";
    const string WORKING = "BiorreactorWorking";

    // Start is called before the first frame update
    void Start()
    {
        deliveryPos = gameObject.transform.GetChild(0);
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();

        if (processStarted == true)
        {
            timer += Time.deltaTime;

            if (timer < timeToComplete)
            {
                timeRemaining = Mathf.Round(timeToComplete - timer) ;
            }
            else if (timer >= timeToComplete)
            {
                if (hasProduct == true)
                {
                    GameObject containerClone = Instantiate(container, deliveryPos) as GameObject;
                    containerClone.transform.localPosition = Vector3.zero;
                    containerClone.GetComponent<Container>().SetType(product);
                    containerClone.GetComponent<Container>().SetInterestProd(interestP2);
                    containerClone.GetComponent<Container>().SetInterestPer(xI2);
                    containerClone.GetComponent<Container>().SetQuantity(F2);
                    containerClone.GetComponent<Container>().SetWater(xW2);
                    containerClone.GetComponent<Container>().SetCarbs(xC2);

                    timer = 0;
                    processStarted = false;
                    DrainOnClick();
                    gameObject.GetComponent<InformationToolEquipmentBiorreactor>().ShowInfo();

                }
                else
                {
                    GameObject containerClone = Instantiate(container, deliveryPos) as GameObject;
                    containerClone.transform.localPosition = Vector3.zero;
                    containerClone.GetComponent<Container>().SetType("Medio contaminado");
                    containerClone.GetComponent<Container>().SetQuantity(growthMediaQty);

                    timer = 0;
                    processStarted = false;
                    DrainOnClick();
                    gameObject.GetComponent<InformationToolEquipmentBiorreactor>().ShowInfo();
                }
            }

        }
        
    }

    public void InstallOnClick()
    {
        if (canMove == true && canInstall == true)
        {
            canMove = false;
            gameObject.GetComponent<InformationToolEquipmentBiorreactor>().ShowInfo();
        }
        else if (growthMediaQty == 0 && inoculum == "")
        {
            canMove = true;
            gameObject.GetComponent<InformationToolEquipmentBiorreactor>().ShowInfo();
        }


    }

    public void DrainOnClick()
    {
        growthMediaQty = 0;
        growthMediaType = "";
        inoculum = "";
        product = "";
        canAddMat = true;
        canAddQty = true;
        canAddInoculum = true;
        hasProduct = false;
        esterilizationStatus = NO_ESTERILIZADO;
        sound.Stop();

    }

    public void EsterilizeOnClick()
    {
        if (canMove == false && growthMediaQty != 0f && processStarted == false)
        {
            esterilizationStatus = ESTERILIZADO;
            inoculum = "";
            ProductChecker();
        }
    }

    public void StartProcessOnClick()
    {
        if (processStarted == false && canMove == false && growthMediaQty > 0)
        {
            ProductChecker();
            processStarted = true;
            canAddMat = false;
            canAddQty = false;
            timeToComplete = growthMediaQty / rate;
            sound.Play();

        }
    }

    void OnMouseDown()
    {
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

    public void SetMaxCapacity(float newMaxCapacity)
    {
        maxCapacity = newMaxCapacity;
    }

    // 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Container"))
        {
            if (collision.gameObject.GetComponent<Container>().status == "Liquido")
            {
                errorMessage = "";
                float growthMediaQ = collision.GetComponent<Container>().quantity;
                string growthMediaT = collision.GetComponent<Container>().type;

                // Verify it doesn't exceed max capacity
                if (growthMediaQty + growthMediaQ < maxCapacity)
                {
                    canAddQty = true;
                }
                else if (growthMediaQty + growthMediaQ > maxCapacity)
                {
                    canAddQty = false;
                }

                if (canAddMat == true && canMove == false)
                {
                    SetMatType(collision);
                }

                if (canAddQty == true)
                {
                    if (growthMediaT == growthMediaType)
                    {
                        growthMediaQty += growthMediaQ;
                        Destroy(collision.gameObject);
                    }

                }



            }
            else
            {
                errorMessage = "El medio de cultivo debe de estar en estado liquido";
            }


            ProductChecker();

        }

            if (collision.gameObject.CompareTag("Inoculum"))
            {
                if (canAddInoculum == true && canMove == false)
                {
                    SetInoculum(collision);
                    Destroy(collision.gameObject);
                    ProductChecker();
                }
            }
    }

    // Is hitting something so cant install
    private void OnTriggerStay2D(Collider2D collision)
    {
        canInstall = false;
    }

    // Stopped hitting so it can install
    private void OnTriggerExit2D(Collider2D collision)
    {
        canInstall = true;
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


    private void SetMatType(Collider2D collision)
    {
        if (growthMediaType == "")
        {
            growthMediaType = collision.GetComponent<Container>().type;
            xC1 = collision.GetComponent<Container>().carbs;
            xF1 = collision.GetComponent<Container>().fat;
            xP1 = collision.GetComponent<Container>().prot;
            xW1 = collision.GetComponent<Container>().water;
            xI1 = collision.GetComponent<Container>().interestPer;

        }
        else
        {
            canAddMat = false;
        }


    }

    private void SetInoculum(Collider2D collision)
    {
        if (inoculum == "")
        {
            inoculum = collision.GetComponent<InoculumContainer>().inoculumType;
        }
        else
        {
            canAddInoculum = false;
        }
    }

    private void ProductChecker()
    {
        string combo = inoculum + growthMediaType;
        if (esterilizationStatus == ESTERILIZADO)
        {
            switch (combo)
            {
                case "Lactobacillus + StreptococcusLeche":
                    rate = 100;
                    product = YOGUR;
                    interestP2 = "";
                    F1 = growthMediaQty;
                    xW2 = 0.81F;
                    xW3 = 1f;
                    F2 = (F1 * xW1 - F1 * xW3) / (xW2 - xW3);
                    xC2 = F1 * xC1 / F2;
                    xI2 = F1 * xI1 / F2;
                    F3 = F1 - F2;
                    hasProduct = true;
                    break;

                case "Leche":
                    rate = 50;
                    product = LECHE;
                    hasProduct = true;
                    break;

                case "Saccharomyces cerevisiaeMedio para cerveza":
                    rate = 100;
                    product = CERVEZA_S;
                    interestP2 = "Alcohol";
                    F1 = growthMediaQty;
                    F2 = F1;
                    float sugar = F1 * xC1 * 0.8f;
                    float alcohol = sugar * 0.15f;
                    float sugarLeft = (F1 * xC1) - alcohol;
                    
                    xC2 = sugarLeft / F2;
                    xI2 = alcohol / F2;
                    xW2 = xW1;

                    hasProduct = true;
                    break;

                case "Medio para cerveza":
                    rate = 50;
                    product = MEDIO_CERVEZA;
                    hasProduct = true;
                    break;

                case "Saccharomyces cerevisiaeJugo de uva":
                    rate = 100;
                    product = VINO;
                    interestP2 = "Alcohol";
                    F1 = growthMediaQty;
                    F2 = F1;
                    float sugarV = F1 * xC1 * 0.8f;
                    float alcoholV = sugarV * 0.15f;
                    float sugarLeftV = (F1 * xC1) - alcoholV;

                    xC2 = sugarLeftV / F2;
                    xI2 = alcoholV / F2;
                    xW2 = xW1;
                    hasProduct = true;
                    break;

                case "Jugo de uva":
                    rate = 50;
                    product = JUGO_UVA;
                    hasProduct = true;
                    break;

                case "ArthrospiraAgua de mar":
                    rate = 100;
                    product = ARTHROSPIRA;
                    interestP2 = "Arthrospira";
                    F1 = growthMediaQty;
                    float minerals = F1 * xC1;
                    float algae = minerals * 2;
                    F2 = F1 + (F1 * xC1);
                    xI2 = algae / F1;
                    xW2 = 1 - xI2;
                    hasProduct = true;
                    break;

                case "Agua de mar":
                    rate = 50;
                    product = AGUA_M;
                    hasProduct = true;
                    break;
                case "Corynebacterium glutamicumMedio para BCAA":
                    rate = 100;
                    product = AMINOACIDOS;
                    interestP2 = AMINOACIDOS;
                    F1 = growthMediaQty;
                    float glc = F1 * xC1;
                    float bcaa = glc * 0.25f;
                    F2 = F1;
                    xI2 = bcaa / F2;
                    xC2 = (glc - bcaa) / F2;
                    xW2 = 1 - xI2 - xC2;

                    hasProduct = true;
                    break;
                case "Medio para BCAA":
                    rate = 50;
                    product = MEDIO_BCAA;
                    hasProduct = true;
                    break;

            }
        }
        else
        {
            switch (combo)
            {
                case "Lactobacillus + StreptococcusLeche":
                    product = "Leche rancia";
                    rate = 75;
                    hasProduct = true;
                    break;
                case "Leche":
                    product = "Leche rancia";
                    rate = 50;
                    hasProduct = true;
                    break;
                case "Saccharomyces cerevisiaeMedio para cerveza":
                    rate = 100;
                    product = "Cerveza podrida";
                    hasProduct = true;
                    break;
                case "Medio para cerveza":
                    rate = 50;
                    product = "Cerveza podrida";
                    hasProduct = true;
                    break;
                case "Saccharomyces cerevisiaeJugo de uva":
                    rate = 100;
                    product = "Vinagre";
                    hasProduct = true;
                    break;
                case "Jugo de uva":
                    rate = 50;
                    product = "Vinagre";
                    hasProduct = true;
                    break;
                case "ArthrospiraAgua de mar":
                    rate = 100;
                    product = "Agua verde";
                    hasProduct = true;
                    break;
                case "Agua de mar":
                    rate = 50;
                    product = "Agua verde";
                    hasProduct = true;
                    break;
                case "Corynebacterium glutamicumMedio para BCAA":
                    rate = 100;
                    product = "Medio contaminado";
                    hasProduct = true;
                    break;
                case "Medio para BCAA":
                    rate = 50;
                    product = "Medio contaminado";
                    hasProduct = true;
                    break;
            }
        }

    }

    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }

}
