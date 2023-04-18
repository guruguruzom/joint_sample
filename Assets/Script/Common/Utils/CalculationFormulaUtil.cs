using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationFormulaUtil
{
    public static float CalculateAngle(Vector3 from, Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - from).eulerAngles.z;
    }

    public static float GetAngleVertical(float start, float end)
    {
        float degree = Mathf.Atan2(start, end) * Mathf.Rad2Deg;
        return degree + 180;
    }

    public static float GetEightDegree(float a, float b, float c)
    {
        float cosA = (Mathf.Pow(b, 2) + Mathf.Pow(c, 2) - Mathf.Pow(a, 2)) / (2 * b * c);

        return Mathf.Acos(cosA) * Mathf.Rad2Deg;
    }
}
