using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public static class PGTool
{
    public static Vector2 ChangeVector(Vector2 inVec, int which, float changeTo)
    {
        Vector2 outVec = inVec;
        if (which == 1) outVec.x = changeTo;
        else if (which == 2) outVec.y = changeTo;
        return outVec;
    }
    public static Vector3 ChangeVector(Vector3 inVec, int which, float changeTo)
    {
        Vector3 outVec = inVec;
        if (which == 1) outVec.x = changeTo;
        else if (which == 2) outVec.y = changeTo;
        else if (which == 3) outVec.z = changeTo;
        return outVec;
    }

}
