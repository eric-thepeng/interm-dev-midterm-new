using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryCounter : MonoBehaviour
{
    [SerializeField] string countText = "Amount of stories unlocked: ";
    List<string> alreadyGet = new List<string>();
    TMP_Text label;

    private void Start()
    {
        label = GetComponent<TMP_Text>();
    }

    public void newStory(string ID)
    {
        if (!alreadyGet.Contains(ID))
        {
            alreadyGet.Add(ID);
            label.text = countText + alreadyGet.Count;
        }
    }
}
