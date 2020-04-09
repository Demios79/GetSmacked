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
    public Collider resetPoint;
    public float reloadSpeed;
    public float returnSpeed;
    Vector3 origin;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private bool atOrigin;
     [SerializeField]
    private bool rotating;
    [SerializeField]
    private bool returning;
    private bool canCharge;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = 0;
        rotating = false;
        atOrigin = true;
        returning = false;
        canCharge = true;
    }

    // Update is called once per frame
    void Update()
    {
        hand.RotateAround(cam.transform.position, cam.transform.up, currentSpeed);
        origin = resetPoint.transform.position;

        if(Input.GetMouseButton(0))
        {

            if(!rotating && canCharge && !returning)
            {
                currentSpeed = reloadSpeed;
                material.SetColor("_Color",color);
                rotating = true;
                atOrigin = false;
                canCharge = false;
            }

            if(atOrigin && rotating)
            {
                currentSpeed = 0;
                rotating = false;
            }
        
        }

        if(!Input.GetMouseButton(0) && !canCharge)
        {
            if((!returning && rotating) || (!returning && atOrigin))
            {
                atOrigin = false;
                material.SetColor("_Color",returnColor);
                currentSpeed = returnSpeed;
                returning = true;
                
            }

            else if(returning && atOrigin)
            {
                canCharge = true;
                returning = false;
                currentSpeed = 0;
            }
        }

    }

    void OnTriggerEnter(Collider resetPoint) 
    {
        atOrigin = true;
    }
}
