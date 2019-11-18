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
    void Start()
    {
        ToastManager.instance.toasts.Enqueue("Hey there, welcome to Escort Hero!");
        ToastManager.instance.toasts.Enqueue("I am Princess Elsa, whom you will be escorting.");
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
                ToastManager.instance.count += 1;
                movementTutorial.SetActive(true);
                movementTutorial.GetComponent<MovementTutorial>().playEffect();
                ToastManager.instance.toasts.Enqueue("Use LEFT JOYSTICK to move to the flashing area!");
            }
            if (ToastManager.instance.count == 2)
            {

                ToastManager.instance.toasts.Enqueue("Press 'A' to attack the Red Boximon!");
                ToastManager.instance.count += 1;
                attackTutorial.SetActive(true);
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
}
