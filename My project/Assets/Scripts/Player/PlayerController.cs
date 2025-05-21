using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public bool isground;


    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;   //���콺�� ��Ÿ��
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    public Action inventory;
    public Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   //���콺 Ŀ�� �����
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;    //w,s�Է� / a d�Է�
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; //3�������� �̵���y�� ��鸱 �� �־ ���������� �ֱ�

        _rigidbody.velocity = dir;  //���Ϳ� �� ����ؼ� velocity �ɱ�
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;       //���� �����°Ŷ� x -> y ���� �ݴ�� �־���� ��
        camCurXRot = Mathf.Clamp(camCurXRot,minXLook,maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);  //���콺 ����������/ ���Ʒ��� ���� �ݴ�

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity , 0);    //y�࿡ ���� x
    }


    public void OnMove(InputAction.CallbackContext context) //������¸� �޾ƿ�
    {
        if (context.phase == InputActionPhase.Performed) // Ű�Է½�+���� ������
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled) // Ű ���� ���� 0
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); //�������� ���� �ֱ� ���� ���޽�
        }
    }

    public void OnInv(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f),/*����*/ Vector3.down),  //�յھ翷�̵����Ѽ� ���̸� ��
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f),/*����*/ Vector3.down), 
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f),/*����*/ Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f),/*����*/ Vector3.down)
        };
        

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.7f, groundLayerMask))//ª�� ���̸� ���� ���� �پ����� Ȯ��
            {
                Debug.Log("true");
                return true;
            }

        }
        Debug.Log("false");
        return false;

    }
    
}
