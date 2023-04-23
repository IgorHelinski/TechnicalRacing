using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class ObjectActivate : NetworkBehaviour
{
    private void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "RaceTrack1")
        {
            if(gameObject.transform.GetChild(0).gameObject.activeSelf == false)
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    gameObject.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}
