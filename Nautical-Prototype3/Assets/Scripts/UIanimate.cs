using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIanimate : MonoBehaviour
{
    public int framerate = 30;
    Sprite[] sprites;
    Image image;
    float time = 0f;
    private float elapsedTime = 0f;
    private int currentFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        enabled = false;
        LoadSpriteSheet();
    }

    private void LoadSpriteSheet()
    {
        sprites = Resources.LoadAll<Sprite>("player1_down");
        if (sprites != null && sprites.Length > 0)
        {
            time = 1f / framerate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= time)
        {
            ++currentFrame;
        }
    }
}
