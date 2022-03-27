using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private static GameEvent instance;
    public static GameEvent i
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameEvent>();
            return instance;
        }
    }

    public event Action onEnterMachineVision, onExitMachineVision;
    public void EnterMachineVision()
    {
        if (onEnterMachineVision != null) onEnterMachineVision();
    }
    public void ExitMachineVision()
    {
        if (onExitMachineVision != null) onExitMachineVision();
    }

}
