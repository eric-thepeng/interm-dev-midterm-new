using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class w5c_player : MonoBehaviour
{
    public float speed = 3;
    Vector2 direction = new Vector2(1, 0);
    float raycastDistance = 0.6f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, direction*raycastDistance);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if(Physics2D.Raycast(transform.position, direction, raycastDistance).collider != null)
        {
            if (Physics2D.Raycast(transform.position, AlternateDirection(direction,0), raycastDistance).collider == null)
            {
                direction = AlternateDirection(direction, 0);
            }
            else if (Physics2D.Raycast(transform.position, AlternateDirection(direction, 1), raycastDistance).collider == null)
            {
                direction = AlternateDirection(direction, 1);
            }
        }
    }

    private Vector2 AlternateDirection(Vector2 inDir, int which)
    {
        if(inDir.x == 0)
        {
            if (which == 0) return (new Vector2(1, 0));
            else return (new Vector2(-1, 0));
        }
        if (inDir.y == 0)
        {
            if (which == 0) return (new Vector2(0, 1));
            else return (new Vector2(0, -1));
        }
        return new Vector2(0, 0);
    }
}
