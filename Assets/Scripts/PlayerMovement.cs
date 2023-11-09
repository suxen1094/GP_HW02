using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed = 5.0f;
    public Transform cameraTransform; 
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (vertical < 0)
        {
            vertical = 0;
        }

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDirection = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
            moveDirection.y = 0;
            moveDirection.Normalize();

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, 1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            characterController.Move(moveDirection * speed * Time.deltaTime);
        }
    }
}
