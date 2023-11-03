using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{
    public GameObject explosion;
    private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collisionObject)
    {
        if (collisionObject.gameObject.tag == "Monster"){
            prefab = Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(prefab, 1);
        }
    }
}
