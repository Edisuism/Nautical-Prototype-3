using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDamage : MonoBehaviour
{
    // Start is called before the first frame update
    bool state = true;
    void Start()
    {
        GameEvents.current.onDamageInteract += setState;
    }

    void setState(){
        gameObject.SetActive(false);
        Debug.Log("Gone");
    }
}
