using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private GameObject player;
    public TMPro.TextMeshProUGUI attentionText;
    private PlayerStatusHandle playerStatusHandle;
    //public GameObject startPoint;
    // Start is called before the first frame update
    void Start()
    {
        playerStatusHandle = GameObject.Find("GameStatus").GetComponent<PlayerStatusHandle>();
    }

    void OnCollisionEnter(Collision collisionObject)
    {
        if (collisionObject.gameObject.tag == "EndPoint")
        {
            // No enemy left
            if (!IsEnemySurvive())
            {
                int current_scene_idx = SceneManager.GetActiveScene().buildIndex;

                // If current scene isn't the final stage -> Load the next stage
                if (current_scene_idx <= 2)
                {
                    SceneManager.LoadScene(current_scene_idx + 1);
                }
                // Current scene is the final stage -> Win!!
                else
                {
                    playerStatusHandle.Win();
                }
            }
            // At least one enemy exists -> Show attention text for 5 seconds
            else
            {
                attentionText.text = "You need to defeat all enemies!!";
                StartCoroutine(WaitForTheTextToDisable());
            }
        }
    }

    IEnumerator WaitForTheTextToDisable()
    {
        yield return new WaitForSeconds(3);
        attentionText.text = "";
    }

    bool IsEnemySurvive()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
        if (enemies.Length == 0) return false;
        else return true;
    }
}
