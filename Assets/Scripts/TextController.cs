using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public float lifetime = 3;
    public float speed = 1;
    public float fadeSpeed = 0.5f;

    TextMeshPro tmp;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Camera.main.transform.rotation;
        tmp = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, Time.deltaTime * speed);
        lifetime -= Time.deltaTime;
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, tmp.color.a-(Time.deltaTime * fadeSpeed));
        if(lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
