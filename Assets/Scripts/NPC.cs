using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour
{

    public Transform ChatBackGround;
    public Transform NPCCharacter;
    public Transform text;

    private DialogSystem dialogueSystem;



    void Start()
    {
        dialogueSystem = FindObjectOfType<DialogSystem>();
    }

    void Update()
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(NPCCharacter.position);
        Pos.y += 105;
        ChatBackGround.position = Pos;
        text.position = Pos;

    }

    public void presentDialog(string[] sentences)
    {
        dialogueSystem.dialogueLines = sentences;
        FindObjectOfType<DialogSystem>().ShowDialog();
    }
}


