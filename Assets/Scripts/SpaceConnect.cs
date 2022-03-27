using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceConnect : MonoBehaviour
{
    Vector3 rebornPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        rebornPos = transform.GetChild(0).position;
        FindObjectOfType<LevelController>().newRebornPos(rebornPos);
    }
}
