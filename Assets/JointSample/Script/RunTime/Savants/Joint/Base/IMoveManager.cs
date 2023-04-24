using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveManager
{
    public void SetFrontJointRotate();
    public void SetBackJointRotate(float distance0_2, float degreeHorizons_1);
}
