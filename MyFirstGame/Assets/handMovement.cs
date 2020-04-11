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
    public int timeMulti;
    private Vector3 origin;
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private bool atOrigin;
     [SerializeField]
    private bool rotating;
    [SerializeField]
    private bool returning;
    [SerializeField]
    private bool canCharge;
    private float accel;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = 0;
        rotating = false;
        atOrigin = true;
        returning = false;
        canCharge = true;
        time = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hand.RotateAround(cam.transform.position, cam.transform.up, currentSpeed);
        origin = resetPoint.transform.position;

        if(Input.GetMouseButton(0))
        {

            if(!rotating && canCharge && !returning)
            {
                time += Time.deltaTime;
                currentSpeed = (reloadSpeed) * time * timeMulti;
                material.SetColor("_Color",color);
                rotating = true;
                atOrigin = false;
                canCharge = false;
            }

            if(atOrigin && rotating)
            {
                currentSpeed = 0;
                rotating = false;
                time = 0;
            }
        
        }

        if(!Input.GetMouseButton(0) && !canCharge)
        {
            if((!returning && rotating) || (!returning && atOrigin))
            {
                time += Time.deltaTime;
                atOrigin = false;
                material.SetColor("_Color",returnColor);
                currentSpeed = (returnSpeed) * time * timeMulti;
                returning = true;
                rotating = false;
                
            }

            else if(returning && atOrigin)
            {
                canCharge = true;
                returning = false;
                time = 0;
                currentSpeed = 0;
            }
        }

    }

    void OnTriggerEnter(Collider resetPoint) 
    {
        atOrigin = true;
    }
}
