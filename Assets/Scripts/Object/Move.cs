using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform transform;
    public float duration;
    Vector3 minpos;
    Vector3 maxpos;
        
    private void Awake()
    {
        transform = GetComponent<Transform>();
        minpos = transform.localPosition;
        minpos.x -= 10f;
        maxpos = transform.localPosition;
        maxpos.x += 10f;
    }
    void Update()
    {

        float smoothT = Mathf.Sin(Time.time * Mathf.PI / duration) * 0.5f + 0.5f;   //사인값을 이용해서 러프의 주기를 만듦(0~1)

        transform.position = Vector3.Lerp(minpos, maxpos, smoothT);
    }
}
