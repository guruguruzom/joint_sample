using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMoveManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] jointPoint = new Transform[3];


    [SerializeField]
    private Transform virtualTracker;

    [SerializeField]
    private Transform endTargetTracker;

    private float frontSection;
    private float backSection;

    // Start is called before the first frame update
    void Start()
    {
        frontSection = Vector3.Distance(jointPoint[2].position, jointPoint[1].position);
        backSection = Vector3.Distance(jointPoint[1].position, jointPoint[0].position);
    }
    public virtual void SetFrontJointRotate(){ }
    public virtual void SetBackJointRotate(float distance0_2, float degreeHorizons_1){ }
    // Start is called before the first frame update
}
