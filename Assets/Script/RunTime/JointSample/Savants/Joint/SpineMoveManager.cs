using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineMoveManager : MonoBehaviour
{
    [SerializeField]
    private Transform realTargetPoint;

    [SerializeField]
    private Transform targetObserverPoint;

    [SerializeField]
    private Transform[] spineJoint;

    private float yPos;
    private float xPos;

    //[SerializeField]
    //private float objSpeed; //��� �ӵ�

    Vector2 mouse;
    Vector2 beforeMouse;
    bool isMouseInit = true;
    float m_positionGapX = 15;
    float m_positionGapY = 30;

    float m_positionGap = 1;
    void Start()
    {
        mouse = Input.mousePosition;
        isMouseInit = true;
    }
    void Update()
    {


        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));

        if (Input.GetMouseButton(1))
        {
            if (mouse.x != Input.mousePosition.x && mouse.y != Input.mousePosition.y)
            {
                if (isMouseInit)
                {
                    isMouseInit = false;
                    beforeMouse = point;
                    return;
                }
                //���� ���� 2���� ����

                SetSpineHorizontalTarget(targetObserverPoint, point);

                beforeMouse = point;
            }

            mouse = Input.mousePosition;
        }
        //else {
        //    targetObserverPoint
        //}
        //realTargetPoint.position = targetObserverPoint.position;

    }

    //-45 45 ���Ʒ� 90 -45
    private void SetSpineHorizontalTarget(Transform targetObserverPoint, Vector3 point)
    {
        Vector2 distanceArm = new Vector2((point.x - beforeMouse.x) / m_positionGap, (point.y - beforeMouse.y) / m_positionGap);
        Debug.Log(distanceArm);

        xPos += (point.x - beforeMouse.x) * 10;
        yPos += (point.y - beforeMouse.y) * 10;
        //��ǥ�� �̵�
        //targetObserverPoint.localPosition = new Vector3(targetObserverPoint.localPosition.x + distanceArm.x, targetObserverPoint.localPosition.y + distanceArm.y, targetObserverPoint.localPosition.z );
        //Debug.Log(xPos + ":" + yPos);
        foreach (Transform transform in spineJoint) {
            transform.localRotation = Quaternion.Euler(new Vector3(0, xPos, -yPos));
        }
    }

    public void SetSpineVaticalTarget(float degree)
    {
        foreach (Transform transform in spineJoint)
        {
            Vector3 vector3 = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(new Vector3(degree, vector3.y, vector3.z));
            //transform.Rotate(45,0,0);
        }
    }
    //[SerializeField]
    //private float test0;

    //[SerializeField]
    //private float test1;

    //[SerializeField]
    //private float test2;

    private void SetSpine() { 
        //1.Ÿ������ �ٶ󺸴� ���ϴ� spine���� ���Ѵ�
        //2.�������� ���� ������ �����ش�
    }
}
