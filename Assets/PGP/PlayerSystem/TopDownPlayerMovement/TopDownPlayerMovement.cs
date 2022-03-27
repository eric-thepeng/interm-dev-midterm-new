using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerMovement : MonoBehaviour
{

    //----------SETTINGS----------//
    //移动方向:
    public enum eDirectionConstrain { Four, Eight }; //4: 上下左右四向移动; 8: 左上左下右上右下允许八向斜着
    public eDirectionConstrain DIRECTION_CONSTRAIN = eDirectionConstrain.Four;

    //是否应用鼠标于旋转和变换朝向
    public bool CURSOR_CONTROL = true;
    public bool CURSOR_INDICATOR = true;
    public enum eRotationMode { Instant, Fix, Ratio } //"INSTANT":旋转锁死鼠标 "FIX":固定旋转速度 "RATIO":速度与距离成正比
    public eRotationMode ROTATION_MODE = eRotationMode.Instant;
    GameObject rotateComp;
    GameObject cursorComp;
    Vector2 cursorDir;
    public float rotationSpeed = 150;
    //----------SETTINGS ENDS----------//
    static int PlayerCount = 1;
    static int PlayerID = 1;

    bool canMove = true;
    public float movingSpeed = 5;
    float momentum = 0; //between 0 and 1
    public float momentumIndex = 5; //how fast accelerates to max speed
    Vector2 movingDir, inputNow = new Vector2(0, 0);

    private void Awake()
    {
        PlayerID = PlayerCount;
        PlayerID += 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        rotateComp = transform.Find("Rotate Comp").gameObject;
        cursorComp = transform.Find("Cursor Comp").gameObject;
        if (!CURSOR_CONTROL) rotateComp.SetActive(false);
        if (!CURSOR_INDICATOR) cursorComp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (CURSOR_CONTROL)
        {
            //Get Cursor Data
            cursorDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            cursorDir = VectorChangeSingle(cursorDir, 3, 0);
            float angle = Mathf.Atan2(cursorDir.y, cursorDir.x) * Mathf.Rad2Deg;

            //Rotation of RotateComp
            if (ROTATION_MODE == eRotationMode.Instant)
            {
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                rotateComp.transform.rotation = rotation;
            }
            else if (ROTATION_MODE == eRotationMode.Fix)
            {
                float currentAngle = ChangeToRotateFromRight(rotateComp.transform.rotation.eulerAngles.z);
                if (Mathf.Abs(angle - currentAngle) >= 3)
                {
                    rotateComp.transform.rotation = Quaternion.AngleAxis(currentAngle + rotationSpeed * (Mathf.Sign(angle - currentAngle)) * Time.deltaTime, Vector3.forward);
                }
            }
            else if (ROTATION_MODE == eRotationMode.Ratio)
            {
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                rotateComp.transform.rotation = Quaternion.Lerp(rotateComp.transform.rotation, rotation, rotationSpeed / 20 * Time.deltaTime);
            }

            if (CURSOR_INDICATOR)
            {
                cursorComp.transform.position = VectorChangeSingle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 3, 0);
            }
            ChangeFacing(cursorDir);
        }

        if (DIRECTION_CONSTRAIN == eDirectionConstrain.Eight) //Get current input
        {
            inputNow = new Vector2(0, 0);
            if (Input.GetKey(KeyCode.W)) inputNow.y += 1;
            if (Input.GetKey(KeyCode.S)) inputNow.y -= 1;
            if (Input.GetKey(KeyCode.A)) inputNow.x -= 1;
            if (Input.GetKey(KeyCode.D)) inputNow.x += 1;
        }
        else if (DIRECTION_CONSTRAIN == eDirectionConstrain.Four)
        {
            if (Input.GetKeyDown(KeyCode.W)) inputNow = new Vector2(0, 1);
            else if (Input.GetKeyDown(KeyCode.S)) inputNow = new Vector2(0, -1);
            else if (Input.GetKeyDown(KeyCode.A)) inputNow = new Vector2(-1, 0);
            else if (Input.GetKeyDown(KeyCode.D)) inputNow = new Vector2(1, 0);
            else
            {
                List<Vector2> pressedKeys = new List<Vector2>();
                if (Input.GetKey(KeyCode.W)) pressedKeys.Add(new Vector2(0, 1));
                if (Input.GetKey(KeyCode.S)) pressedKeys.Add(new Vector2(0, -1));
                if (Input.GetKey(KeyCode.A)) pressedKeys.Add(new Vector2(-1, 0));
                if (Input.GetKey(KeyCode.D)) pressedKeys.Add(new Vector2(1, 0));
                if (pressedKeys.Count == 0) inputNow = new Vector2(0, 0);
                if (pressedKeys.Count == 1 && inputNow != pressedKeys[0]) inputNow = pressedKeys[0];
            }
        }

        //Calculate current momentum
        if (inputNow.magnitude == 0) //no movement key pressed 
        {
            momentum -= momentumIndex * Time.deltaTime;
        }
        else //movement key pressed
        {
            if (inputNow != movingDir) //there is a direction change
            {
                if (!CURSOR_CONTROL) ChangeFacing(inputNow);
                movingDir = inputNow;
            }
            momentum += momentumIndex * Time.deltaTime;
        }
        momentum = Mathf.Clamp(momentum, 0, 1);

        //Move
        if (GetComponent<Rigidbody2D>().velocity != movingDir.normalized * movingSpeed * momentum)
        {
            GetComponent<Rigidbody2D>().velocity = movingDir.normalized * movingSpeed * momentum;
        }
    }

    void ChangeFacing(Vector2 changeTo)
    {
        if (CURSOR_CONTROL)
        {
            float calAngle = Vector2.Angle(changeTo, new Vector2(1, 0));
            if (changeTo.y <= 0)
            {
                calAngle = 360 - calAngle;
            }
            if (0 <= calAngle && calAngle <= 22.5) ChangeFaceTo(new Vector2(1, 0));
            else if (22.5 < calAngle && calAngle <= 67.5) ChangeFaceTo(new Vector2(1, 1));
            else if (67.5 < calAngle && calAngle <= 112.5) ChangeFaceTo(new Vector2(0, 1));
            else if (112.5 < calAngle && calAngle <= 157.5) ChangeFaceTo(new Vector2(-1, 1));
            else if (157.5 < calAngle && calAngle <= 202.5) ChangeFaceTo(new Vector2(-1, 0));
            else if (202.5 < calAngle && calAngle <= 247.5) ChangeFaceTo(new Vector2(-1, -1));
            else if (247.5 < calAngle && calAngle <= 292.5) ChangeFaceTo(new Vector2(0, -1));
            else if (292.5 < calAngle && calAngle <= 337.5) ChangeFaceTo(new Vector2(1, -1));
            else if (337.5 < calAngle && calAngle <= 360) ChangeFaceTo(new Vector2(1, 0));
        }
        else
        {
            ChangeFaceTo(changeTo);
        }

        void ChangeFaceTo(Vector2 changeTo) {
            /*
            if (changeTo == new Vector2(0, 1)) print("change facing to 正上");
            else if (changeTo == new Vector2(1, 1)) print("change facing to 右上");
            else if (changeTo == new Vector2(1, 0)) print("change facing to 正右");
            else if (changeTo == new Vector2(1, -1)) print("change facing to 右下");
            else if (changeTo == new Vector2(0, -1)) print("change facing to 正下");
            else if (changeTo == new Vector2(-1, -1)) print("change facing to 左下");
            else if (changeTo == new Vector2(-1, 0)) print("change facing to 正左");
            else if (changeTo == new Vector2(-1, 1)) print("change facing to 左上");
            */
        }
    }

    Vector3 VectorChangeSingle(Vector3 origional, int which, float toWhat)
    {
        Vector3 output = origional;
        if (which == 1)
        {
            output = new Vector3(toWhat, output.y, output.z);
        }
        else if (which == 2)
        {
            output = new Vector3(output.x, toWhat, output.z);
        }
        else if (which == 3)
        {
            output = new Vector3(output.x, output.y, toWhat);
        }
        return output;
    }
    Vector2 VectorChangeSingle(Vector2 origional, int which, float toWhat)
    {
        Vector2 output = origional;
        if (which == 1)
        {
            output = new Vector2(toWhat, output.y);
        }
        else if (which == 2)
        {
            output = new Vector2(output.x, toWhat);
        }
        return output;
    }

    float ChangeToRotateFromRight(float z)
    {
        float output = z % 360;
        if (Mathf.Abs(z) > 180)
        {
            if (z > 0) z = z - 360;
            else z = z + 360;
        }
        return z;
    }

    //Following actions can be tested solely but should be called from other classes such as a Finite State Machine//
    void FreezeMovement()
    {

    }

    void RestoreMovement()
    {

    }

}
