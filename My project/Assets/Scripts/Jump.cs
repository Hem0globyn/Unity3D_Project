using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController controller =other.GetComponent<PlayerController>();
        if (controller != null)
        {
            Debug.Log("jump");
            Vector3 velocity = controller._rigidbody.velocity;
            velocity.y = 0f;
            controller._rigidbody.velocity = velocity;
            controller._rigidbody.AddForce(Vector3.up*200 , ForceMode.Impulse);
        }
    }
}
