using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySpawner : MonoBehaviour
{
    public GameObject liquidContainer;
    public GameObject inoculumContainer;
    public GameObject marmite;
    public GameObject bioreactor;
    public GameObject filter;
    public GameObject oven;
    public GameObject packager;
    public GameObject dryer;
    public GameObject grinder;

    public GameObject pInfo;

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;
    public Transform pos5;
    public Transform pos6;
    public Transform pos7;
    public Transform pos8;
    public Transform pos9;

    public Transform posToSpawn;

    public bool canBuy = false;

    static string LECHE = "Leche";
    static string YOGUR = "Yogur";
    static string YOGUR_F = "Yogur de fresa";
    static string S_FRESA = "Saborizante de fresa";
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


    public void Update()
    {
        VerifyFreeSpace();
    }

    public  void SpawnInoculum(string newInoculumType)
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject inoculumClone = Instantiate(inoculumContainer, posToSpawn) as GameObject;
                inoculumClone.transform.localPosition = Vector3.zero;
                inoculumClone.GetComponent<InoculumContainer>().SetInoculumType(newInoculumType);
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }

        }

    }

    public void SpawnContainerMilk()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject containerClone = Instantiate(liquidContainer, posToSpawn) as GameObject;
                containerClone.transform.localPosition = Vector3.zero;
                containerClone.GetComponent<Container>().SetType(LECHE);
                containerClone.GetComponent<Container>().SetQuantity(100);
                containerClone.GetComponent<Container>().SetCarbs(0.13f);
                //containerClone.GetComponent<Container>().SetFat(0.05f);
                //containerClone.GetComponent<Container>().SetProt(0.03f);
                containerClone.GetComponent<Container>().SetWater(0.87f);
                containerClone.GetComponent<Container>().SetInterestProd("Ninguno");
                containerClone.GetComponent<Container>().SetInterestPer(0f);
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnContainerSberryFlavor()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject containerClone = Instantiate(liquidContainer, posToSpawn) as GameObject;
                containerClone.transform.localPosition = Vector3.zero;
                containerClone.GetComponent<Container>().SetType(S_FRESA);
                containerClone.GetComponent<Container>().SetQuantity(1);
                containerClone.GetComponent<Container>().SetCarbs(1);
                //containerClone.GetComponent<Container>().SetFat(0.33f);
                //containerClone.GetComponent<Container>().SetProt(0.27f);
                containerClone.GetComponent<Container>().SetWater(0);
                containerClone.GetComponent<Container>().SetInterestProd("Ninguno");
                containerClone.GetComponent<Container>().SetInterestPer(0f);
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnContainerBarley()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject containerClone = Instantiate(liquidContainer, posToSpawn) as GameObject;
                containerClone.transform.localPosition = Vector3.zero;
                containerClone.GetComponent<Container>().SetType(CEBADA);
                containerClone.GetComponent<Container>().SetQuantity(100);
                containerClone.GetComponent<Container>().SetCarbs(0.87f);
                //containerClone.GetComponent<Container>().SetFat(0.33f);
                //containerClone.GetComponent<Container>().SetProt(0.27f);
                containerClone.GetComponent<Container>().SetWater(0.13f);
                containerClone.GetComponent<Container>().SetInterestProd("Ninguno");
                containerClone.GetComponent<Container>().SetInterestPer(0f);
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnContainerWater()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject containerClone = Instantiate(liquidContainer, posToSpawn) as GameObject;
                containerClone.transform.localPosition = Vector3.zero;
                containerClone.GetComponent<Container>().SetType(AGUA);
                containerClone.GetComponent<Container>().SetQuantity(100);
                containerClone.GetComponent<Container>().SetCarbs(0);
                //containerClone.GetComponent<Container>().SetFat(0.33f);
                //containerClone.GetComponent<Container>().SetProt(0.27f);
                containerClone.GetComponent<Container>().SetWater(1);
                containerClone.GetComponent<Container>().SetInterestProd("Ninguno");
                containerClone.GetComponent<Container>().SetInterestPer(0f);
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnContainerGrapes()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject containerClone = Instantiate(liquidContainer, posToSpawn) as GameObject;
                containerClone.transform.localPosition = Vector3.zero;
                containerClone.GetComponent<Container>().SetType(UVAS);
                containerClone.GetComponent<Container>().SetQuantity(100);
                containerClone.GetComponent<Container>().SetCarbs(0.19f);
                //containerClone.GetComponent<Container>().SetFat(0.33f);
                //containerClone.GetComponent<Container>().SetProt(0.27f);
                containerClone.GetComponent<Container>().SetWater(0.81f);
                containerClone.GetComponent<Container>().SetInterestProd("Ninguno");
                containerClone.GetComponent<Container>().SetInterestPer(0f);
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnContainerSeaWater()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject containerClone = Instantiate(liquidContainer, posToSpawn) as GameObject;
                containerClone.transform.localPosition = Vector3.zero;
                containerClone.GetComponent<Container>().SetType(AGUA_M);
                containerClone.GetComponent<Container>().SetQuantity(100);
                containerClone.GetComponent<Container>().SetCarbs(0.035f);
                //containerClone.GetComponent<Container>().SetFat(0.33f);
                //containerClone.GetComponent<Container>().SetProt(0.27f);
                containerClone.GetComponent<Container>().SetWater(0.965f);
                containerClone.GetComponent<Container>().SetInterestProd("Ninguno");
                containerClone.GetComponent<Container>().SetInterestPer(0f);
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnContainerBCAA()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject containerClone = Instantiate(liquidContainer, posToSpawn) as GameObject;
                containerClone.transform.localPosition = Vector3.zero;
                containerClone.GetComponent<Container>().SetType(MEDIO_BCAA);
                containerClone.GetComponent<Container>().SetQuantity(1);
                containerClone.GetComponent<Container>().SetCarbs(1);
                //containerClone.GetComponent<Container>().SetFat(0.33f);
                //containerClone.GetComponent<Container>().SetProt(0.27f);
                containerClone.GetComponent<Container>().SetWater(0);
                containerClone.GetComponent<Container>().SetInterestProd("Ninguno");
                containerClone.GetComponent<Container>().SetInterestPer(0f);
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnMarmite()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject marmiteClone = Instantiate(marmite, posToSpawn) as GameObject;
                marmiteClone.transform.localPosition = Vector3.zero;
                marmiteClone.GetComponent<Marmita>().SetMaxCapacity(1000);
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnBioreactor()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject bioreactorClone = Instantiate(bioreactor, posToSpawn) as GameObject;
                bioreactorClone.transform.localPosition = Vector3.zero;
                bioreactorClone.GetComponent<Biorreactor>().SetMaxCapacity(1000);
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnFilter()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject filterClone = Instantiate(filter, posToSpawn) as GameObject;
                filterClone.transform.localPosition = Vector3.zero;
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnOven()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject filterClone = Instantiate(oven, posToSpawn) as GameObject;
                filterClone.transform.localPosition = Vector3.zero;
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnPackager()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject packagerClone = Instantiate(packager, posToSpawn) as GameObject;
                packagerClone.transform.localPosition = Vector3.zero;
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnDryer()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject dryerClone = Instantiate(dryer, posToSpawn) as GameObject;
                dryerClone.transform.localPosition = Vector3.zero;
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void SpawnGrinder()
    {
        float cost = 0;

        VerifyCanBuy(cost);

        if (canBuy == true)
        {
            if (posToSpawn.GetComponent<PosChecker>().canSpawn == true)
            {
                GameObject grinderClone = Instantiate(grinder, posToSpawn) as GameObject;
                grinderClone.transform.localPosition = Vector3.zero;
                pInfo.GetComponent<PlayerInfo>().RestMoney(cost);
            }
            else
            {
                Debug.Log("Por favor despeja la zona de entrega");
            }
        }
    }

    public void VerifyCanBuy(float cost)
    {
        if (cost < pInfo.GetComponent<PlayerInfo>().money)
        {
            canBuy = true;
        }
        else
        {
            canBuy = false;
        }

    }

    public void VerifyFreeSpace()
    {
        if (pos1.GetComponent<PosChecker>().canSpawn == true)
        {
            posToSpawn = pos1;
        }
        else if (pos2.GetComponent<PosChecker>().canSpawn == true)
        {
            posToSpawn = pos2;
        }
        else if (pos3.GetComponent<PosChecker>().canSpawn == true)
        {
            posToSpawn = pos3;
        }
        else if (pos4.GetComponent<PosChecker>().canSpawn == true)
        {
            posToSpawn = pos4;
        }
        else if (pos5.GetComponent<PosChecker>().canSpawn == true)
        {
            posToSpawn = pos5;
        }
        else if (pos6.GetComponent<PosChecker>().canSpawn == true)
        {
            posToSpawn = pos6;
        }
        else if (pos7.GetComponent<PosChecker>().canSpawn == true)
        {
            posToSpawn = pos7;
        }
        else if (pos8.GetComponent<PosChecker>().canSpawn == true)
        {
            posToSpawn = pos8;
        }
        else if (pos9.GetComponent<PosChecker>().canSpawn == true)
        {
            posToSpawn = pos9;
        }
    }
}
