using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///////////////////////////////////////////Creates a random tile from a list/////////////////////////////////////////////////////
//alternatively, could have parts of tiles such as a hill and be called by another script that loops over all pos for features
public class CreateObject : MonoBehaviour
{
    public GameObject[] objects;

    private void Start()
    {
        int rand = Random.Range(0, objects.Length);
        Instantiate(objects[rand], transform.position, Quaternion.identity);
    }

}
