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
    private int carrying = 0;
    public GameObject mimic;
    Animator anim;
    float lastdirx,lastdiry;
    public RuntimeAnimatorController[] animSets;
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
        anim = this.GetComponent<Animator>();
        if(playerID%2 != 0){
            anim.runtimeAnimatorController = animSets[0];
        }else{
            anim.runtimeAnimatorController = animSets[1];
        }
        
        carrying = 0;
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
            //Debug.Log(playerID + " is on the wheel");
            GameObject.FindGameObjectWithTag("ShipPivot").GetComponent<ShipMovAcc>().setInput(moveInput.x * -1);
            transform.position = new Vector2(0,-3);
        }
        //animatorHandler();
        
    }
    private void animatorHandler(){
        //Sets animation: Kina jank atm
        //needs to be maintained on mimic side
        anim.SetBool("Wheel", wheelLocked(playerID));

            if(moveInput.x != 0){
                lastdirx = moveInput.x;
            }
            anim.SetFloat("Horizontal", moveInput.x);
            if(moveInput.y != 0){
                lastdirx = moveInput.y;
                if(moveInput.x == 0){
                    lastdirx = 0;
                }
            }
            anim.SetFloat("Vertical", moveInput.y);
            if(moveInput.x == 0 && moveInput.y == 0){
                anim.SetFloat("Horizontal", lastdirx);
                anim.SetFloat("Vertical", lastdiry);
            }
        float s = Mathf.Abs(moveInput.x) + Mathf.Abs(moveInput.y);
        anim.SetFloat("Speed", s);
    }
    public Vector2 getInputs(){
        return moveInput;
    }
    public bool getWheelLocked(){
        return wheelLocked(playerID);
    }
    public int getCarrying(){
        return carrying;
    }
    //New Input System functions
    private void OnMove(InputValue value){
        //Store controller stick/keyboard wasd inputs to be used in update
        moveInput = value.Get<Vector2>();
    }
    private void OnInteract(){
        //Function runs when interact button is pressed
        //Switch statement based on
        if(carrying == 0){
            switch(inRange){
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
                            GameEvents.current.WheelInteract();
                            GameObject.FindGameObjectWithTag("ShipPivot").GetComponent<ShipMovAcc>().setWheelInUse(playerID);
                        }else{
                        }
                    }
                break;
                case "Materials":
                    carrying = 1;
                    Debug.Log("Pickup");
                break;
            }
        }else{
            Debug.Log("Cannot interact while carrying");
        } 
    }
    private void OnDrop(){
        carrying = 0;
        Debug.Log("Drop");
        if (wheelLocked(playerID)){
                        GameEvents.current.WheelInteract();
                        GameObject.FindGameObjectWithTag("ShipPivot").GetComponent<ShipMovAcc>().setWheelInUse(0);
        }
    }
    bool wheelLocked(int x){
        return GameObject.FindGameObjectWithTag("ShipPivot").GetComponent<ShipMovAcc>().getWheelInUse() == x;
    }
    //Store nearest object thats interactable
    void OnTriggerEnter2D(Collider2D dataFromCollision) {
            if(!colIgnore(dataFromCollision.name)){
                inRange = dataFromCollision.gameObject.name;
            }
                
    }
    bool colIgnore(string name){
        //Make sure collisions was only with interactables
        switch(name){
            case "BoatPlayable":
                return true;
                case "Pirate(Clone)":
                return true;
        }
        return false; 
    }
    //reset when out of range
    void OnTriggerExit2D(Collider2D dataFromCollision) {
        if(!(dataFromCollision.gameObject.name.Equals("BoatPlayable"))){
            inRange = null;
        }
    }
}
