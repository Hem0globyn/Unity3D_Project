using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public LayerMask layerMask;
    Rigidbody rb;
    PlayerCondition playerCondition;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, new Vector3(1, 0, 0));    //x축으로 발사
        if (Physics.Raycast(ray, out hit, 5f, layerMask)) //10f는 레이저의 길이
        {
            pushBack(hit);
        }
    }
    
    void pushBack(RaycastHit hit)
    {
        Debug.Log("레이저에 맞음");
        Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
        PlayerCondition playerCondition = hit.collider.gameObject.GetComponent<PlayerCondition>();
        Transform playerTransform = hit.collider.gameObject.GetComponent<Transform>();
        PlayerController controller = hit.collider.gameObject.GetComponent<PlayerController>();

        controller.StopForSec(); //플레이어의 이동을 비활성화
        playerCondition.Eat(100f); //배고픔 회복
        playerCondition.Heal(100f); //체력도 회복
        rb.velocity = Vector3.zero; //플레이어의 속도를 0으로 초기화
        Vector3 oppositeForce = (-playerTransform.forward + Vector3.up).normalized; //플레이어의 반대 방향으로 힘을 줌
        rb.AddForce(oppositeForce*100f, ForceMode.Impulse); //플레이어를 밀어냄
    }
}
