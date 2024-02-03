using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueMenager : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject choiceBox;
    public GameObject choicePrefab;

    public TextMeshProUGUI dialogueTextMesh;
    public AudioSource textPopUpSound;

    // states
    public bool isSpeaking;
    bool finishedLine;
    bool answeared = false;

    int choiceId;
    public int index;

    public GameEvent OnDialogueEnd;

    private void Start()
    {
        //dialogueBox.SetActive(false);
    }

    public void OnStartDialogue(Component sender, object data)
    {
        if(data is DialogueComponent[])
        {
            //DialogueAgent agent = (DialogueAgent)sender;
            DialogueComponent[] components = (DialogueComponent[])data;

            if(isSpeaking == false)
                StartCoroutine(DialogueCycle(components));
        }
    }

    IEnumerator DialogueCycle(DialogueComponent[] components)
    {
        dialogueTextMesh.text = "";
        dialogueBox.SetActive(true);
        isSpeaking = true;
        finishedLine = true;
        index = 0;

        while (isSpeaking == true)
        {
            yield return null;

            if (finishedLine)
            {
                DialogueComponent component = components[index];
                StartCoroutine(TypeLine(component));
                index++;
                if (index >= components.Length && answeared)
                {
                    isSpeaking = false;
                    yield return new WaitForSeconds(5);
                    OnDialogueEnd.Raise(this, components);
                }
            }
        }
    }

    IEnumerator TypeLine(DialogueComponent component)
    {
        finishedLine = false;
        dialogueTextMesh.text = "";

        foreach (char c in component.text.ToCharArray())
        {
            dialogueTextMesh.text += c;
            yield return new WaitForSeconds(component.displaySpeed);
            if(component.displaySound != null)
                textPopUpSound.PlayOneShot(component.displaySound);
        }

        answeared = true;

        // when we have a choice
        if(component.type == D_ComponentType.choise)
        {
            Button[] buttons = new Button[component.choices.Length];

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            for (int i = 0; i < component.choices.Length; i++)
            {
                GameObject choice = Instantiate(choicePrefab, Vector3.zero, Quaternion.identity, choiceBox.transform);
                choice.GetComponentInChildren<TextMeshProUGUI>().text = component.choices[i];
                buttons[i] = choice.GetComponent<Button>();
                int index = i;
                buttons[i].onClick.AddListener(delegate { OnClick(index); });
            }

            answeared = false;

            while (!answeared)
            {
                yield return null;
            }

            foreach (Button button in buttons)
            {
                button.onClick.RemoveAllListeners();
            }

            for (int i = 0; i < choiceBox.transform.childCount; i++)
                Destroy(choiceBox.transform.GetChild(i).gameObject);


            DialogueComponent[] components = component.answears[choiceId].components;

            finishedLine = true;
            isSpeaking = false;

            StartCoroutine(DialogueCycle(components));

            yield break;
        }

        yield return new WaitForSeconds(component.clearWaitTime);
        finishedLine = true;

        if(isSpeaking == false)
            dialogueBox.SetActive(false);
    }

    void OnClick(int index)
    {
        Debug.Log("Button with index: " + index + " was just pressed!");
        choiceId = index;
        answeared = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
