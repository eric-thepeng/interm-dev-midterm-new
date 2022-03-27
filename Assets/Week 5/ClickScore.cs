using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class ClickScore : MonoBehaviour
{

    public TextMeshPro tmpText;

    public GameObject objectToTriggerThisSciprt;

    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        tmpText = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        tmpText.text = "Score: " + score;
    }

    public void AddScoreByOne()
    {
        score++;
    }

    public void AddScoreByTwo()
    {
        score += 2;
    }

}
