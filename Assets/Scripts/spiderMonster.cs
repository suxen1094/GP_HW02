using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class spiderMonster : MonoBehaviour
{
    public AudioClip injuriedAudioClip;
    public AudioSource audioPlayer;
    public GameObject injuriedParticleSystem;
    private GameObject player;
    public float rushVelocity = 500.0f;
    public float stopAttackingSeconds = 5.0f;
    public float waitForRushSeconds = 1.5f;
    public float emitParticleSeconds = 1.0f;
    public float rangeOfSensor = 500.0f;
    public float rangeOfAttack = 20.0f;
    public float maxHp = 10.0f;
    public float destroyingSeconds = 0.0f;
    public string playerTag = "Player";

    private NavMeshAgent naviAgent;
    private Animator animator;
    private Vector3 originSpiderPos;
    private int idleState;
    private int attackState;
    private int runningState;
    private float hp;
    private bool isAttacking = false;
    public bool isStopAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("unitychan");
        StartCoroutine(emitParticleForSeconds());
        naviAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        originSpiderPos = this.GetComponent<Transform>().position;

        idleState = Animator.StringToHash("Base Layer.Idle");
        attackState = Animator.StringToHash("Base Layer.Attack");
        runningState = Animator.StringToHash("Base Layer.Run");
        hp = maxHp;
    }

    void FixedUpdate()
    {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        Vector3 spiderPos = this.GetComponent<Transform>().position;
        Quaternion spiderRotation = this.GetComponent<Transform>().rotation;
        Vector3 spiderVelocity = naviAgent.velocity;
        Vector3 playerPos = player.GetComponent<Transform>().position;
        RaycastHit hit;

        naviAgent.SetDestination(spiderPos);
        animator.SetFloat("Speed", 0.0f);
        Physics.Raycast(spiderPos, playerPos - spiderPos, out hit);
        if(Vector3.Distance(spiderPos, playerPos) <= rangeOfSensor && hit.collider.tag == playerTag)
        {
            if(Vector3.Distance(spiderPos, playerPos) <= rangeOfAttack && !isStopAttacking)
            {
                animator.SetBool("Attack", true);
                isAttacking = true;
                StartCoroutine(waitForRushForSeconds());
                StartCoroutine(stopAttackingForSeconds());
            }
            else if(!isStopAttacking)
            {
                naviAgent.SetDestination(playerPos);
                animator.SetFloat("Speed", spiderVelocity.magnitude);
            }
        }

        if(state.fullPathHash == attackState)
        {
            animator.SetBool("Attack", false);
            isAttacking = false;
        }
        
    }

    IEnumerator waitForRushForSeconds()
    {
        Vector3 spiderPos = this.GetComponent<Transform>().position;
        Vector3 playerPos = player.GetComponent<Transform>().position;
        yield return new WaitForSeconds(waitForRushSeconds);
        this.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(playerPos - spiderPos) * rushVelocity, ForceMode.VelocityChange);
    }

    IEnumerator stopAttackingForSeconds()
    {
        isStopAttacking = true;
        yield return new WaitForSeconds(stopAttackingSeconds);
        isStopAttacking = false;
    }

    IEnumerator emitParticleForSeconds()
    {
        Vector3 particlePos = this.GetComponent<Transform>().position;
        particlePos.y += 5.0f;
        GameObject test = GameObject.Instantiate(injuriedParticleSystem, particlePos, Quaternion.Euler (270, 0, 0));
        yield return new WaitForSeconds(emitParticleSeconds);
        Destroy(test, 0.0f);
    }

    void beAttacked(float damage)
    {
        hp -= damage;
        audioPlayer.PlayOneShot(injuriedAudioClip);
        StartCoroutine(emitParticleForSeconds());
        if(hp <= 0)
        {
            //死亡聲音與粒子效果
            Destroy(this, destroyingSeconds);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == playerTag && isAttacking)
        {
            //對player造成傷害
            isAttacking = false;
        }
    }
}
