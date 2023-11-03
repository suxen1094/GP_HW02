using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerStatusHandle : MonoBehaviour
{
    // Handle HP
    private int maxHP = 100;
    private int currentHP;
    private int score;
    private int time;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        score = 0;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
