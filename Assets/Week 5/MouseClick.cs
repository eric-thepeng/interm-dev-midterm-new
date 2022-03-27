using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //print(ray);
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit)
            {
                print("hit");
                if (hit.transform.gameObject.GetComponent<ClickButton>() != null)
                {
                    print("hit");
                    hit.transform.gameObject.GetComponent<ClickButton>().RandomColor();
                    FindObjectOfType<ClickScore>().AddScoreByOne();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (hit)
            {
                print("hit");
                if (hit.transform.gameObject.GetComponent<ClickButton>() != null)
                {
                    print("hit");
                    hit.transform.gameObject.GetComponent<ClickButton>().OneColor();
                    FindObjectOfType<ClickScore>().AddScoreByTwo();
                }
            }
        }
    }
}
