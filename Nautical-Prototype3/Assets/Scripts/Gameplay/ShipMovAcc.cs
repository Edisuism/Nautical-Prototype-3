using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Rigidbody2D))]
public class ShipMovAcc : MonoBehaviour
{
    public int mainSail = 0;
    private bool vCooldown;
    public float forwardVelocity, forwardAcceleration, angularVel;
    private Vector2 hitforce = new Vector2(0f,0f);
    public float  maxAngVel = 20;

    private bool mSail, sSail;
    public int wheelInUse;
    private bool anchor, setSail;

    //For Manual Control
    public bool manualControl = false;
        float InputX;
        //float InputY;

    // Start is called before the first frame update
    private void Start(){
        GameEvents.current.onMastInteract += mSailStateChange;
        GameEvents.current.onMast2Interact += sSailStateChange;
        GameEvents.current.onAnchorInteract += setAnchor;
    }
    void Awake()
    {
        //Set default Values
        mainSail = 0;
        vCooldown = false;
        angularVel = 0;
        maxAngVel = 20;
        wheelInUse = 0;
        anchor = true;
        setSail = false;
    }

    // Update is called once per frame
    void Update(){
        if(manualControl){
            //Get inputs
            //InputX = -1 *(Input.GetAxisRaw("Horizontal"));
            //InputY = Input.GetAxisRaw("Vertical");
            //mainSailDeploy(InputY);
            currentAngularVelocity(InputX);
        }else{
            setForwardAcceleration();
            if(wheelInUse != 0){
                //InputX = -1 *(Input.GetAxisRaw("Horizontal"));
                currentAngularVelocity(InputX);
            }
        }
    }
    public Vector3 getEulerAngles(){
        return transform.eulerAngles;
    }
    public int getWheelInUse(){
        return wheelInUse;
    }
    public void setWheelInUse(int x){
        wheelInUse = x;
    }
    public void setAnchor(){
        anchor = !anchor;
        if(setSail == false){
            setSail = true;
        }
    }
    public void setInput(float x){
        InputX = x;
    }
    public bool getSetSail(){
        return setSail;
    }
    //update for physics calculations
    void FixedUpdate()
    {
        //Ship Angle Calculations
        if(!anchor){GetComponent<Rigidbody2D>().angularVelocity = angularVel;}
        else{GetComponent<Rigidbody2D>().angularVelocity = 0;}
        float shipAngle = transform.localRotation.eulerAngles.z;
        //Forwad Velocity.
        calculateForwardVelocity();
        //Work out collision bounce
        updateHitForce();
        GetComponent<Rigidbody2D>().velocity = seperateVelocity(shipAngle, forwardVelocity) - hitforce;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //Gets the direction to apply the force
        Vector2 collisionDir = other.transform.position - gameObject.transform.position;
        hitforce = hitforce + (collisionDir * 3);
        //If I have nothing else to do I'll work out ang velocity
    }
    private void updateHitForce(){
        if(hitforce != new Vector2(0,0)){
            Debug.Log(hitforce);
            //reduces the force ammount each frame
            //Sub function didn't work for some reason so we have this nightmare
            if(Mathf.Abs(hitforce.x) < 0.5f){
                hitforce.x = 0;
            }else if(hitforce.x >= 0.5){
                hitforce.x = hitforce.x - 0.5f;
            }else{
                hitforce.x = hitforce.x + 0.5f;
            }
            if(Mathf.Abs(hitforce.y) < 0.5f){
                hitforce.y = 0;
            }else if(hitforce.y >= 0.5){
                hitforce.y = hitforce.y - 0.5f;
            }else{
                hitforce.y = hitforce.y + 0.5f;
            }
        }
    }
    void currentAngularVelocity(float i){
        angularVel = angularVel + i/3;
        if (Mathf.Abs(angularVel) > maxAngVel){
            if(angularVel > 0){
                angularVel = maxAngVel;
            }else{
                angularVel = maxAngVel * -1;
            }
        }
    }

    void mainSailDeploy(float i){
        /*if (Input.GetButtonDown("Jump")){
            mainSail++;
            if(mainSail == 3){mainSail = 0;}
        }*/
        setForwardAcceleration();
    }
    private void mSailStateChange(){
        if(mSail){mainSail--;}
        else{mainSail++;}
        mSail = !mSail;
        Debug.Log("Main Sail state changed");
    }
    void sSailStateChange(){
        if(sSail){mainSail--;}
        else{mainSail++;}
        sSail = !sSail;
        Debug.Log("Second Sail state changed");
    }
    
    void setForwardAcceleration(){
        switch(mainSail){
            case 0:
            forwardAcceleration = -0.1f;
            maxAngVel = 10;
            break;
            case 1:
            forwardAcceleration = 0.5f;
            maxAngVel = 30;
            break;
            case 2:
            forwardAcceleration = 0.1f;
            maxAngVel = 20;
            break;
        }
    }
    void calculateForwardVelocity(){
        int x = mainSail;
        if(anchor == true){
            x = 0;
        }
        switch(x){
            case 0:
            if (forwardVelocity>0)
            {
                if(anchor == true){forwardAcceleration = -5;}
                forwardVelocity += (forwardAcceleration);
            }else {
                forwardVelocity = 0;
            }
            break;
            case 1:
            if (forwardVelocity<6){forwardVelocity += forwardAcceleration;}
            break;
            case 2:
            if (forwardVelocity<12){forwardVelocity += forwardAcceleration;}
            break;
             
        }
    }

    Vector2 seperateVelocity(float angle, float radius){
        float vx = radius * Mathf.Sin(ConvertToRadians(angle)) *-1;
        float vy = radius * Mathf.Cos(ConvertToRadians(angle));
        return new Vector2(vx,vy);
    }
    public float ConvertToRadians(float angle)
    {
        return (Mathf.PI / 180) * angle;
    }
}
