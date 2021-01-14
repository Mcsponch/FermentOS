using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoEventListener : MonoBehaviour
{

    public void CleanInfoUI()
    {
        Destroy(GameObject.Find("MarmiteInfoUI(Clone)"));
    }
}
