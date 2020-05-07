using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamage : MonoBehaviour
{
    // Start is called before the first frame update
    bool state = true;
    
    void Awake() {
        
        /*switch(gameobject.name){
            case("Damage1"):
                GameEvents.current.onDamage1Interact += setState;
            break;
            case("Damage2"):
                GameEvents.current.onDamage2Interact += setState;
            break;
            
        }*/
    }

    void setState(){
        gameObject.SetActive(false);
        Debug.Log("Gone");
    }
}
