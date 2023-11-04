using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.CoreModule;

public class PlayerStatusHandle : MonoBehaviour
{
    public GameObject unitychan;
    public float maxHP = 100;
    public float currentHP;
    public int score;
    private float curtime, totaltime, lasttime;
    public Canvas StatusCanvas;
    public Image lifeImage;
    public TMPro.TextMeshProUGUI timeText, scoreText;
    // Start is called before the first frame update

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentHP = maxHP;
        score = 0;
        curtime = lasttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        curtime = Time.time;
        totaltime += curtime - lasttime;
        lasttime = curtime;
        timeText.text = totaltime.ToString("f2");
        scoreText.text = score.ToString();
        lifeImage.fillAmount = currentHP/maxHP;
        if(currentHP<=0){
            Lose();
        }
    }

    public void Win(){
        CloseStatusCanvas();
        SceneManager.LoadScene(4);
        currentHP = 100;
    }

    public void Lose(){
        CloseStatusCanvas();
        SceneManager.LoadScene(5);
        currentHP = 100;
    }

    public void OpenStatusCanvas(){
        StatusCanvas.enabled = true;
        unitychan.SetActive(true);
        currentHP = maxHP;
        score = 0;
        totaltime = 0;
    }
    public void CloseStatusCanvas(){
        StatusCanvas.enabled = false;
        unitychan.SetActive(false);
    }
}
