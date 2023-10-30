using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HPHandler : MonoBehaviour
{
    public int Max_HP = 100;
    private int current_HP;
    // Start is called before the first frame update
    void Start()
    {
        current_HP = Max_HP;
    }

    void OnCollisionEnter(Collision collisionObject)
    {
        // Handle collision: 1. Healing item 2. Monsters
        // Monster collision又分成兩種: 1. 被怪物攻擊 2. 自己主動攻擊怪物
        // 可以使用Animator的Attack bool來做判定
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
