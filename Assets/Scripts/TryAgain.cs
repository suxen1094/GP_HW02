using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerStatusHandle playerStatusHandle;
    void Start(){
        playerStatusHandle = GameObject.Find("GameStatus").GetComponent<PlayerStatusHandle>();
        //playerStatusHandle.CloseStatusCanvas();
    }
    public void PlayGame()
    {
       //SceneManager.LoadSceneAsync("Menu");
       SceneManager.LoadScene(0);
        playerStatusHandle.OpenStatusCanvas();  
    }
}
