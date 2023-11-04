using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    Vector3 direction;
    public GameObject hand;
    public GameObject bomb;
    GameObject player;
    GameObject prefab;
    Animator animator;
    private int idle_state, locomotion_state, back_state;
    public float FireballSpeed = 1000;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        animator = player.GetComponent<Animator>();

        idle_state = Animator.StringToHash("Base Layer.WAIT00");
        locomotion_state = Animator.StringToHash("Base Layer.Locomotion");
        back_state = Animator.StringToHash("Base Layer.WALK00_B");
    }

    void FixedUpdate()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        // If current state is locomotion or idle
        if (state.fullPathHash == locomotion_state || state.fullPathHash == idle_state || state.fullPathHash == back_state)
        {
            // If clicking mouse left
            if (Input.GetKey(KeyCode.J) && !animator.IsInTransition(0))
            {
                prefab = Instantiate(bomb, hand.transform.position, Quaternion.identity);
                direction = hand.transform.position - player.transform.position;
                prefab.GetComponent<Rigidbody>().AddForce(new Vector3(
                    direction.x, 
                    0, 
                    direction.z) * FireballSpeed);
                prefab.GetComponent<Rigidbody>().AddTorque(new Vector3(1, 0, 0) * 5);
            }
        }
    }
}
