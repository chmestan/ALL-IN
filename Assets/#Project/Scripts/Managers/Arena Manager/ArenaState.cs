using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaState : MonoBehaviour
{
    public ArenaStateEnum state = ArenaStateEnum.Ongoing;
    PauseGame pauseScript;

    void Awake()
    {
        pauseScript = GetComponent<PauseGame>();
    }

    void Update()
    {
        state = pauseScript.paused? ArenaStateEnum.Paused : ArenaStateEnum.Ongoing;
    }
}
