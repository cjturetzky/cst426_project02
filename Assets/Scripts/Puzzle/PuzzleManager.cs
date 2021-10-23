using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manager class for resetting the maze in battle
// Feel free to do whatever with this as the maze gets reworked, I just wanted to have a functional loop for the playtest
public class PuzzleManager : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnPoint;

    public static PuzzleManager Instance {private set ; get;}
    void Awake()
    {
        Instance = this;
    }

    public void Restart()
    {
        player.transform.position = spawnPoint.transform.position;
    }
}
