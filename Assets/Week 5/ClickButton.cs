using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    List<Color> colors = new List<Color>() { Color.blue, Color.red, Color.cyan, Color.gray, Color.green, Color.magenta } ;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomColor();
        }
    }

    public void RandomColor()
    {
        GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Count - 1)];
    }

    public void OneColor()
    {
        GetComponent<SpriteRenderer>().color = colors[2];
    }
}
