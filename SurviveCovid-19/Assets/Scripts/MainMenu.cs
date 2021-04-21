using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Image black;
    public Animator anim;
    public GameObject backblack;

    public Button yourButton;
    public Button ExitButton;

    public Button yourAbout;
    public string sceneToLoad;
    public GameObject about;
    private bool isabout;
    // Start is called before the first frame update

    void Start()
    {
        
        Button ebtn = ExitButton.GetComponent<Button>();
        StartCoroutine("killBackground");
        Button btnAbout = yourAbout.GetComponent<Button>();
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        btnAbout.onClick.AddListener(TaskOnAbout);
        ebtn.onClick.AddListener(TaskOnExit);
    }

    void TaskOnAbout()
    {
        if (!isabout)
        {
            isabout = true;
            about.SetActive(true);
        }
        else
        {
            isabout = false;
            about.SetActive(false);
        }
    }
    void TaskOnExit()
    {
        Application.Quit();
    }


    void TaskOnClick()
    {
        StartCoroutine("Fading");
    }

    IEnumerator killBackground()
    {
        yield return new WaitUntil(() => black.color.a == 1);
        backblack.SetActive(false);
    }


    IEnumerator Fading()
    {

        anim.SetBool("fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(sceneToLoad);
    }
}
