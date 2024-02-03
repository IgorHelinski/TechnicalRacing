using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueAgent : MonoBehaviour
{
    public GameEvent onStartDialogue;

    public DialogueComponent[] dialogueComponents;

    public Transform cameraPos;

    bool debug = true;

    public void OnTriggerEnter(Collider other)
    {
        if(debug)
            onStartDialogue.Raise(this, dialogueComponents);

        debug = false;
    }
}
