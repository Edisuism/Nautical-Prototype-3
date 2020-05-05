using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MastAnimator : MonoBehaviour
{
    private bool mastOn = false;
    public bool upper;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        if (!upper){
            GameEvents.current.onMastInteract += SailStateChange;
        }else{
            GameEvents.current.onMast2Interact += Sail2StateChange;
        }
    }
    void SailStateChange(){
        Debug.Log("SAIL");
        if(!mastOn){
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }else{
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        mastOn = !mastOn;
    }
    void Sail2StateChange(){
        Debug.Log("SAIL");
        if(!mastOn){
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }else{
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        mastOn = !mastOn;
    }
}
