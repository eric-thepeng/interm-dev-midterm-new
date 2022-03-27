using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineVision : MonoBehaviour
{
    SideScrollerPlayerMovement player;
    [SerializeField] float maxTime, regainSpeed;
    float leftTime;
    public bool inVision = false;
    [SerializeField]SpriteRenderer AvatarSR;
    [SerializeField]Transform IndicatorTF;

    private void Start()
    {
        leftTime = maxTime;
        player = GetComponent<SideScrollerPlayerMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EnterVision();
        }

        if (inVision)
        {
            leftTime -= Time.deltaTime/Time.timeScale;
            leftTime = Mathf.Clamp(leftTime,0,maxTime);
            IndicatorTF.localScale = PGTool.ChangeVector(IndicatorTF.localScale, 1, leftTime / maxTime);
            if (leftTime <= 0) ExitVision();
        }
        else
        {
            leftTime += Time.deltaTime*regainSpeed / Time.timeScale;
            leftTime = Mathf.Clamp(leftTime, 0, maxTime);
            IndicatorTF.localScale = PGTool.ChangeVector(IndicatorTF.localScale, 1, leftTime / maxTime);
        }

        if (Input.GetMouseButtonUp(1))
        {
            ExitVision();
        }
    }

    void EnterVision()
    {
        if (inVision) return;
        PGSoundManager.PlaySound("Open");
        GameEvent.i.EnterMachineVision();
        inVision = true;
        Time.timeScale = 0.3f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        AvatarSR.color = new Color(0.70f, 0.95f, 0.74f, 1f);
        player.canDash = true;
    }
    public void ExitVision()
    {
        if (!inVision) return;
        PGSoundManager.PlaySound("Close");
        GameEvent.i.ExitMachineVision();
        inVision = false;
        Time.timeScale = 1f;
        AvatarSR.color = new Color(0.46f, 0.76f, 0.51f, 1f);
        player.canDash = false;
    }
}
