using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroVideoPlayer : MonoBehaviour
{
    public string sceneToLoad;
    public VideoPlayer vp;
    private bool Tset;
    private bool ultimate;

    public Button continueButton;
    
    // Start is called before the first frame update
    void Start()
    {
        Button ctn = continueButton.GetComponent<Button>();
        ctn.onClick.AddListener(Continue_Game);
        ultimate = false;
        Tset = false;
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Ultimate.mp4");
    }

    void Continue_Game()
    {

        ultimate = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (vp.isPlaying)
        {
            Tset = true;
            
        }
        if(Tset && !vp.isPlaying)
        {
            ultimate = true;
        }
        if (ultimate)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        
    }
}
