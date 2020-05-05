using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Rigidbody2D))]
 
public class FakeChild : MonoBehaviour
{
    public Transform FakeParent;
    public Vector3 _positionOffset;
    private Quaternion _rotationOffset;
    public bool isPlayer = false;

    float playerSpeed = -0.02f;

    private void Start()
    {
        if(FakeParent != null)
        {
            SetFakeParent(FakeParent);
        }
    }
 
    private void Update()
    {
        if (FakeParent == null)
            return;
        if(isPlayer == true){
            Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            GetComponent<Rigidbody2D>().velocity=(inputVector * playerSpeed);
            _positionOffset += (inputVector * playerSpeed);
        }
        var targetPos = FakeParent.position - _positionOffset;
        var targetRot = FakeParent.localRotation * _rotationOffset;
        
        transform.position = (RotatePointAroundPivot(targetPos, FakeParent.position, targetRot));
        transform.localRotation = targetRot;

       
        //account for ship velocity
        //transform.position += (inputVector * playerSpeed);
        
    }

    public void SetFakeParent(Transform parent)
    {
        //Offset vector
        if (isPlayer){
            Vector3 rbp = (GetComponent<Rigidbody2D>().position);
            _positionOffset = parent.position - rbp;
        }else{
            _positionOffset = parent.position - transform.position;
        }
        //Offset rotation
        
        _rotationOffset = Quaternion.Inverse(parent.localRotation * transform.localRotation);
        //Our fake parent
        FakeParent = parent;
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
}