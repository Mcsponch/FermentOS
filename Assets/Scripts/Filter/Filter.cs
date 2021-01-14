using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour
{
    public GameObject container;
    public Transform deliveryPos0;
    public Transform deliveryPos1;

    private Vector3 mOffset;
    private float mZCoord;

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

    public float maxCapacity;
    public float rate;
    public float timer;
    public float timeToComplete;
    public float timeRemaining;

    public string F1s = "";
    public string F2s = "";
    public string F3s = "";
    public string status = "No instalada";
    public string errorMsg = "";

    string NO_INSTALADA = "No instalada";
    string LISTO_PARA_OPERAR = "Listo para operar";
    string PROCESO_INICIADO = "Proceso iniciado";

    public bool canAddQty = true;
    public bool canAddMat = true;
    public bool canMove = true;
    public bool processStarted = false;
    public bool canInstall = true;
    public bool hasProducts = false;

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

    Animator animator;
    AudioSource sound;

    const string UNINSTALLED = "FilterIdle";
    const string INSTALLED = "FilterInstall";
    const string WORKING = "FilterWorking";

    // Start is called before the first frame update
    void Start()
    {
        maxCapacity = 1000f;
        deliveryPos0 = gameObject.transform.GetChild(0);
        deliveryPos1 = gameObject.transform.GetChild(1);
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
                timeRemaining = Mathf.Round(timeToComplete - timer);
            }
            else if (timer >= timeToComplete)
            {
                if (hasProducts == true)
                {
                    GameObject containerF2Clone = Instantiate(container, deliveryPos0) as GameObject;
                    containerF2Clone.transform.localPosition = Vector3.zero;
                    containerF2Clone.GetComponent<Container>().SetType(F2s);
                    containerF2Clone.GetComponent<Container>().SetQuantity(F2);
                    containerF2Clone.GetComponent<Container>().SetCarbs(xC2);
                    containerF2Clone.GetComponent<Container>().SetProt(xP2);
                    containerF2Clone.GetComponent<Container>().SetFat(xF2);
                    containerF2Clone.GetComponent<Container>().SetWater(xW2);
                    containerF2Clone.GetComponent<Container>().SetInterestPer(xI2);
                    containerF2Clone.GetComponent<Container>().SetInterestProd(interestP2);

                    GameObject containerF3Clone = Instantiate(container, deliveryPos1) as GameObject;
                    containerF3Clone.transform.localPosition = Vector3.zero;
                    containerF3Clone.GetComponent<Container>().SetType(F3s);
                    containerF3Clone.GetComponent<Container>().SetQuantity(F3);
                    containerF3Clone.GetComponent<Container>().SetCarbs(xC3);
                    containerF3Clone.GetComponent<Container>().SetProt(xP3);
                    containerF3Clone.GetComponent<Container>().SetFat(xF3);
                    containerF3Clone.GetComponent<Container>().SetWater(xW3);
                    containerF3Clone.GetComponent<Container>().SetInterestPer(xI3);
                    containerF3Clone.GetComponent<Container>().SetInterestProd(interestP3);

                    timer = 0;
                    processStarted = false;
                    DrainOnClick();

                }
                else
                {
                    
                    GameObject containerF2Clone = Instantiate(container, deliveryPos0) as GameObject;
                    containerF2Clone.transform.localPosition = Vector3.zero;
                    containerF2Clone.GetComponent<Container>().SetType(F2s);
                    containerF2Clone.GetComponent<Container>().SetQuantity(F2);
                    containerF2Clone.GetComponent<Container>().SetCarbs(xC2);
                    containerF2Clone.GetComponent<Container>().SetProt(xP2);
                    containerF2Clone.GetComponent<Container>().SetFat(xF2);
                    containerF2Clone.GetComponent<Container>().SetWater(xW2);
                    containerF2Clone.GetComponent<Container>().SetInterestPer(xI2);
                    containerF2Clone.GetComponent<Container>().SetInterestProd(interestP2);

                    GameObject containerF3Clone = Instantiate(container, deliveryPos1) as GameObject;
                    containerF3Clone.transform.localPosition = Vector3.zero;
                    containerF3Clone.GetComponent<Container>().SetType(F3s);
                    containerF3Clone.GetComponent<Container>().SetQuantity(F3);
                    containerF3Clone.GetComponent<Container>().SetCarbs(xC3);
                    containerF3Clone.GetComponent<Container>().SetProt(xP3);
                    containerF3Clone.GetComponent<Container>().SetFat(xF3);
                    containerF3Clone.GetComponent<Container>().SetWater(xW3);
                    containerF3Clone.GetComponent<Container>().SetInterestPer(xI3);
                    containerF3Clone.GetComponent<Container>().SetInterestProd(interestP3);

                    timer = 0;
                    processStarted = false;
                    DrainOnClick();
                }

            }

        }

    }

    public void InstallOnClick()
    {
        if (canMove == true && canInstall == true)
        {
            canMove = false;
        }
        else if (F1 == 0)
        {
            canMove = true;
        }

    }

    public void DrainOnClick()
    {
        F1 = 0;
        F1s = "";
        F2s = "";
        F3s = "";
        canAddMat = true;
        canAddQty = true;
        hasProducts = false;
        sound.Stop();
    }

    public void StartProcessOnClick()
    {
        if (processStarted == false && canMove == false && F1 > 0)
        {
            sound.Play();
            ProductChecker();
            processStarted = true;
            canAddMat = false;
            canAddQty = false;
            timeToComplete = F1 / rate;

        }
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
            if (collision.gameObject.GetComponent<Container>().status == "Liquido")
            {
                errorMsg = "";
                float f1Amount = collision.GetComponent<Container>().quantity;
                string f1Type = collision.GetComponent<Container>().type;

                // Verify it doesn't exceed max capacity
                if (F1 + f1Amount < maxCapacity)
                {
                    canAddQty = true;
                }
                else if (F1 + f1Amount > maxCapacity)
                {
                    canAddQty = false;
                }

                if (canAddMat == true && canMove == false)
                {
                    SetMatType(collision);
                }

                if (canAddQty == true)
                {
                    if (f1Type == F1s)
                    {
                        F1 += f1Amount;
                        Destroy(collision.gameObject);
                    }

                }

                ProductChecker();
               
            }
            else if (collision.gameObject.GetComponent<Container>().status == "Húmedo")
            {
                errorMsg = "Muy seco, usa otro equipo";
            }
            else if (collision.gameObject.GetComponent<Container>().status == "Seco")
            {
                errorMsg = "Demasiado seco, usa otro equipo";
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
        if (F1s == "")
        {
            F1s = collision.GetComponent<Container>().type;
            xC1 = collision.GetComponent<Container>().carbs;
            xF1 = collision.GetComponent<Container>().fat;
            xP1 = collision.GetComponent<Container>().prot;
            xW1 = collision.GetComponent<Container>().water;
            xI1 = collision.GetComponent<Container>().interestPer;
            interestP1 = collision.GetComponent<Container>().interestProd;

        }
        else
        {
            canAddMat = false;
        }

    }

    public void ProductChecker()
    {
        switch (F1s)
        {
            case "Cerveza con sólidos":
                F2s = "Mosto";
                F3s = CERVEZA;
                interestP2 = "";
                interestP3 = interestP1;
                float alcohol = F1 * xI1;
                float liquid = F1 * xW1;
                float mosto = F1 * xC1;
                F2 = mosto + (0.15f * liquid);
                F3 = F1 - F2;
                xI3 = alcohol / F3;
                xW3 = 1 - xI3;
                xC2 = mosto / F2;
                xW2 = 1 - xC2;
                rate = 100f;
                hasProducts = true;
                break;

            case "Uvas molidas":
                F2s = "Residuos de uva";
                F3s = JUGO_UVA;
                interestP2 = "";
                interestP3 = "";
                xW2 = 0.15F;
                xW3 = 1f;
                F2 = (F1 * xW1 - F1 * xW3) / (xW2 - xW3);
                xC2 = F1 * xC1 / F2;
                xF2 = F1 * xF1 / F2;
                xP2 = F1 * xP1 / F2;
                xI2 = F1 * xI1 / F2;
                F3 = F1 - F2;
                rate = 100f;
                hasProducts = true;
                break;

            

        }

        if (hasProducts == false)
        {
            F2s = F1s;
            F3s = AGUA;
            interestP2 = interestP1;
            interestP3 = "";
            xW2 = 0.35F;
            xW3 = 1f;
            F2 = (F1 * xW1 - F1 * xW3) / (xW2 - xW3);
            xC2 = F1 * xC1 / F2;
            xF2 = F1 * xF1 / F2;
            xP2 = F1 * xP1 / F2;
            xI2 = F1 * xI1 / F2;
            xC3 = 0;
            xF3 = 0;
            xP3 = 0;
            xI3 = 0;

            F3 = F1 - F2;
            rate = 100f;
        }

    }

    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }
}
