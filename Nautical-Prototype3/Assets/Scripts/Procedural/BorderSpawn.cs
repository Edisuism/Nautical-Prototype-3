using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderSpawn : MonoBehaviour
{
    public GameObject[] type;
    void Start()
    {
        int rand = Random.Range(0, type.Length);
        Instantiate(type[rand], transform.position, Quaternion.identity);
    }

}
