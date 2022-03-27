using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SideScrollerBlockMovement : MonoBehaviour
{
    [SerializeField] bool damage = false;
    [SerializeField] bool rotation = false;
    [SerializeField] float rotationSpeed = 0f;
    [SerializeField] bool playerControl = false;
    [SerializeField] bool moveBySelf = true;
    [SerializeField] bool cycle = false;
    [SerializeField] float speed = 2;
    float zRotation = 0;
    bool move;
    Rigidbody2D rb;
    Pathpoint pp;
    SpriteRenderer sr;

    //for playercontrol
    Vector3 mouseDown, mouseNow, mousePoint, blockOrigional;
    bool canControl = false;
    //for playercontrol end

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (GetComponent<Pathpoint>()) pp = GetComponent<Pathpoint>();
        sr = GetComponent<SpriteRenderer>();
        move = moveBySelf;
    }
    void Start()
    {
        if (playerControl) { 
            GameEvent.i.onEnterMachineVision += OnEnterMachineVision;
            GameEvent.i.onExitMachineVision += OnExitMachineVision;
        }
        if (pp!=null)
        {
            transform.position = pp.CurrentPoint();
            pp.setCycle(cycle);
        }

        if (damage) sr.color = ColorPalettes.Red;
        else sr.color = ColorPalettes.Grey;
        
    }

    private void OnDestroy()
    {
        if (playerControl)
        {
            if(GameEvent.i != null) GameEvent.i.onEnterMachineVision -= OnEnterMachineVision;
            if (GameEvent.i != null) GameEvent.i.onExitMachineVision -= OnExitMachineVision;
        }
    }

    void Update()
    {
        if (playerControl)
        {

        }
        else
        {
            if (move) rb.velocity = ((pp.NextPoint() - pp.CurrentPoint()).normalized) * speed;
            if (rotation)
            {
                zRotation += rotationSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, zRotation), Time.deltaTime * 5f);
            }
        }
    }
    public void OnEnterMachineVision()
    {
        sr.color = ColorPalettes.Yellow;
        canControl = true;
        print("enter");
    }
    public void OnExitMachineVision()
    {
        sr.color = ColorPalettes.Grey;
        canControl = false;
        print("exit");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (damage && collision.gameObject.tag == "Player")
        {
            FindObjectOfType<LevelController>().PlayerDeath();
        }
    }

    private void OnMouseDown()
    {
        if (!playerControl || !canControl) return;
        mouseDown = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        blockOrigional = transform.position;
    }

    private void OnMouseDrag()
    {
        if (!playerControl || !canControl) return;
        mouseNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoint = mouseNow - mouseDown + blockOrigional;

        if (true) {
            pp.playerMoving = true;
            float finalX = mousePoint.x;
            float finalY = mousePoint.y;
            if(pp.CurrentPoint().x - pp.NextPoint().x!=0&& pp.CurrentPoint().y - pp.NextPoint().y != 0)
            {
                float a = (pp.CurrentPoint().y - pp.NextPoint().y) / (pp.CurrentPoint().x - pp.NextPoint().x);
                float b = pp.CurrentPoint().y - a * pp.CurrentPoint().x;
                float b2 = mousePoint.y - mousePoint.x * (-1 / a);
                finalX = (b2 - b) / (a - (-1 / a));
                finalY = a * finalX + b;
            }
            finalX = Mathf.Clamp(finalX, Mathf.Min(pp.CurrentPoint().x, pp.NextPoint().x), Mathf.Max(pp.CurrentPoint().x, pp.NextPoint().x));
            finalY = Mathf.Clamp(finalY, Mathf.Min(pp.CurrentPoint().y, pp.NextPoint().y), Mathf.Max(pp.CurrentPoint().y, pp.NextPoint().y));
            transform.position = new Vector3(finalX, finalY, transform.position.z);
        }
    }
}
