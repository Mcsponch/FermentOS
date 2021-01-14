using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosChecker : MonoBehaviour
{
    public bool canSpawn = true;

    public void OnTriggerStay2D(Collider2D collision)
    {
        canSpawn = false;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        canSpawn = true;
    }

    private void OnMouseDown()
    {
        Debug.Log("Esta arriba");
    }
}
