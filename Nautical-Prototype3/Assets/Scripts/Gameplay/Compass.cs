using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = -1 * GameObject.FindGameObjectWithTag("ShipPivot").GetComponent<ShipMovAcc>().getEulerAngles();
    }
}
