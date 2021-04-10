using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    [SerializeField]
    int beansPushedOff = 0;

    public int[] eventBeanThresholds;
    public UnityEvent[] events;
    int nextEventIndex = 0;

    public TextMeshProUGUI dialogueBox;
    public TextMeshProUGUI introText;
    public Image blockerImage;
    public Image logo;
    public TextMeshProUGUI beanCounter;

    public CameraController cameraScript;

    public static event Action OnGameVisible;

    // Start is called before the first frame update
    void Start()
    {
        BeanCounter.OnBeanCounted += OnBeanCounted;
        //BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBeanCounted(int points)
    {
        beansPushedOff += points;

        if (nextEventIndex < eventBeanThresholds.Length)
        {
            beanCounter.text = $"{beansPushedOff}/{eventBeanThresholds[nextEventIndex]} Beans Pushed";
            if ((beansPushedOff >= eventBeanThresholds[nextEventIndex]))
            {
                events[nextEventIndex]?.Invoke();
                nextEventIndex++;
            }
        }
    }

    void UpdateText(string text)
    {
        dialogueBox.text = text;
    }

    IEnumerator ClearAndUpdateText(string text, float delay = 0)
    {
        
        yield return new WaitForSeconds(delay);
        // works, but makes the text too long
        // and you reread stuff
        // hurts more than it helps
        //string oldText = dialogueBox.text;
        //text = oldText + System.Environment.NewLine + text;
        UpdateText("");
        yield return new WaitForSeconds(0.8f);
        UpdateText(text);
    }

    public void PlayIntro()
    {
        StartCoroutine(IntroOverTime());
    }

    IEnumerator IntroOverTime()
    {
        string openingMessage = "At the beginning, there was nothing";
        string message = "Then beans were invented";
        // button
        // first text
        introText.text = openingMessage;
        // wait few sec
        yield return new WaitForSeconds(2);
        // fade
        for (int i = 0; i < 10; i++)
        {
            introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, introText.color.a-0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        // second text
        introText.text = message;
        introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, 1);
        yield return new WaitForSeconds(2);
        // fade
        for (int i = 0; i < 10; i++)
        {
            introText.color = new Color(introText.color.r, introText.color.g, introText.color.b, introText.color.a - 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        // remove black screen
        cameraScript.StartTracking();

        for (int i = 0; i < 20; i++)
        {
            blockerImage.color = new Color(blockerImage.color.r, blockerImage.color.g, blockerImage.color.b, blockerImage.color.a - 0.1f);
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, logo.color.a - 0.05f);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(blockerImage);
        yield return null;

        OnGameVisible?.Invoke();
        BeginGame();
        
    }

    public void BeginGame()
    {
        // At the beginning, there was nothing. Then beans were invented. We found out if you push the beans into the void, it creates void fuel which can power that old dozer you've got.
        
        string message2 = "We found out if we pushed the beans into the void, it could create some power.";
        string message3 = "So that's where you come in. I see you've found that old dozer. Well, take that dozer out there and push those beans off the table into the void.";

        // message and message1 should be in a special text box in the center of the screen that plays before the game starts

        UpdateText(""); 
        StartCoroutine(ClearAndUpdateText(message2));
        StartCoroutine(ClearAndUpdateText(message3, 6));
    }

    public void VoidDerrickInvented()
    {
        // called by the unity event when you meet the requirements
        // print some text
        // good news! Those beans you pushed off did something.
        // we can now drill into the void and create fuel to make your dozer move faster
        // keep it fueled up to keep up the speed
        //FindObjectOfType<Bulldozer>().ToggleSpeed();
        string message = "Wow! All those beans you pushed off actually did something. Well how 'bout that?";
        string message2 = "Now we can drill into the void to create fuel to make your dozer move faster. Keep it fueled up to keep up the speed.";
        UpdateText("");
        StartCoroutine(ClearAndUpdateText(message));
        StartCoroutine(ClearAndUpdateText(message2, 6));

    }

    public void EndGame()
    {
        // all the extra launchers spawned
        // A better fuel source was just invented. So that means this place is old news. We won't need all of these beans we've been storing.
        // Here, you can have them. That old dozer is also outclassed so don't worry about giving it back.
        // Oh, I guess this means you've been formally fired. Good bye then.
        string message = "Good news! A new fuel source was just invented. So that means this place is yesterday's can of beans; we won't need all of these beans we've been storing.";
        string message2 = "Here, you can have them. That old dozer is also outclassed so don't worry about giving it back.";
        string message3 = "Oh, I guess this means you've been formally fired. Uh, good bye then.";

        UpdateText("");
        StartCoroutine(ClearAndUpdateText(message));
        StartCoroutine(ClearAndUpdateText(message2, 10));
        StartCoroutine(ClearAndUpdateText(message3, 10+7));

    }

    public void FallIntoVoid()
    {
        string message = "Enter the void.";

        UpdateText("");
        StartCoroutine(ClearAndUpdateText(message));
    }
}
