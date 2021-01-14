using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dryer : MonoBehaviour
{
    public GameObject container;
    public Transform deliveryPos0;

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

    Animator animator;
    AudioSource sound;

    const string UNINSTALLED = "DryerIdle";
    const string INSTALLED = "DryerInstall";
    const string WORKING = "DryerWorking";

    // Start is called before the first frame update
    void Start()
    {
        maxCapacity = 1000f;
        deliveryPos0 = gameObject.transform.GetChild(0);
        rate = 100f;
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
        F2s = "";
        F3s = "";
        canAddMat = true;
        canAddQty = true;
        rate = 100f;
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
                rate = 200f;

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
                rate = 60f;

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
            else if (collision.gameObject.GetComponent<Container>().status == "Seco")
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

        }
        else
        {
            canAddMat = false;
        }

    }

    public void ProductChecker()
    {
        
                F2s = F1s;
                F3s = "Agua";
                interestP2 = "";
                interestP3 = "";
                xW3 = 1f;
                xW2 = 0.03F;
                F2 = (F1 * xW1 - F1 * xW3) / (xW2 - xW3);
                xC2 = F1 * xC1 / F2;
                xF2 = F1 * xF1 / F2;
                xP2 = F1 * xP1 / F2;
                xI2 = F1 * xI1 / F2;
                F3 = F1 - F2;
                

    }

    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }
}
