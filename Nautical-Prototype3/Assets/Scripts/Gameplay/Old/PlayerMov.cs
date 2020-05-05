using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent (typeof (Rigidbody2D))]
public class PlayerMov : MonoBehaviour
{

    public float playerSpeed = 2f;
    private Vector2 moveInput;
    //public Rigidbody2D ShipRB2D;
    private bool disableMove;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onWheelInteract += changeDisableState;
        disableMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!disableMove){
            //Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            GetComponent<Rigidbody2D>().velocity = (moveInput * playerSpeed);
        }
    }

    void changeDisableState(){
        disableMove = !disableMove;
    }

    private void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }
    private void OnInteract(){
        Debug.Log("I");
    }
}
