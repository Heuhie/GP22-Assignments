using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    AudioSource theWizard;

    // Start is called before the first frame update
    void Start()
    {
        theWizard = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
        theWizard.Play();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Wizard");
    }

    public void HoverButton()
    {
        animator.Play("HoveringButton");        
    }

    public void ExitHoverButton()
    {
        animator.Play("Stop");
    }


}
