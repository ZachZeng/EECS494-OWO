using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntFirstEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public IEnumerator MoveOn()
    {
        yield return new WaitForSeconds(2f);
        ToastManager.instance.count += 1;
        ToastManager.instance.toasts.Enqueue("Good job! Now let's move on to the dash skill.");
    }*/

    private void OnDestroy()
    {
        ToastManager.instance.count += 1;
        ToastManager.instance.toasts.Enqueue("Good job! Now let's move on to the dash skill.");

    }
}
