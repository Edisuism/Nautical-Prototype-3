using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicPlayerLocation: MonoBehaviour
{
    public Transform FakeParent;
    private GameObject playerToMimic;

    private Vector3 _positionOffset;
    private Quaternion _rotationOffset;
    Animator anim;
    float lastdirx,lastdiry;

    private void Awake()
    {
        FakeParent = GameObject.FindGameObjectWithTag("ShipPivot").transform;
        GameObject[] playerCount = GameObject.FindGameObjectsWithTag("Player");
        int h = 0;
        foreach (GameObject p in playerCount){
            if(p.GetComponent<PlayerController>().getID() > h){
                playerToMimic = p;
            }
        }
        if(FakeParent != null)
        {
            
            SetFakeParent(FakeParent);
        }
        transform.position = FakeParent.transform.position + getToMimicOffset();
        anim = this.GetComponent<Animator>();
        anim.runtimeAnimatorController = playerToMimic.GetComponent<Animator>().runtimeAnimatorController;
    }

    private void Update()
    {
        if (FakeParent == null)
            return;
        _positionOffset = getToMimicOffset();
        var targetPos = FakeParent.position + _positionOffset;
        var targetRot = FakeParent.localRotation * _rotationOffset;

        transform.position = RotatePointAroundPivot(targetPos, FakeParent.position, targetRot);
        transform.localRotation = targetRot;
        animatorHandler();
    }
    private void animatorHandler(){
        //Sets animation: Kina jank atm
        PlayerController PC = playerToMimic.GetComponent<PlayerController>();
        anim.SetBool("Wheel", PC.getWheelLocked());

            if(PC.getInputs().x != 0){
                lastdirx = PC.getInputs().x;
            }
            anim.SetFloat("Horizontal", PC.getInputs().x);
            if(PC.getInputs().y != 0){
                lastdirx = PC.getInputs().y;
                if(PC.getInputs().x == 0){
                    lastdirx = 0;
                }
            }
            anim.SetFloat("Vertical", PC.getInputs().y);
            if(PC.getInputs().x == 0 && PC.getInputs().y == 0){
                anim.SetFloat("Horizontal", lastdirx);
                anim.SetFloat("Vertical", lastdiry);
            }
        float s = Mathf.Abs(PC.getInputs().x) + Mathf.Abs(PC.getInputs().y);
        anim.SetFloat("Speed", s);
    }

    public void SetFakeParent(Transform parent)
    {
        //Offset vector
        //_positionOffset = parent.position - transform.position;
        //Offset rotation
        SetInitialRotation();
        _rotationOffset = Quaternion.Inverse(parent.localRotation * transform.localRotation);
        //Our fake parent
        FakeParent = parent;
    }
    public void SetInitialRotation(){
       // _positionOffset = getToMimicOffset();
        //var targetPos = FakeParent.position + _positionOffset;
        //var targetRot = FakeParent.rotation;
        //transform.position = RotatePointAroundPivot(targetPos, FakeParent.position, targetRot);
    }
    public void SetPlayerToMimic(GameObject player){
        playerToMimic = player;
    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        //Get a direction from the pivot to the point
        Vector3 dir = point - pivot;
        //Rotate vector around pivot
        dir = rotation * dir; 
        //Calc the rotated vector
        point = dir + pivot; 
        //Return calculated vector
        return point; 
    }
    Vector3 getToMimicOffset(){
        return (playerToMimic.transform.position);
    }
}