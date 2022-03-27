using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PieceOfAdvice : MonoBehaviour
{
    [SerializeField] TextMeshPro targetText;
    [SerializeField] string storyID;
    StoryCounter counter;
    bool activated = false;

    Dictionary<string, string> allAdvices = new Dictionary<string, string>()
    {
        // 4 digit, first 2: level, last 2: order

        //level 1
        {"0101", "Stop at purple for a piece of story!"},
        {"0102", "Avoid the red blocks! They will kill you!"},
        {"0103", "You're welcomed!"},
        {"0104", "You don't know where you are, do you?"},
        {"0105", "All these colored boxes lying around you doing different things when you touch them"},
        {"0106", "Don't worry! You're doing fine!"},
        {"0107", "Right click and hold to enter a special vision, Left click on the yellow objects to move them around!"},
        {"0108", "You got it!"},
        {"0109", "Ok ok fine, I'll tell you the truth. You're in a bioengineering lab."},
        {"0110", "I won't tell you who put you here though."},
        {"0111", "But don't freak out, consider this, you know, an experience."},
        {"0112", "Because life is easy here, not anywhere else."},
        {"0113", "Because here, you can pave your own path."},
        {"0114", "Turly, and quite literally, deciding what you walk on and where you are heading."},
        {"0115", "Have the entire world at your disposal."},
        {"0116", "You can progress with purpose and thrive with faith."},
        {"0117", "Aren't you truly in love with the lab yet?"},
         {"0118", "Do you know where you are going?"},
        {"0119", "Or do you really want to know?"},
        {"0120", "Congradulation! You made your escape!!! You win!! But now what? Now where you wanna go? Awwww are you missing us already? [level complete]"},

        //level 2
        {"0201","Hey you are back! Welcome!" },
        {"0202","Oh what? You say you can't get out anyway?" },
        {"0203","Hey hey lift up your spirit! This experiments remind me of... the show... ah darn it what's the name of it..." },
        {"0204","You know, the show with people flying around, crazy strength? Some fighter something? That show?" },
        {"0205","Oh yes! American Ninja Worrier" },
        {"0206","Ahhh don't we love this!" },
        {"0207","Have you wondered, you know, why are you here?" },
        {"0208","I know I know, you don't exactly have a choice." },
        {"0209","But what if you do?" },
        {"0210","Wait wait... I don't mean to send you away... please stayyyyyy!!!!!" },
        {"0211","[level complete]" },
    };

    private void Start()
    {
        counter = FindObjectOfType<StoryCounter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        targetText.text = allAdvices[storyID];
        counter.newStory(storyID);
        if(activated == false)
        {
            activated = true;
            Color newColor = GetComponent<SpriteRenderer>().color;
            newColor.a = newColor.a / 3;
            GetComponent<SpriteRenderer>().color = newColor;
            PGSoundManager.PlaySound("Get");
        }
    }
}
