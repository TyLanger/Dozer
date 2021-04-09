using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject triangleSprite;
    public int numSprites = 12;
    public float radius = 1;

    public float rotationSpeed = 10;
    public Vector3 axis;
    public Vector3 initialRotation;
    public Color tint;
    public float scaling = 1;

    bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreatePortal()
    {
        float angle = 360 / numSprites;
        for (int i = 0; i < numSprites; i++)
        {
            GameObject copy = Instantiate(triangleSprite, transform);

            copy.GetComponentInChildren<SpriteRenderer>().color = tint;

            Vector3 position = transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle * i) * radius, Mathf.Sin(Mathf.Deg2Rad * angle * i) * radius, 0);
            copy.transform.position = position;
            copy.transform.Rotate(transform.forward, angle * i);
            copy.transform.localScale = Vector3.one * scaling;
        }
        transform.Rotate(initialRotation);

        ready = true;
    }

    public void CreatePortalOVerTime()
    {
        StartCoroutine(SpawnPortalOverTime());
    }

    IEnumerator SpawnPortalOverTime()
    {
        float angle = 360 / numSprites;
        for (int i = 0; i < numSprites; i++)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject copy = Instantiate(triangleSprite, transform);

            copy.GetComponentInChildren<SpriteRenderer>().color = tint;

            Vector3 position = transform.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle * i) * radius, Mathf.Sin(Mathf.Deg2Rad * angle * i) * radius, 0);
            copy.transform.position = position;
            copy.transform.Rotate(transform.forward, angle * i);
            copy.transform.localScale = Vector3.one * scaling;
        }
        transform.Rotate(initialRotation);

        ready = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            transform.Rotate(axis, rotationSpeed * Time.deltaTime);
        }
    }
}
