using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelDisplay : MonoBehaviour
{

    

    public Bulldozer Dozer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fuel = Dozer.GetFuelPercent();
        // -45 is full
        // 45 is empty
        // 45 - 90 = -45
        // 45 - 0 = 45
        float zRot = 45 - fuel * 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRot));
    }
}
