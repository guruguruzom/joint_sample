using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMoveManager : MonoBehaviour
{
    [SerializeField]
    private Transform armTargetL;

    [SerializeField]
    private Transform armTargetObserverL;

    [SerializeField]
    private float circleR; 

    [SerializeField]
    private float targetVerticalPos; //각도

    [SerializeField]
    private float targetHorizontalPos; //각도

    [SerializeField]
    private Vector2 compensationAxisHorizontal;

    [SerializeField]
    private Vector2 compensationAxisVertical;
    //[SerializeField]
    //private float objSpeed; //원운동 속도

    Vector2 mouse;
    Vector2 beforeMouse;
    bool isMouseInit = true;
    float m_positionGapX = 15;
    float m_positionGapY = 30;

    float m_positionGap = 10;
    void Start()
    {
        if (compensationAxisHorizontal == null) 
        {
            compensationAxisHorizontal = new Vector2(0, 0);
        }
        if (compensationAxisVertical == null)
        { 
            compensationAxisVertical = new Vector2(0, 0);
        }

        mouse = Input.mousePosition;
        isMouseInit = true;
    }
    void Update()
    {
        

        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

        if (Input.GetMouseButton(0))
        {
            if (mouse.x != Input.mousePosition.x && mouse.y != Input.mousePosition.y)
            {
                if (isMouseInit)
                {
                    isMouseInit = false;
                    beforeMouse = point;
                    return;
                }
                //원래 값의 2차원 추출

                Vector2 distanceArm = new Vector2((point.x - beforeMouse.x) * m_positionGapX, (point.y - beforeMouse.y) * m_positionGapY);
                //targetHorizontalPos -= distanceArm.x;
                //targetVerticalPos += distanceArm.y;
                SetArmTarget(armTargetObserverL, point);

                beforeMouse = point;
            }
            
            mouse = Input.mousePosition;
        }
        armTargetL.position = armTargetObserverL.position;

    }

    private void SetArmTarget(Transform armTarget, Vector3 point)
    {
        Vector2 distanceArm = new Vector2((point.x - beforeMouse.x) / m_positionGap, (point.y - beforeMouse.y) / m_positionGap);

        //목표값 이동
        armTargetObserverL.position = new Vector3(armTargetObserverL.position.x + distanceArm.x, armTargetObserverL.position.y + distanceArm.y, armTargetObserverL.position.z);

        //Vector3 armTargetPosition = armTarget.localPosition;

        //(float a, float b) targetHorizontalCoordinate = SetMoveDegree(targetHorizontalPos, compensationAxisHorizontal);


        //armTarget.transform.localPosition = new Vector3(armTargetPosition.x, targetHorizontalCoordinate.a * 3, targetHorizontalCoordinate.b * 3);

        //armTargetPosition = armTarget.transform.localPosition;
    }


    private void SetLeftArmTarget(Transform armTarget) {
        Vector3 armTargetPosition = armTarget.localPosition;

        (float a, float b) targetHorizontalCoordinate = SetMoveDegree(targetHorizontalPos, compensationAxisHorizontal);


        armTarget.transform.localPosition = new Vector3(armTargetPosition.x, targetHorizontalCoordinate.a * 3, targetHorizontalCoordinate.b * 3);

        armTargetPosition = armTarget.transform.localPosition;

    }

    private (float a, float b) SetMoveDegree(float targetPos, Vector2 compensationAxis)
    {
        var rad = Mathf.Deg2Rad * (targetPos);
        var a = circleR * Mathf.Sin(rad) + compensationAxis.x;
        var b = circleR * Mathf.Cos(rad) + compensationAxis.y;

        return (a, b);// new Vector2(a, b);
    }


    // Start is called before the first frame update


    // Update is called once per frame
}
