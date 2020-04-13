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

    // Initial State without any changes
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
        //Constantly rotating depends on currentSpeed variable;
        hand.RotateAround(cam.transform.position, cam.transform.up, currentSpeed);

        //Constantly updating the origin as the player is able to look around
        origin = resetPoint.transform.position;

        //All states surrounding a held mouse button
        if(Input.GetMouseButton(0))
        {
            //If the player is able to charge, a hit
            //this stage runs
            if(!rotating && canCharge && !returning)
            {
                //Timer starts here for a charge and multiplier
                time += Time.deltaTime;
                //Setting a velocity calculated by time held and 
                //a multiplier set in editor
                currentSpeed = (reloadSpeed) * time * timeMulti;
                //Color Change
                material.SetColor("_Color",color);
                //State variables set
                rotating = true;
                atOrigin = false;
                canCharge = false;
            }

            //If the player hits the origin point while rotating
            if(atOrigin && rotating)
            {
                //The speed is set to 0
                //and the object stops rotating
                //time multiplier reset
                currentSpeed = 0;
                rotating = false;
                time = 0;
            }
        
        }

        //This if statement will happen in reaction to releasing
        //the mouse button.
        if(!Input.GetMouseButton(0) && !canCharge)
        {
            //This wil begin returning the hand to the origin in the
            //reverse direction
            if((!returning && rotating) || (!returning && atOrigin))
            {
                //Time starts as return begins 
                time += Time.deltaTime;
                //As movement begins the hand is no longer at origin,
                //the color changes and the  speed is set to a 
                //negative value
                atOrigin = false;
                material.SetColor("_Color",returnColor);
                currentSpeed = (returnSpeed) * time * timeMulti;
                //The value is now returning but the value is not rotating
                returning = true;
                rotating = false;
                
            }

            //This will happen when the vvalue hits the original 
            //origin point, comming to a stop and changing approproate variables;
            else if(returning && atOrigin && !canCharge)
            {
                returning = false;
                time = 0;
                currentSpeed = 0;
                canCharge = true;
            }
        }

    }

    void OnTriggerEnter(Collider resetPoint) 
    {
        atOrigin = true;
    }
}
