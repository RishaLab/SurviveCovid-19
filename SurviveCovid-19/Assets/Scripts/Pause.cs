using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameIsPaused = true;

    public Button yourButton;

    public GameObject MuteOn;
    public GameObject MuteOff;

    public Button ToggleSoundOn;


    public Button continueButton;

    public Button pauseButton;

    public Button ToggleSound;
    public GameObject SoundSource;

    public GameObject left;
    public GameObject right;
    public GameObject up;
    public GameObject down;


    public string sceneToLoad;

    public GameObject pauseMenuUI;

    public Image black;
    public Animator anim;

    void Start()
    {
        left.gameObject.SetActive(false);

        right.gameObject.SetActive(false);

        up.gameObject.SetActive(false);

        down.gameObject.SetActive(false);

        Button ctn = continueButton.GetComponent<Button>();

        Button btn = yourButton.GetComponent<Button>();
        Button ptn = pauseButton.GetComponent<Button>();
        Button soundbtn = ToggleSound.GetComponent<Button>();
        Button soundonbtn = ToggleSoundOn.GetComponent<Button>();



        ptn.onClick.AddListener(Pause_Game);
        soundbtn.onClick.AddListener(Toggle_Sound_Off);
        soundonbtn.onClick.AddListener(Toggle_Sound_On);
        ctn.onClick.AddListener(Continue_Game);
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        StartCoroutine("Fading");
    }

    void Toggle_Sound_Off()
    {
        SoundSource.gameObject.SetActive(false);
        MuteOff.gameObject.SetActive(false);
        MuteOn.gameObject.SetActive(true);
    }

    void Toggle_Sound_On()
    {
        SoundSource.gameObject.SetActive(true);
        MuteOff.gameObject.SetActive(true);
        MuteOn.gameObject.SetActive(false);
    }

    
    IEnumerator Fading()
    {
        Time.timeScale = 1f;

        anim.SetBool("fade", true);
        yield return new WaitUntil(() => black.color.a == 1);

        SceneManager.LoadScene(sceneToLoad);
    }

    void Continue_Game()
    {

            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        
    }

    void Pause_Game()
    {

        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Paused();
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void Resume()
    {
        left.gameObject.SetActive(true);

        right.gameObject.SetActive(true);

        up.gameObject.SetActive(true);

        down.gameObject.SetActive(true);

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Paused()
    {
        left.gameObject.SetActive(false);

        right.gameObject.SetActive(false);

        up.gameObject.SetActive(false);

        down.gameObject.SetActive(false);

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
