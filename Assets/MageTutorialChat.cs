using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageTutorialChat : MonoBehaviour
{
    // Start is called before the first frame update
    public Text myText;
    bool displaying = false;
    public GameObject arrow;
    public GameObject movementTutorial;
    public GameObject attackTutorial;
    public GameObject attackTutorial2;
    public GameObject tauntTutorial;
    public GameObject dashTutorial;
    void Start()
    {
        ToastManager.instance.magetoasts.Enqueue("Hey there, welcome to Escort Hero!");
        ToastManager.instance.magetoasts.Enqueue("I am Princess Elsa, whom you will be escorting.");
        ToastManager.instance.magetoasts.Enqueue("Before we embark on this journey together,\nlet's see what you can do!");
    }

    // Update is called once per frame
    void Update()
    {
        if (ToastManager.instance.magetoasts.Count != 0 && !displaying)
        {
            StartCoroutine(DisplayText());
        }
        if (ToastManager.instance.magetoasts.Count == 0 && !displaying)
        {
            if (ToastManager.instance.MageCount == 0)
            {

                ToastManager.instance.magetoasts.Enqueue("Use LEFT JOYSTICK to move to the flashing area!");
                StartCoroutine(DisplayTextIncrement());
            }
            if (ToastManager.instance.MageCount == 1)
            {
                movementTutorial.SetActive(true);
                movementTutorial.GetComponent<MovementTutorial>().playEffect();
            }
            if (ToastManager.instance.MageCount == 2)
            {
                arrow.SetActive(true);
                ToastManager.instance.magetoasts.Enqueue("Use RIGHT JOYSTICK TO aim.\nPress RIGHT TRIGGER to attack the Red Boximon!");
                ToastManager.instance.MageCount += 1;
                attackTutorial.SetActive(true);
            }
            if (ToastManager.instance.MageCount == 4)
            {
                ToastManager.instance.magetoasts.Enqueue("Well done! Now let's test your FIREBALL aiming skill!!!");
                StartCoroutine(DisplayTextIncrement());
            }
            if (ToastManager.instance.MageCount == 5)
            {
                attackTutorial2.SetActive(true);
            }
            if (ToastManager.instance.MageCount == 6)
            {
                ToastManager.instance.magetoasts.Enqueue("Your 'A' button summons a snowstorm, and freezes\nenemies within the circle. Why don't you try it out?");
                StartCoroutine(DisplayTextIncrement());
            }
            if (ToastManager.instance.MageCount == 7)
            {
                tauntTutorial.SetActive(true);
            }
            if (ToastManager.instance.MageCount == 8 && !displaying)
            {
                ToastManager.instance.MageFinished = true;
            }

        }
    }
    IEnumerator DisplayText()
    {
        displaying = true;
        myText.text = "";
        string displayMessage = ToastManager.instance.magetoasts.Dequeue();
        foreach (char letter in displayMessage)
        {
            myText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(1f);
        displaying = false;
    }

    IEnumerator DisplayTextIncrement()
    {
        yield return StartCoroutine(DisplayText());
        ToastManager.instance.MageCount += 1;
    }


}
