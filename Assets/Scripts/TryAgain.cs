using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerStatusHandle playerStatusHandle;
    private SceneControl sceneControl;
    void Start(){
        playerStatusHandle = GameObject.Find("GameStatus").GetComponent<PlayerStatusHandle>();
        sceneControl = GameObject.FindGameObjectWithTag("Player").GetComponent<SceneControl>();
        //playerStatusHandle.CloseStatusCanvas();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        GameObject.Find("Camera").GetComponent<Skybox>().material = sceneControl.ChenSkybox;
        playerStatusHandle.OpenStatusCanvas();  
    }
}
