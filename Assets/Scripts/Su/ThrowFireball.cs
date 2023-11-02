using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFireball : MonoBehaviour
{
    Vector3 direction;
    public GameObject hand;
    public GameObject Fireball;
    GameObject player;
    GameObject prefab;
    Animator animator;
    private int idle_state;
    private int locomotion_state;
    private int jump_state;
    private int attack_state;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        animator = player.GetComponent<Animator>();

        idle_state = Animator.StringToHash("Base Layer.WAIT00");
        locomotion_state = Animator.StringToHash("Base Layer.Locomotion");
        jump_state = Animator.StringToHash("Base Layer.JUMP00");
        attack_state = Animator.StringToHash("Base Layer.ATK00");
    }

    void FixedUpdate()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        // If current state is locomotion or idle
        if (state.fullPathHash == locomotion_state || state.fullPathHash == idle_state)
        {
            // If clicking mouse left
            if (Input.GetMouseButton(0) && !animator.IsInTransition(0))
            {
                animator.SetBool("Attack", true);
            }
        }
    }
}
