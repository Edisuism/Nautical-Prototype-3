using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Rigidbody2D))]

public class FakeChildPlayer : MonoBehaviour
{
    public Transform FakeParent;
    public Vector3 _positionOffset;
    private Quaternion _rotationOffset;
    float playerSpeed = -0.02f;
    // Start is called before the first frame update
    void Start()
    {
        if(FakeParent != null)
        {
            SetFakeParent(FakeParent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetFakeParent(Transform parent)
    {
        //Offset vector
        _positionOffset = parent.position - transform.position;
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
