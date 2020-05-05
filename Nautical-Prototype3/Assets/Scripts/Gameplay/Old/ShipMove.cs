using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Rigidbody2D))]
public class ShipMove : MonoBehaviour
{
    float fullSailV = 8;
    float anglularV = 20;
    public float shipAngle;
    public Vector2 anv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //apply rotation
        shipAngle = transform.localRotation.eulerAngles.z;
        float hi = -1 *(Input.GetAxisRaw("Horizontal"));
        GetComponent<Rigidbody2D>().angularVelocity = anglularV * hi;
        //apply forward velocity
        anv = rotateVelocity(shipAngle, getFV());
        GetComponent<Rigidbody2D>().velocity= anv;
    }
    float getFV(){
        if ((Input.GetAxisRaw("Vertical")) <=0){
         return 0;
        }
         return(fullSailV);
    }

    Vector2 rotateVelocity(float angle, float radius){
        float vx = radius * Mathf.Sin(ConvertToRadians(angle)) *-1;
        float vy = radius * Mathf.Cos(ConvertToRadians(angle));
        return new Vector2(vx,vy);
    }
    public float ConvertToRadians(float angle)
    {
        return (Mathf.PI / 180) * angle;
    }
}
