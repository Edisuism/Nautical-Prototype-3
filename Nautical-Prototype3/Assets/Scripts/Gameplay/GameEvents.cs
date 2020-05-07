using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake() {
        current = this;
    }

    public event Action onMastInteract;
    public void MastInteract(){
        if (onMastInteract != null){
            onMastInteract();
        }
    }
    public event Action onMast2Interact;
    public void Mast2Interact(){
        if (onMastInteract != null){
            onMast2Interact();
        }
    }
    public event Action onAnchorInteract;
    public void AnchorInteract(){
        if (onAnchorInteract != null){
            onAnchorInteract();
        }
    }
    public event Action onWheelInteract;
    public void WheelInteract(){
        if (onWheelInteract != null){
            onWheelInteract();
        }
    }
    public event Action onDamage1Interact;
    public void Damage1Interact(){
        if (onDamage1Interact != null){
            onDamage1Interact();
        }
    }
    public event Action onDamage2Interact;
    public void Damage2Interact(){
        if (onDamage2Interact != null){
            onDamage2Interact();
        }
    }
    public event Action onDamage3Interact;
    public void Damage3Interact(){
        if (onDamage3Interact != null){
            onDamage3Interact();
        }
    }
    public event Action onDamage4Interact;
    public void Damage4Interact(){
        if (onDamage4Interact != null){
            onDamage4Interact();
        }
    }
        public event Action onDamage5Interact;
    public void Damage5Interact(){
        if (onDamage5Interact != null){
            onDamage5Interact();
        }
    }
}
