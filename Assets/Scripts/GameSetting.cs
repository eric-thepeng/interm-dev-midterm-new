using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    static GameSetting instance;
    public static GameSetting i
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameSetting>();
            return instance;
        }
    }
}

public static class ColorPalettes
{
    public static Color Blue = new Color(0.53f, 0.75f, 0.86f, 1f);
    public static Color Grey = new Color(0.5377358f, 0.5377358f, 0.5377358f, 1f);
    public static Color Yellow = new Color(0.8588235f, 0.7874829f, 0.5254902f, 1f);
    public static Color Red = new Color(1f, 0.5911949f, 0.5911949f);
    public static Color Purple = new Color(0.5730725f, 0.2704402f, 1f, 0.2745098f);
}
