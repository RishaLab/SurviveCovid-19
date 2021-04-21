using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Image black;
    public Animator anim;
    public string sceneToLoad;

    private GameHandler gameScript;
    public GameObject handler;

    void Start()
    {
        gameScript = handler.GetComponent<GameHandler>();

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {

            if (gameScript.Max_med <= gameScript.medicine_col && gameScript.Max_gro <= gameScript.grocery_col)
            {
                StartCoroutine("Fading");
            }

        }
    }

    IEnumerator Fading()
    {
        anim.SetBool("fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(sceneToLoad);
    }
}
