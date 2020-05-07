using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorAnim : MonoBehaviour
{
    public Sprite[] sprites;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onAnchorInteract += setAnchor;
    }

    // Update is called once per frame
    void setAnchor()
    {
        if(i == 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            i = 1;
        }else{
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        i = 0;
        }
        
    }
}
