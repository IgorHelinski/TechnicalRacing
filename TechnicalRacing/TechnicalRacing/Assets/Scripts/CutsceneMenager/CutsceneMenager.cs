using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;   

public class CutsceneMenager : MonoBehaviour
{
    public float waitTime = 3f;
    public DialogueMenager dMenager;
    public GameObject ImagesHolder;

    private int sceneIndex;
    private int characterIndex;

    public GameObject speaker;
    public TextMeshProUGUI characterName;
    public Image characterImage;

    private bool introEnd = false;

    public GameEvent onStartDialogue;
    public GameEvent onIntroEnd;
    public DialogueComponent[] dialogueComponents;
    public DialogueComponent[] characterDialogueComponents;

    [SerializeField]
    public Scenes[] scenes;

    [System.Serializable]
    public class Scenes
    {
        [SerializeField]
        public GameObject imagePrefab;
        public int sceneIndex;
    }

    [SerializeField]
    public Characters[] characters;

    [System.Serializable]
    public class Characters
    {
        [SerializeField]
        public Sprite characterSprite;
        public string characterName;
        public int sceneIndex;
    }

    public void OnIntroEnd(Component sender, object data)
    {
        //introEnd = true;
        //sceneIndex = 0;
        //onStartDialogue.Raise(this, characterDialogueComponents);

        //speaker.SetActive(true);

    }

    private void Update()
    {
        
        
        if(sceneIndex <= scenes.Length - 1 && scenes[sceneIndex].sceneIndex == dMenager.index)
        {
            if(ImagesHolder.transform.childCount > 0)
            {
                Destroy(ImagesHolder.transform.GetChild(0).gameObject);
            }
            Instantiate(scenes[sceneIndex].imagePrefab, ImagesHolder.transform);
            sceneIndex++;
        }

       
        if (characters[characterIndex].sceneIndex == dMenager.index)
        {
            speaker.SetActive(true);
            characterName.text = characters[characterIndex].characterName;
            characterImage.sprite = characters[characterIndex].characterSprite;
            characterIndex++;
        }
    }

    private void Start()
    {
        characterIndex = 0;
        sceneIndex = 0;
        StartCoroutine(starting());
    }

    IEnumerator starting()
    {
        yield return new WaitForSeconds(waitTime);
        onStartDialogue.Raise(this, dialogueComponents);
        Debug.Log("started");
    }
}
