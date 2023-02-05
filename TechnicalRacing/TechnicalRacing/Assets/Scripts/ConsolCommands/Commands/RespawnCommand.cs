using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Log Command", menuName = "devCommands/Respawn Command")]
public class RespawnCommand : ConsoleCommand
{
    public override bool Process(string[] args)
    {
        if(args[0] == "here")
        {
            GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];

            Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y + 2f, player.transform.position.z);
            Quaternion newRot = Quaternion.Euler(0, 0, 0);

            player.transform.position = newPos;
            player.transform.rotation = newRot;

            return true;
        }
        else
        {
            if(args[0] == "spawn")
            {
                GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
                Vector3 newPos = new Vector3(0, 3, 0);
                Quaternion newRot = Quaternion.Euler(0, 0, 0);

                player.transform.position = newPos;
                player.transform.rotation = newRot;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
