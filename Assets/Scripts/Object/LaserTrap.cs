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
        ray = new Ray(transform.position, new Vector3(1, 0, 0));    //x������ �߻�
        if (Physics.Raycast(ray, out hit, 5f, layerMask)) //10f�� �������� ����
        {
            pushBack(hit);
        }
    }
    
    void pushBack(RaycastHit hit)
    {
        Debug.Log("�������� ����");
        Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
        PlayerCondition playerCondition = hit.collider.gameObject.GetComponent<PlayerCondition>();
        Transform playerTransform = hit.collider.gameObject.GetComponent<Transform>();
        PlayerController controller = hit.collider.gameObject.GetComponent<PlayerController>();

        controller.StopForSec(); //�÷��̾��� �̵��� ��Ȱ��ȭ
        playerCondition.Eat(100f); //����� ȸ��
        playerCondition.Heal(100f); //ü�µ� ȸ��
        rb.velocity = Vector3.zero; //�÷��̾��� �ӵ��� 0���� �ʱ�ȭ
        Vector3 oppositeForce = (-playerTransform.forward + Vector3.up).normalized; //�÷��̾��� �ݴ� �������� ���� ��
        rb.AddForce(oppositeForce*100f, ForceMode.Impulse); //�÷��̾ �о
    }
}
