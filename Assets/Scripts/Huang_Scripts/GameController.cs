using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject Player;
    public List<GameObject> Enemies = new List<GameObject>();
    private List<UnityEngine.AI.NavMeshAgent> enemiesNaviAgent = new List<UnityEngine.AI.NavMeshAgent>();
    void Start()
    {
        Player = GameObject.Find("Player");
        foreach (GameObject enemy in Enemies)
        {
            UnityEngine.AI.NavMeshAgent navi = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navi != null)
            {
                enemiesNaviAgent.Add(navi);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (UnityEngine.AI.NavMeshAgent navi in enemiesNaviAgent)
        {
            if (Player != null)
            {
                navi.SetDestination(Player.transform.position);
            }
        }
    }
}
