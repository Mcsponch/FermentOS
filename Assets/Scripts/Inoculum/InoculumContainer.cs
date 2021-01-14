using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InoculumContainer : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    public string inoculumType = "";

    public void SetInoculumType(string newInoculumType)
    {
        inoculumType = newInoculumType;
    }
    

    void OnMouseDown()
    {
        // To drag
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
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
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }

}
