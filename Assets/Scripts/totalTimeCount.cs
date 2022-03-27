using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class totalTimeCount : MonoBehaviour
{
    float totalTime = 0;
    void Start()
    {
        StartCoroutine(CountTime());
    }

    IEnumerator CountTime()
    {
        while (true)
        {
            totalTime += Time.deltaTime;
            GetComponent<TMP_Text>().text = "It's been moving for: " + (int)totalTime + " sec";
            yield return new WaitForSeconds(0);
        }
    }
}
