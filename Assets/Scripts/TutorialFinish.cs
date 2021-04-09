using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFinish : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent endEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Bulldozer>())
        {
            // it's the player
            other.GetComponent<Bulldozer>().SetNewSpawn();
            endEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
