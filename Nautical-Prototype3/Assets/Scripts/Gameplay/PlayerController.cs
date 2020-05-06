using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent (typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private int playerID;
    public float playerSpeed = 2f;
    private Vector2 moveInput;
    //public Rigidbody2D ShipRB2D;
    private string inRange;

    public GameObject mimic;

    // Start is called before the first frame update
    public int getID(){
        return playerID;
    }
    void Awake()
    {
        if(GameObject.FindGameObjectWithTag("ShipPivot").GetComponent<ShipMovAcc>().getSetSail()){
            Destroy(gameObject);
        }else{
        //Add observer functionality
        //GameEvents.current.onWheelInteract += changeDisableState;
        //set ID based on Players present
        GameObject[] playerCount = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in playerCount){
            playerID++;
        }
        //TODO Instantiate Mimic
        var Object = GameObject.Instantiate(mimic);
        //Object.GetComponent<MimicPlayerLocation>().SetPlayerToMimic(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If movement not disabled due to wheel use
        if(!wheelLocked(playerID)){
            //Move player via velocity component
            GetComponent<Rigidbody2D>().velocity = (moveInput * playerSpeed);
        }else{
            Debug.Log(playerID + " is on the wheel");
            GameObject.FindGameObjectWithTag("ShipPivot").GetComponent<ShipMovAcc>().setInput(moveInput.x * -1);
            GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
            transform.position = new Vector3(0,-3,0);
        }
    }
    //New Input System functions
    private void OnMove(InputValue value){
        //Store controller stick/keyboard wasd inputs to be used in update
        moveInput = value.Get<Vector2>();
    }
    private void OnInteract(){
        //Function runs when interact button is pressed
        //Switch statement based on
        switch(inRange)
            {
                case "MainMast":
                    GameEvents.current.MastInteract();
                break;
                case "Mast2":
                    GameEvents.current.Mast2Interact();
                break;
                case "Anchor":
                    GameEvents.current.AnchorInteract();
                break;
                case "Wheel":
                    if (wheelLocked(playerID)){
                        GameEvents.current.WheelInteract();
                        GameObject.FindGameObjectWithTag("ShipPivot").GetComponent<ShipMovAcc>().setWheelInUse(0);
                        //wheelLocked = false;
                    }else{
                        
                        if (wheelLocked(0)){
                            //If wheel is not in use
                            Debug.Log("YAD");
                            GameEvents.current.WheelInteract();
                            GameObject.FindGameObjectWithTag("ShipPivot").GetComponent<ShipMovAcc>().setWheelInUse(playerID);
                        }else{
                            Debug.Log("Else");
                        }
                    }
                break;
            } 
    }
    bool wheelLocked(int x){
        return GameObject.FindGameObjectWithTag("ShipPivot").GetComponent<ShipMovAcc>().getWheelInUse() == x;
    }
    //Store nearest object thats interactable
    void OnTriggerEnter2D(Collider2D dataFromCollision) {
        if(!(dataFromCollision.gameObject.name.Equals("BoatPlayable"))){
            inRange = dataFromCollision.gameObject.name;
            Debug.Log(inRange);
        }
        
    }
    //reset when out of range
    void OnTriggerExit2D(Collider2D dataFromCollision) {
        if(!(dataFromCollision.gameObject.name.Equals("BoatPlayable"))){
            inRange = null;
            Debug.Log(inRange);
        }
    }
}
