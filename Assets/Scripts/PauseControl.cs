using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public bool isPause;
    public GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {     
        
        //If the player press the p key, then the game will be paused.
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPause = !isPause;
            Time.timeScale = isPause ? 0 : 1;
            pausePanel.SetActive(isPause);
        }
    }
}
