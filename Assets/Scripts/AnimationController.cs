using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
     void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Attack");
        }
    }
}
