using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Rigidbody2D))]
public class ShipMovAcc : MonoBehaviour
{
    public int mainSail = 0;
    private bool vCooldown;
    public float forwardVelocity, forwardAcceleration, angularVel;
    public float  maxAngVel = 20;

    private bool mSail, sSail;
    public int wheelInUse;

    //For Manual Control
    public bool manualControl = false;
        float InputX;
        //float InputY;

    // Start is called before the first frame update
    private void Start(){
        GameEvents.current.onMastInteract += mSailStateChange;
        GameEvents.current.onMast2Interact += sSailStateChange;
    }
    void Awake()
    {
        //Set default Values
        mainSail = 0;
        vCooldown = false;
        angularVel = 0;
        maxAngVel = 20;
        wheelInUse = 0;
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
    public int getWheelInUse(){
        return wheelInUse;
    }
    public void setWheelInUse(int x){
        wheelInUse = x;
    }
    public void setInput(float x){
        InputX = x;
    }
    //update for physics calculations
    void FixedUpdate()
    {
        //Ship Angle Calculations
        GetComponent<Rigidbody2D>().angularVelocity = angularVel;
        float shipAngle = transform.localRotation.eulerAngles.z;
        //Forwad Velocity.
        calculateForwardVelocity();
        GetComponent<Rigidbody2D>().velocity= seperateVelocity(shipAngle, forwardVelocity);
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
        switch(mainSail){
            case 0:
            if (forwardVelocity>0){forwardVelocity += forwardAcceleration;}
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
