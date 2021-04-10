using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public float lifetime = 3;
    float ogLifetime;
    public float speed = 1;
    public float fadeSpeed = 0.5f;

    TextMeshPro tmp;

    // Start is called before the first frame update
    void Awake()
    {
        transform.rotation = Camera.main.transform.rotation;
        tmp = GetComponentInChildren<TextMeshPro>();
        ogLifetime = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, Time.deltaTime * speed);
            lifetime -= Time.deltaTime;
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, tmp.color.a - (Time.deltaTime * fadeSpeed));
        }
    }

    public void Reset()
    {
        lifetime = ogLifetime;
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1);
    }
}
