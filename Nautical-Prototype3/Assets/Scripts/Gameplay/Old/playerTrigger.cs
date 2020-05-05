using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTrigger : MonoBehaviour
{
    private string inRange;
    bool wheelLocked = false;
    void Update() {/*
        if (Input.GetButtonDown("Jump")){
            switch(inRange)
            {
                case "MainMast":
                    GameEvents.current.MastInteract();
                break;
                case "Mast2":
                    GameEvents.current.Mast2Interact();
                break;
                case "Wheel":
                    if (wheelLocked){
                        GameEvents.current.WheelInteract();
                        wheelLocked = false;
                    }else{
                        if (GameObject.FindGameObjectWithTag("ShipPivot").GetComponent<ShipMovAcc>().getWheelInUse()){
                            //To do: Implement Player Lock Event and Boat Control Event
                            
                        }else{
                            GameEvents.current.WheelInteract();
                            wheelLocked = true;
                            
                        }
                    }
                break;
            } 
        }*/
    }

    void OnTriggerEnter2D(Collider2D dataFromCollision) {
        if(!(dataFromCollision.gameObject.name.Equals("BoatPlayable"))){
            inRange = dataFromCollision.gameObject.name;
            Debug.Log(inRange);
        }
        
    }
    void OnTriggerExit2D(Collider2D dataFromCollision) {
        if(!(dataFromCollision.gameObject.name.Equals("BoatPlayable"))){
            inRange = null;
            Debug.Log(inRange);
        }
    }
}
