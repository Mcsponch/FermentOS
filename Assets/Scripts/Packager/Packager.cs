using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Packager : MonoBehaviour
{
    Transform delivery;
    Collider2D toPackage;

    private Vector3 mOffset;
    private float mZCoord;

    public float maxCapacity;
    public float rate;
    public float timer;
    public float timeToComplete;
    public float timeRemaining;
    public float F1;

    public string status = "No instalada";
    public string errorMsg = "";

    string NO_INSTALADA = "No instalada";
    string LISTO_PARA_OPERAR = "Listo para operar";
    string PROCESO_INICIADO = "Proceso iniciado";

    public string F1s;
    public bool canAddMat = true;
    public bool canMove = true;
    public bool processStarted = false;
    public bool canInstall = true;

    Animator animator;
    AudioSource sound;

    const string UNINSTALLED = "PackagerIdle";
    const string INSTALLED = "PackagerInstall";
    const string WORKING = "PackagerWorking";


    // Start is called before the first frame update
    void Start()
    {
        maxCapacity = 1000f;
        delivery = gameObject.transform.GetChild(0);
        rate = 50f;
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

                toPackage.tag = "Packed";
                toPackage.gameObject.SetActive(true);
                toPackage.GetComponent<Container>().status = "Empacado";


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
        toPackage.gameObject.SetActive(true);
        sound.Stop();
    }

    public void StartOnClick()
    {
        if (processStarted == false && canMove == false && F1 > 0)
        {
            sound.Play();
            processStarted = true;
            canAddMat = false;
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
            if (canMove == false && collision.GetComponent<Container>().quantity <= maxCapacity && canAddMat == true)
            {
                toPackage = collision;
                canAddMat = false;
                F1 = toPackage.GetComponent<Container>().quantity;
                F1s = toPackage.GetComponent<Container>().type;
                toPackage.gameObject.SetActive(false);
                toPackage.transform.SetParent(delivery);
                toPackage.transform.localPosition = Vector3.zero;

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
            ChangeAnimationState(WORKING);
            status = PROCESO_INICIADO;
            
        }

    }

    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }
}
