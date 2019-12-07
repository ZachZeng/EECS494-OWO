using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TogetherTutorialChat : MonoBehaviour
{
    // Start is called before the first frame update
    public Text myText;
    bool displaying = false;
    public GameObject knight;
    public GameObject mage;
    public bool hurt = false;
    public bool checkfull = false;
    public bool hurtmage = false;
    public bool finished = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ToastManager.instance.togethertoasts.Count != 0 && !displaying)
        {
            StartCoroutine(DisplayText());
        }
        if (ToastManager.instance.togetherCount == 1 && !hurt)
        {
            hurt = true;
            knight.GetComponent<Health>().ModifyHealth(-50);
            ToastManager.instance.togethertoasts.Enqueue("Oh no! Knight got hurt!");
            ToastManager.instance.togetherCount += 1;
            ToastManager.instance.togethertoasts.Enqueue("Mage: Aim (RIGHT JOYSTICK) at knight,\nLEFT TRIGGER to shoot a healing ball!\n");
        }
        if (ToastManager.instance.togetherCount == 6 && knight.GetComponent<Health>().currentHealth == 100 && !checkfull)
        {
            checkfull = true;
            ToastManager.instance.togethertoasts.Enqueue("Good job!\nMage, try healing yourself.");

        }
        if (ToastManager.instance.togetherCount == 7 && !hurtmage)
        {
            mage.GetComponent<Health>().ModifyHealth(-50);
            hurtmage = true;
            ToastManager.instance.togetherCount += 1;


        }
        if(ToastManager.instance.togetherCount == 8 && mage.GetComponent<Health>().currentHealth == 100 && !finished)
        {
            finished = true;
            ToastManager.instance.togethertoasts.Enqueue("Congratulations! You have finished the tutorial!\nThe game will begin shortly");
        }

    }

    IEnumerator DisplayText()
    {
        displaying = true;
        myText.text = "";
        string displayMessage = ToastManager.instance.togethertoasts.Dequeue();
        foreach (char letter in displayMessage)
        {
            myText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
        yield return new WaitForSeconds(1f);
        displaying = false;
        ToastManager.instance.togetherCount += 1;
    }

}
