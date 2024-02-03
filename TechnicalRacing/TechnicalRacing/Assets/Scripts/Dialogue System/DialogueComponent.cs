using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum D_ComponentType
{
    text,
    choise
}

[CreateAssetMenu]
public class DialogueComponent : ScriptableObject
{
    public D_ComponentType type;
    public string text;
    public float displaySpeed;
    public float clearWaitTime;
    public AudioClip displaySound;

    public string[] choices;

    [SerializeField]
    public Answears[] answears;

    [System.Serializable]
    public class Answears
    {
        [SerializeField]
        public DialogueComponent[] components;
    }

}

