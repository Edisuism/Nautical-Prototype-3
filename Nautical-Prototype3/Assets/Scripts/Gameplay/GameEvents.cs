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
    public event Action onDamageInteract;
    public void DamageInteract(){
        if (onDamageInteract != null){
            onDamageInteract();
        }
    }
}
