using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handMovement : MonoBehaviour
{
    public Material material;
    public Color color;
    public Color returnColor;
    public Transform hand;
    public GameObject cam;
    public Transform resetPoint;
    public float reloadSpeed;
    Vector3 origin;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private bool atOrigin;
     [SerializeField]
    private bool rotating;

    // Start is called before the first frame update
    void Start()
    {
        origin = resetPoint.transform.position;
        currentSpeed = 0;
        rotating = false;
        atOrigin = true;
    }

    // Update is called once per frame
    void Update()
    {
        hand.RotateAround(cam.transform.position, cam.transform.up, currentSpeed);
        origin = resetPoint.transform.position;

        if(hand.position == origin)
        {
            atOrigin = true;
        }
        else
        {
            atOrigin = false;
        }

        if(Input.GetMouseButton(0))
        {

            if(atOrigin && !rotating)
            {
                currentSpeed = reloadSpeed;
                material.SetColor("_Color",color);
                rotating = true;
            }
            if(hand.position == origin)
            {
                atOrigin = true;
            }
            else
            {
                atOrigin = false;
            }

            
            if(rotating && atOrigin)
            {
                currentSpeed = 0;
                rotating = false;
            }

            // if(currentSpeed != 0)
            // {
            //     currentSpeed = reloadSpeed;
            // }
            
            // isRotate = true;
            // hand.RotateAround(cam.transform.position, cam.transform.up, currentSpeed);
            // if (hand.position == origin && isRotate)
            // {
            //     Debug.Log("BEIG PEEEn");
            //     currentSpeed = 0;
            //     isRotate = false;
            // }
            
        }
        else
        {

        }
        // else
        // {
        //     material.SetColor("_Color",returnColor);
        //     hand.transform.position = origin;
        // }
    }
}
