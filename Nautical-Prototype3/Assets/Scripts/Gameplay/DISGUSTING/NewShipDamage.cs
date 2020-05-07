using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShipDamage : MonoBehaviour
{
    public int x;
    // Start is called before the first frame update
    void Start()
    {
        switch(x){
            case(0):
                GameEvents.current.onDamage1Interact += setState;
            break;
            case(1):
                GameEvents.current.onDamage2Interact += setState;
            break;
            case(2):
                GameEvents.current.onDamage3Interact += setState;
            break;
            case(3):
                GameEvents.current.onDamage4Interact += setState;
            break;
            case(4):
                GameEvents.current.onDamage5Interact += setState;
            break;
            
        }
    }

    void setState(){
        gameObject.SetActive(false);
        Debug.Log("Gone");
    }
}
