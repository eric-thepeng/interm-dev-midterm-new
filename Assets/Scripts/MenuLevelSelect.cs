using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevelSelect : MonoBehaviour
{
    static MenuLevelSelect instance;
    string currentLevel = "";
    public static MenuLevelSelect i
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<MenuLevelSelect>();
            return instance;
        }
    }

    List<string> allScenesName = new List<string>() {
    "Level1",
    "Level2",
    "Level3",
    "Level4",
    "Level5"};

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.Q)) GoToLevelSelection();
    }

    void GoToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelectionSecond", LoadSceneMode.Single);
    }

    public void QueLevel(int which)
    {
        if (which == 7)
        {
            QueWin();
        }
        else if (which == 6)
        {
            GoToLevelSelection();
        }
        else if (which <= allScenesName.Count)
        {
            currentLevel = allScenesName[which - 1];
            SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
        }
        
    }

    public void NextLevel()
    {
        if (currentLevel == "Level1") { QueLevel(2); }
        else if (currentLevel == "Level2") { QueLevel(3); }
        else if (currentLevel == "Level3") { QueLevel(4); }
        else if (currentLevel == "Level4") { QueWin(); }
    }

    public void QueWin()
    {
        SceneManager.LoadScene("Win", LoadSceneMode.Single);
    }

    public void QueLose()
    {
        SceneManager.LoadScene("Lose", LoadSceneMode.Single);
    }
}
