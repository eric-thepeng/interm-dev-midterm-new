using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartNextLevel : MonoBehaviour
{
    public int toWhich;
    public float countDownTime;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(NextLevelCountDown());
        }
    }

    IEnumerator NextLevelCountDown()
    {
        while (countDownTime >=0)
        {
            countDownTime -= Time.deltaTime;
            GetComponentInChildren<TMP_Text>().text = "next level in " + (int)countDownTime;
            yield return new WaitForSeconds(0);
        }
        FindObjectOfType<MenuLevelSelect>().QueLevel(toWhich);
    }
}
