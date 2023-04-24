using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTime : MonoBehaviour
{
    private float m_time;
    private float timeGab = 0.1f;

    public float Time { get => m_time; set => m_time = value; }

    // Start is called before the first frame update
    void Start() {
        Time = 0;
        StartCoroutine(CoGameTime());
    }

    private IEnumerator CoGameTime() {
        while (true) {
            m_time += timeGab;

            yield return new WaitForSecondsRealtime(timeGab);
        }
    }
}
