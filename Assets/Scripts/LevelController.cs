using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] Transform playerTf, cameraTf;
    [SerializeField] bool CameraFollowsPlayer = false;
    [SerializeField] Transform defaultRebornPos;

    Vector3 rebornPos;

    public void Start()
    {
        rebornPos = defaultRebornPos.position;
    }

    public void PlayerDeath()
    {
        FindObjectOfType<SideScrollerPlayerMovement>().coT = 10;
        FindObjectOfType<MachineVision>().ExitVision();
        PGSoundManager.PlaySound("Die", false, 0.4f);
        playerTf.position = PGTool.ChangeVector(rebornPos, 3, playerTf.position.z);
    }

    public void newRebornPos(Vector3 newPos)
    {
        rebornPos = newPos;
    }
}
