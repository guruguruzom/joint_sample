using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPositionController : MonoBehaviour
{
    [SerializeField] Transform upperarmL;
    [SerializeField] Transform upperarmR;
    void Update()
    {
        float moveZ = 0f;

        float moveX = 0f;

        if (Input.GetKey(KeyCode.W))

        {

            moveZ += 0.1f;

        }



        if (Input.GetKey(KeyCode.S))

        {

            moveZ -= 0.1f;

        }



        if (Input.GetKey(KeyCode.A))

        {
            moveX -= 0.1f;
        }



        if (Input.GetKey(KeyCode.D))
        {
            moveX += 0.1f;
        }



        transform.Translate(new Vector3(moveX, 0f, moveZ) * 0.1f);

        
        if (Input.GetKey(KeyCode.Z))
        {
            Quaternion rotation = upperarmL.transform.localRotation;
            Vector3 vector3 = rotation.eulerAngles;
            vector3.z += 0.05f;
            upperarmL.localRotation = Quaternion.Euler(vector3);
            //moveX += 0.1f;
        }
        if (Input.GetKey(KeyCode.C))
        {
            Quaternion rotation = upperarmL.transform.localRotation;
            Vector3 vector3 = rotation.eulerAngles;
            vector3.z -= 0.05f;
            upperarmL.localRotation = Quaternion.Euler(vector3);
            //moveX += 0.1f;
        }

        if (Input.GetKey(KeyCode.X))
        {
            Quaternion rotation = upperarmL.transform.localRotation;
            Vector3 vector3 = rotation.eulerAngles;
            vector3.x += 0.05f;
            upperarmL.localRotation = Quaternion.Euler(vector3);
            //moveX += 0.1f;
        }
        if (Input.GetKey(KeyCode.V))
        {
            Quaternion rotation = upperarmL.transform.localRotation;
            Vector3 vector3 = rotation.eulerAngles;
            vector3.x -= 0.05f;
            upperarmL.localRotation = Quaternion.Euler(vector3);
            //moveX += 0.1f;
        }
        
        if (Input.GetKey(KeyCode.Y))
        {

            Quaternion rotation = upperarmL.transform.localRotation;
            Vector3 vector3 = rotation.eulerAngles;
            vector3.y += 0.05f;
            upperarmL.localRotation = Quaternion.Euler(vector3);
            //moveX += 0.1f;
        }
        if (Input.GetKey(KeyCode.U))
        {

            Quaternion rotation = upperarmL.transform.localRotation;
            Vector3 vector3 = rotation.eulerAngles;
            vector3.y -= 0.05f;
            upperarmL.localRotation = Quaternion.Euler(vector3);
            //moveX += 0.1f;
        }
    }
}

