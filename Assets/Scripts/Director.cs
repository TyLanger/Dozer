using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Director : MonoBehaviour
{
    [SerializeField]
    int beansPushedOff = 0;

    public int[] eventBeanThresholds;
    public UnityEvent[] events;
    int nextEventIndex = 0;

    public TextMeshProUGUI dialogueBox;

    // Start is called before the first frame update
    void Start()
    {
        BeanCounter.OnBeanCounted += OnBeanCounted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBeanCounted(int points)
    {
        beansPushedOff += points;

        if ((nextEventIndex < eventBeanThresholds.Length) && (beansPushedOff >= eventBeanThresholds[nextEventIndex]))
        {
            events[nextEventIndex]?.Invoke();
            nextEventIndex++;
        }
    }

    void UpdateText(string text)
    {
        dialogueBox.text = text;
    }

    IEnumerator ClearAndUpdateText(string text)
    {
        UpdateText("");
        yield return new WaitForSeconds(1);
        UpdateText(text);
    }

    void BeginGame()
    {

    }

    public void VoidDerrickInvented()
    {
        // called by the unity event when you meet the requirements
        // print some text
        // good news! Those beans you pushed off did something.
        // we can now drill into the void and create fuel to make your dozer move faster
        // keep it fueled up to keep up the speed
        //FindObjectOfType<Bulldozer>().ToggleSpeed();
        string message = "Now we can drill into the void to create fuel to make your dozer move faster. Keep it fueled up to keep up the speed";
        StartCoroutine(ClearAndUpdateText(message));
    }

    public void EndGame()
    {
        // all the extra launchers spawned
        // A better fuel source was just invented. So that means this place is old news. We won't need all of these beans we've been storing.
        // Here, you can have them. That old dozer is also outclassed so don't worry about giving it back.
        // Oh, I guess this means you've been formally fired. Good bye then.
        

    }
}
