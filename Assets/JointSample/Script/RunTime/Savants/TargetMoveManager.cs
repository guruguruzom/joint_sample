using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TargetMoveManager : MonoBehaviour, IDragHandler, IBeginDragHandler
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
    private InGameController inGameController;

    private int inputKey;

    Vector2 mouse;
    Vector2 beforePosition;
    bool isMouseInit = true;
    float m_positionGapX = 15;
    float m_positionGapY = 30;

    float m_positionGap = 1000;
    void Start()
    {
        //inGameController = InGameController.Instance;

        //if (compensationAxisHorizontal == null) 
        //{
        //    compensationAxisHorizontal = new Vector2(0, 0);
        //}
        //if (compensationAxisVertical == null)
        //{ 
        //    compensationAxisVertical = new Vector2(0, 0);
        //}

        //mouse = Input.mousePosition;
        //isMouseInit = true;
    }
    void Update()
    {
        

    //    Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
    //            Input.mousePosition.y, -Camera.main.transform.position.z));

    //    if (Input.GetMouseButton(0))
    //    {
    //        if (mouse.x != Input.mousePosition.x && mouse.y != Input.mousePosition.y)
    //        {
    //            if (isMouseInit)
    //            {
    //                isMouseInit = false;
    //                beforePosition = point;
    //                return;
    //            }
    //            //원래 값의 2차원 추출

    //            Vector2 distanceArm = new Vector2((point.x - beforePosition.x) * m_positionGapX, (point.y - beforePosition.y) * m_positionGapY);
    //            //targetHorizontalPos -= distanceArm.x;
    //            //targetVerticalPos += distanceArm.y;
    //            SetArmTarget(armTargetObserverL, point);

    //            beforePosition = point;
    //        }
            
    //        mouse = Input.mousePosition;
    //    }
        armTargetL.position = armTargetObserverL.position;

    }

    float beforeTime;
    float beforeDistance;
    float beforeDegree;
    float totalDistance;
    Coroutine m_targetMoveCoroutine;
    bool isTargetMoveCoroutine;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isTargetMoveCoroutine = false;
        beforePosition = eventData.position;
        beforeTime = InGameController.Instance.GetGameTime();
        //throw new System.NotImplementedException();
    }
    public void OnDrag(PointerEventData eventData)
    {
        //TODO : event 시스템으로 변경예정
        if (Input.GetMouseButton(0))
        {
            //시간이 같다면 무시
            float nowTime = InGameController.Instance.GetGameTime();
            if (nowTime == beforeTime) {
                return;
            }

            float distance = Vector2.Distance(beforePosition, eventData.position);
            //Vector2 distanceArm = new Vector2((pointPosition.x - beforeMouse.x) / m_positionGap, (pointPosition.y - beforeMouse.y) / m_positionGap);
            
            //시간을 통해 속도 계산
            float speed = (nowTime - beforeTime) * (distance / 1000);
            Debug.Log((nowTime - beforeTime) +":"+ distance);

            if (isTargetMoveCoroutine) {
                StopCoroutine(m_targetMoveCoroutine);
                isTargetMoveCoroutine = false;
            }
            m_targetMoveCoroutine = StartCoroutine(CoTargetMove(eventData.position, speed));

            beforeTime = InGameController.Instance.GetGameTime();
            beforePosition = eventData.position;
        }
        //throw new System.NotImplementedException();
    }

    private IEnumerator CoTargetMove(Vector2 pointPosition, float speed) {
        isTargetMoveCoroutine = true;
        //TODO : 단순 포지션 이동이 아닌 해상도 대비 비율값으로 변경필요
        Vector2 distanceArm = new Vector2((pointPosition.x - beforePosition.x) / m_positionGap, (pointPosition.y - beforePosition.y) / m_positionGap);

        Vector3 targetPosition = new Vector3(armTargetObserverL.position.x + distanceArm.x, armTargetObserverL.position.y + distanceArm.y, armTargetObserverL.position.z);

        while (isTargetMoveCoroutine) {
            armTargetObserverL.position = Vector2.Lerp(armTargetObserverL.position, targetPosition,  speed);//0.001

            yield return null;
        }
        isTargetMoveCoroutine = false;
    }



    private void SetArmTarget(Transform armTarget, Vector3 point)
    {
        Vector2 distanceArm = new Vector2((point.x - beforePosition.x) / m_positionGap, (point.y - beforePosition.y) / m_positionGap);
        //목표값 이동
        armTargetObserverL.position = new Vector3(armTargetObserverL.position.x + distanceArm.x, armTargetObserverL.position.y + distanceArm.y, armTargetObserverL.position.z);

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
