using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateWater : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject toInstantiate;
    void Start()
    {
        for(int i = 0; i < 200; i = i + 1){
            for(int k = 0; k < 200; k = k + 1){
                Instantiate(toInstantiate, new Vector3(i,k,0f), new Quaternion());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
