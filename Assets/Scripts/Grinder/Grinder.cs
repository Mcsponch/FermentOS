using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder : MonoBehaviour
{
    public GameObject container;
    public Transform deliveryPos0;
    Collider2D toGrind;

    private Vector3 mOffset;
    private float mZCoord;

    public float F1;

    public float maxCapacity;
    public float rate;
    public float timer;
    public float timeToComplete;
    public float timeRemaining;

    public string F1s = "";
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

    Animator animator;
    AudioSource sound;

    const string UNINSTALLED = "GrinderIdle";
    const string INSTALLED = "GrinderInstall";
    const string WORKING = "GrinderWorking";

    // Start is called before the first frame update
    void Start()
    {
        maxCapacity = 1000f;
        deliveryPos0 = gameObject.transform.GetChild(0);
        rate = 30;
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
                timeRemaining = timeToComplete - timer;
            }
            else if (timer >= timeToComplete)
            {
                ProductChecker();
                toGrind.GetComponent<Container>().type = F1s;
                toGrind.gameObject.SetActive(true);


                timer = 0;
                processStarted = false;
                DrainOnClick();
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
        canAddMat = true;
        canAddQty = true;
        toGrind = null;
        hasProduct = false;
        sound.Stop();
    }

    public void StartProcessOnClick()
    {
        if (processStarted == false && canMove == false && F1 > 0)
        {
            sound.Play();
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
            errorMsg = "";
                float f1Amount = collision.GetComponent<Container>().quantity;
                string f1Type = collision.GetComponent<Container>().type;
                toGrind = collision;

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
                        toGrind.gameObject.SetActive(false);
                        toGrind.transform.SetParent(deliveryPos0);
                        toGrind.transform.localPosition = Vector3.zero;
                    }

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
            case "Malta":
                F1s = MALTA_M;
                hasProduct = true;
                break;
            case "Uvas":
                F1s = JUGO_UVA;
                hasProduct = true;
                break;

        }

        if (hasProduct == false)
        {
            F1s = F1s + " molido";
        }

    }

    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }

}
