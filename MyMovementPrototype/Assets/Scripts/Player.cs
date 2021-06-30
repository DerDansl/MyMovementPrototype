using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private Transform groundCheckTransform;
    [SerializeField]private LayerMask playerMask;
    private bool jumpKeyPressed;
    private float horizonzalInput;
    private Rigidbody rigidbodyComponent;
    private int runMultiplier = 2;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            print("jump key was pressed");
            jumpKeyPressed = true;
        }
        if (Input.GetButtonDown("Run"))
        {
            print("run key was pressed");
            runMultiplier = 5;

        }
        if (Input.GetButtonUp("Run"))
        {            
            print("run key was released");
            runMultiplier = 2;
        }

        horizonzalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once every physic update
    void FixedUpdate()
    {
        // only jump if colliding with ground to prevent an unwanted double jump
        var isNotGrounded = Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0;

        rigidbodyComponent.velocity = new Vector3(horizonzalInput*runMultiplier, rigidbodyComponent.velocity.y, 0);


        if (!isNotGrounded && jumpKeyPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }
    }
}
