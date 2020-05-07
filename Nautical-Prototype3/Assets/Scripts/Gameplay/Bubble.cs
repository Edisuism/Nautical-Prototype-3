using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Sprite[] bubbles;
    public GameObject parent;
    int c;
    // Update is called once per frame
    void Update()
    {
        //This is very unoptimised
        //idealy call only when carrying is changed
        //I am lazy
        int c = parent.GetComponent<MimicPlayerLocation>().getPlayerToMimic().GetComponent<PlayerController>().getCarrying();
        gameObject.GetComponent<SpriteRenderer>().sprite = bubbles[c];
    }
}
