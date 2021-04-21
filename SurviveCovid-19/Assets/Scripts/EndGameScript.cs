using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    public string sceneToLoad;
    public VideoPlayer vp;
    private bool Tset;
    // Start is called before the first frame update
    void Start()
    {
        Tset = false;
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, "CoronaGameEndNew.mp4");
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
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
