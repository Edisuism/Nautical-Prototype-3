using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///////////////////////////////////////////////////////Check type/////////////////////////////////////////////////////// 
public class AreaType : MonoBehaviour
{
    public int type;

    public void areaDestroy()
    {
        Destroy(gameObject);
    }
}
