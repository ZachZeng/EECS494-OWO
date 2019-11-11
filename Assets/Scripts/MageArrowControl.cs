using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MageArrowControl : MonoBehaviour
{

    MagePlayerMovement magePlayerMovement;
    public GameObject arrow;

    private Vector2 prev_direction;

    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        magePlayerMovement = GetComponent<MagePlayerMovement>();
        prev_direction = new Vector2(-1f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        direction = GetRightInput();
        if(direction != Vector2.zero)
        {
            arrow.transform.position = transform.position + new Vector3(direction.x,0.6f,direction.y);
            prev_direction = direction;
            float rotateDegree = Vector2.Angle(new Vector2(-1f, 0f), direction);
            if (direction.y < 0)
            {
                rotateDegree = 0 - rotateDegree;
            }
            arrow.transform.rotation = Quaternion.Euler(0, rotateDegree, 90);
       
        }
        else
        {
            arrow.transform.position = transform.position + new Vector3(prev_direction.x, 0.6f, prev_direction.y);
            direction = prev_direction;
        }
    }


    Vector2 GetRightInput()
    {
        Gamepad active = Gamepad.all[magePlayerMovement.playerNum];
        float horizontal_input = active.rightStick.x.ReadValue();
        float vertical_input = active.rightStick.y.ReadValue();
        if (Mathf.Abs(horizontal_input) < 0.1)
            horizontal_input = 0;
        if (Mathf.Abs(vertical_input) < 0.1)
            vertical_input = 0;

        Vector2 dir = new Vector2(horizontal_input, vertical_input);
        dir.Normalize();

        return dir;
    }

}
