using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public PlayerStatusHandle playerStatusHandle;
    void Start(){
        playerStatusHandle.CloseStatusCanvas();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        playerStatusHandle.OpenStatusCanvas();
    }
}
