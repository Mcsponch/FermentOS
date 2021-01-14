using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    

    private Vector3 mOffset;
    private float mZCoord;

    public float quantity;

    // composition
    public float carbs;
    public float fat;
    public float prot;
    public float water;

    public string interestProd;
    public float interestPer;

    public string status;

    public string type;

   
    public void SetType(string newType)
    {
        type = newType;
    }

    public void SetQuantity(float newQuantity)
    {
        quantity = newQuantity;
    }

    public void SetCarbs(float newQuantity)
    {
        carbs = newQuantity;
    }

    public void SetProt(float newQuantity)
    {
        prot = newQuantity;
    }

    public void SetFat(float newQuantity)
    {
        fat = newQuantity;
    }

    public void SetWater(float newQuantity)
    {
        water = newQuantity;
    }

    public void SetInterestProd(string newType)
    {
        interestProd = newType;
    }

    public void SetInterestPer(float newQuantity)
    {
        interestPer = newQuantity;
    }

    public void StatusSetter()
    {
        if (water <= 0.20)
        {
            status = "Seco";
        }
        else if (water <= 0.65)
        {
            status = "Húmedo";
        }
        else if (water <= 1)
        {
            status = "Liquido";
        }
    }

    private void Start()
    {
       
        StatusSetter();
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
