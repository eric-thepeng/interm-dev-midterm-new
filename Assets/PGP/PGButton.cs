using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PGButton : MonoBehaviour
{
    [SerializeField] SpriteRenderer pic;
    [SerializeField] TextMeshPro text;
    public int ToLevel;

    enum state {Normal, Dim, Dark}
    state stateNow = state.Normal;

    Dictionary<string, Color> colorList = new Dictionary<string, Color> {
        {"normal", new Color(1,1,1,1)},
        {"dim", new Color(0.8f,0.8f,0.8f,1)},
        {"dark", new Color(0.3f,0.3f,0.3f,1)},
    };

    public void onClick()
    {
        print("click");
        MenuLevelSelect.i.QueLevel(ToLevel);
    }

    void switchState(state to)
    {
        if(stateNow != to)
        {
            stateNow = to;
            if(stateNow == state.Normal) pic.color = colorList["normal"];
            else if (stateNow == state.Dim) pic.color = colorList["dim"];
            else if (stateNow == state.Dark) pic.color = colorList["dark"];
        }
    }


    private void OnMouseExit()
    {
        if (stateNow != state.Dark) switchState(state.Normal);
    }
    private void OnMouseEnter()
    {
        if (stateNow != state.Dark) switchState(state.Dim);
    }

    private void OnMouseUp()
    {
        switchState(state.Normal);
        onClick();
    }

    private void OnMouseOver()
    {
        if (stateNow != state.Dark) switchState(state.Dim);
    }

    private void OnMouseDown()
    {
        switchState(state.Dark);
    }

    private void OnMouseDrag()
    {
        switchState(state.Dark);
    }

}
