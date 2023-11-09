using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.PlayerLoop;
public class PlayerControl : MonoBehaviour
{
    private GameObject player;
    public float forward_speed = 5.0f;
    public float backward_speed = 2.0f;
    public float rotate_speed = 2.0f;
    public float jump_power = 7.0f;
    public float use_curve_height = 0.3f;

    private Animator animator;
    private Rigidbody rb;
    private CapsuleCollider col;

    private int idle_state;
    private int locomotion_state;
    private int jump_state;
    private int attack_state;
    private int back_state;
    private bool attacking;

    // For the usage of collidar adjustment
    private float origColliderHeight;
    private Vector3 origColliderCenter;

    void Start()
    {
        player = gameObject;
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();

        // Get state hash
        idle_state = Animator.StringToHash("Base Layer.WAIT00");
        locomotion_state = Animator.StringToHash("Base Layer.Locomotion");
        jump_state = Animator.StringToHash("Base Layer.JUMP00");
        attack_state = Animator.StringToHash("Base Layer.ATK00");
        back_state = Animator.StringToHash("Base Layer.WALK00_B");

        // Get collider data
        col = gameObject.GetComponent<CapsuleCollider>();
        origColliderHeight = col.height;
        origColliderCenter = col.center;
    }

    void FixedUpdate()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        // If the player is moving
        if (state.fullPathHash == locomotion_state)
        {
            // If press jump and not in the transition state
            if (Input.GetKey(KeyCode.Space) && !animator.IsInTransition(0))
            {
                jumpControl();
            }
            if (Input.GetKey(KeyCode.J) && !animator.IsInTransition(0))
            {
                animator.SetBool("Attack", true);
            }
        }
        // Jump state
        else if (state.fullPathHash == jump_state)
        {
            // Disable jump
            animator.SetBool("Jump", false);
        }
        // Attack state
        else if (state.fullPathHash == attack_state)
        {
            animator.SetBool("Attack", false);
        }
        // Idle state
        else if (state.fullPathHash == idle_state)
        {
            if (Input.GetKey(KeyCode.J) && !animator.IsInTransition(0))
            {
                animator.SetBool("Attack", true);
            }
        }
        // Walk back state
        else if (state.fullPathHash == back_state)
        {
            if (Input.GetKey(KeyCode.J) && !animator.IsInTransition(0))
            {
                animator.SetBool("Attack", true);
            }
        }

        moveControl();
    }

    void moveControl()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        
        if (!attacking)
        {
            animator.SetFloat("Speed", v);
            animator.SetFloat("Direction", h);

            // Transform
            gameObject.transform.Rotate(0, h * rotate_speed, 0);
            // Change degree to radius
            float radius = gameObject.transform.eulerAngles.y / 180 * (float)Math.PI;

            if (v < 0)
            {
                Vector3 vel = backward_speed * v * new Vector3((float)Math.Sin(radius), 0, (float)Math.Cos(radius)).normalized;
                // rb.velocity = vel;
                rb.AddForce(vel * 100);
            }
            else if (v >= 0)
            {
                Vector3 vel = forward_speed * v * new Vector3((float)Math.Sin(radius), 0, (float)Math.Cos(radius)).normalized;
                // rb.velocity = vel;
                rb.AddForce(vel * 100);
            }
        }
    }

    void jumpControl()
    {
        animator.SetBool("Jump", true);
        rb.AddForce(Vector3.up * jump_power, ForceMode.VelocityChange);
        this.adjustCollider();
    }

    public void attackStart()
    {
        attacking = true;
    }

    public void attackFinish()
    {
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void adjustCollider()
    {
        Ray ray = new Ray(gameObject.transform.position, -Vector3.up);
        RaycastHit hitInfo = new RaycastHit();

        if (Physics.Raycast(ray, out hitInfo))
        {
            // 如果collider和跳躍的動畫之間的高度差距太多，則需要校正
            if (hitInfo.distance > use_curve_height)
            {
                float jumpHeight = animator.GetFloat("JumpHeight");
                col.height = origColliderHeight - jumpHeight;
                float adjustCenterY = origColliderCenter.y + jumpHeight;
                col.center = new Vector3(0, adjustCenterY, 0);
            }
            // 不用校正
            else
            {
                col.height = origColliderHeight;
                col.center = origColliderCenter;
            }
        }
    }
}