using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/////////////////////////////////////////////Script fills in empty space///////////////////////////////////////////////
public class SpawnArea : MonoBehaviour
{
    public LayerMask area;
    public LevelGen levelGen;
    
    void Update()
    {
        Collider2D areaDetection = Physics2D.OverlapCircle(transform.position, 1, area);
        if (areaDetection == null && levelGen.stopGen == true)
        {
            int rand = Random.Range(0, levelGen.areas.Length);
            Instantiate(levelGen.areas[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
