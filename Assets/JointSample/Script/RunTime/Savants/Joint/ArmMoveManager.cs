using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMoveManager : MonoBehaviour, IMoveManager
{
    [SerializeField]
    private Transform[] jointPoint = new Transform[3];
    //0:wrist, 1:elbow, 2:shoulder


    [SerializeField]
    private Transform virtualTracker;

    [SerializeField]
    private SpineMoveManager spineMoveManager;


    private float frontSection;
    private float backSection;

    // Start is called before the first frame update
    void Start()
    {
        frontSection = Vector3.Distance(jointPoint[2].position, jointPoint[1].position);
        backSection = Vector3.Distance(jointPoint[1].position, jointPoint[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        SetFrontJointRotate();
    }

    //TODO : wristElbowDistance elbowShoulderDistance ��� class�� ����
    //�Լ� �������̽�ȭ
    public void SetFrontJointRotate() {
        //������ǥ ����
        Vector3 trackerVector = virtualTracker.localPosition;
        Vector3 shoulderVector = jointPoint[0].localPosition;

        //ǥ���� ����� ���� ��ǥ ���� ����(�۾���, new vector�� ���������)
        float horizonDistance = Vector2.Distance(new Vector2(trackerVector.x, trackerVector.y), new Vector2(shoulderVector.x, shoulderVector.y));
        //ǥ������ ����ġ�� ����(�۾���, ��ǥ ���������) {��� ���� ��, 2���� �Ÿ�, ������ ��}
        Vector3 pos = new Vector3(jointPoint[0].localPosition.x, horizonDistance, virtualTracker.localPosition.z);
        //������ ǥ������ �ΰ� globalShoulderDegreeVertical, globalShoulderDegreeHorizons ���� �� ���� �����Ͽ� �Ȱ��� ����
        //���� ȸ�� ����
        Vector3 v2 = pos - jointPoint[0].localPosition;
        
        float globalShoulderDegreeVertical = CalculationFormulaUtil.GetAngleVertical(v2.y, v2.z);

        Vector2 v3 = new Vector2(trackerVector.x, trackerVector.y) - new Vector2(shoulderVector.x, shoulderVector.y);
        float degreeHorizons_1 = CalculationFormulaUtil.GetAngleVertical(v3.x, v3.y);

        float distance0_2 = Vector3.Distance(virtualTracker.localPosition, jointPoint[0].localPosition);
        float shoulderArmDegree = CalculationFormulaUtil.GetEightDegree(frontSection, backSection, distance0_2);

        float shoulderDegree = globalShoulderDegreeVertical + shoulderArmDegree;
        if (float.IsNaN(shoulderDegree))
        {
            return;
        }

        
        //Debug.Log(virtualTracker.localRotation);

        //return;
        jointPoint[0].localRotation = Quaternion.Euler(new Vector3(-degreeHorizons_1, -90, -shoulderDegree));

        SetBackJointRotate(distance0_2, degreeHorizons_1);
    }


    public void SetBackJointRotate(float distance0_2, float degreeHorizons_1)
    {
        float elbowDegree = CalculationFormulaUtil.GetEightDegree(distance0_2, frontSection, backSection);
        //x��ǥ ȸ���� ��� �ٽ�

        jointPoint[1].localRotation = Quaternion.Euler(new Vector3(0, 0, 180 - elbowDegree));
        jointPoint[1].Rotate(-degreeHorizons_1, 0, 0);

        spineMoveManager.SetSpineVaticalTarget(-virtualTracker.localPosition.x * 20);
    }
}



