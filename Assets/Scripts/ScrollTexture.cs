using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{

    Renderer myRenderer;
    public float xScroll;
    public float yScroll;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        myRenderer.material.mainTextureOffset = new Vector2(Time.time * xScroll, Time.time * yScroll);
    }
}
