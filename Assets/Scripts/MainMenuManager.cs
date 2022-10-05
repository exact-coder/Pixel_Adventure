using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonPressed()
    {
        DoTransition();

        //wait for few second...
        Invoke("LoadScene",1.5f);
        

    }

    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButtonPressed()
    {
        Application.Quit();
    }

    void DoTransition()
    {
        animator.SetTrigger("DoTransition");

    }
}
