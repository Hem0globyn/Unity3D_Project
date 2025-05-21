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
    private float camCurXRot;   //마우스의 델타값
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
        Cursor.lockState = CursorLockMode.Locked;   //마우스 커서 숨기기
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
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;    //w,s입력 / a d입력
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; //3차원에서 이동시y가 흔들릴 수 있어서 고정용으로 넣기

        _rigidbody.velocity = dir;  //벡터에 다 계산해서 velocity 걸기
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;       //축을 돌리는거라 x -> y 값을 반대로 넣어줘야 댐
        camCurXRot = Mathf.Clamp(camCurXRot,minXLook,maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);  //마우스 반전때문에/ 위아래가 원래 반대

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity , 0);    //y축에 넣은 x
    }


    public void OnMove(InputAction.CallbackContext context) //현재상태를 받아옴
    {
        if (context.phase == InputActionPhase.Performed) // 키입력시+지속 벡터줌
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled) // 키 떼면 벡터 0
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
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse); //순간적인 힘을 주기 위해 임펄스
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
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f),/*방향*/ Vector3.down),  //앞뒤양옆이동시켜서 레이를 쏨
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f),/*방향*/ Vector3.down), 
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f),/*방향*/ Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f),/*방향*/ Vector3.down)
        };
        

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.7f, groundLayerMask))//짧은 레이를 쏴서 땅에 붙었는지 확인
            {
                Debug.Log("true");
                return true;
            }

        }
        Debug.Log("false");
        return false;

    }
    
}
