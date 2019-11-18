using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialChats : MonoBehaviour
{
    // Start is called before the first frame update
    public Text myText;
    bool displaying = false;
    public GameObject movementTutorial;
    public GameObject attackTutorial;
    public GameObject attackTutorial2;
    public GameObject tauntTutorial;
    public GameObject dashTutorial;
    void Start()
    {
        //ToastManager.instance.toasts.Enqueue("Hey there, welcome to Escort Hero!");
        //ToastManager.instance.toasts.Enqueue("I am Princess Elsa, whom you will be escorting.");
        ToastManager.instance.toasts.Enqueue("Before we embark on this journey together,\nlet's see what you can do!");
    }

    // Update is called once per frame
    void Update()
    {
        if(ToastManager.instance.toasts.Count != 0 && !displaying)
        {
            StartCoroutine(DisplayText());
        }
        if (ToastManager.instance.toasts.Count == 0 && !displaying)
        {
            if (ToastManager.instance.count == 0)
            {
              
                ToastManager.instance.toasts.Enqueue("Use LEFT JOYSTICK to move to the flashing area!");
                StartCoroutine(DisplayTextIncrement());
            }
            if (ToastManager.instance.count == 1)
            {
                movementTutorial.SetActive(true);
                movementTutorial.GetComponent<MovementTutorial>().playEffect();
            }
            if (ToastManager.instance.count == 2)
            {

                ToastManager.instance.toasts.Enqueue("Press 'A' to attack the Red Boximon!");
                ToastManager.instance.count += 1;
                attackTutorial.SetActive(true);
            }
            if (ToastManager.instance.count == 4)
            {
                ToastManager.instance.toasts.Enqueue("Well done! If the first two attacks both hit an enemy, the third\nwill be enhanced to a ground smash! Try it out!!!");
                StartCoroutine(DisplayTextIncrement());
            }
            if (ToastManager.instance.count == 5)
            {
                attackTutorial2.SetActive(true);
            }
            if (ToastManager.instance.count == 6)
            {
                ToastManager.instance.toasts.Enqueue("Your 'X' button unleashes a globe of fury, and attracts\nenemies nearby to attack you. Attract then kill that Boximon!");
                StartCoroutine(DisplayTextIncrement());
            }
            if (ToastManager.instance.count == 7)
            {
                tauntTutorial.SetActive(true);
            }
            if (ToastManager.instance.count == 8)
            {
                ToastManager.instance.count += 1;
                ToastManager.instance.toasts.Enqueue("Press 'Y' to dash in the direction you're currently facing.\nThe force will push and stun the enemies along the way!");
                dashTutorial.SetActive(true);
            }

        }
    }
    IEnumerator DisplayText()
    {
        displaying = true;
        myText.text = "";
        string displayMessage = ToastManager.instance.toasts.Dequeue();
        foreach (char letter in displayMessage)
        {
            myText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1f);
        displaying = false;
    }

    IEnumerator DisplayTextIncrement()
    {
        yield return StartCoroutine(DisplayText());
        ToastManager.instance.count += 1;
    }
}
