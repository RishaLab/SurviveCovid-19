using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{

    public Button left;
    public Button right;
    public Button up;
    public bool sound;
    public Button down;



    public string sceneToLoad;
    public int score;
    public bool infected;
    public bool masked;
    public int health;
    public float speed;
    public PlayerState currentState;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    private GameHandler gameScript;
    public GameObject handler;


    public GameObject poison;
    public GameObject Mask;
    public GameObject Sanitizer;

    public Button Mute;

    public int Maxmasktime;
    public int masktime;

    public int Maxsanitime;
    public int sanitime;

    public int medicine_col;
    public int grocery_col;

    public Image black;
    public Animator anim;




    public bool sanitize;
// attaks

    // Start is called before the first frame update
    void Start()
    {
        sound = true;

        Button bleft = left.GetComponent<Button>();

        Button bright = right.GetComponent<Button>();

        Button bup = up.GetComponent<Button>();

        Button bdown = down.GetComponent<Button>();



        //sounds 






        gameScript = handler.GetComponent<GameHandler>();
        score = 0;
        sanitime = 0;
        masktime = 0;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        
    }

    void TaskOnClick()
    {

        StartCoroutine("Fading");
    }

    IEnumerator Fading()
    {
        Time.timeScale = 1f;

        anim.SetBool("fade", true);
        yield return new WaitUntil(() => black.color.a == 1);

        SceneManager.LoadScene(sceneToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            if (sound)
            {
                SoundManager.PlaySound("poisoned_once");
            }
                //this.gameObject.SetActive(false);
            TaskOnClick();
        }
        change = Vector3.zero;
        change.x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        change.y = CrossPlatformInputManager.GetAxisRaw("Vertical");

        if(currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }
        if (infected)
        {
            poison.gameObject.SetActive(true);
        }
        else
        {
            poison.gameObject.SetActive(false);
        }
        if (masked)
        {
            Mask.gameObject.SetActive(true);
        }
        else
        {
            Mask.gameObject.SetActive(false);
        }
        if (sanitize)
        {
            Sanitizer.gameObject.SetActive(true);
        }
        else
        {
            Sanitizer.gameObject.SetActive(false);
        }
        

    }

    /*public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "poison":
                audioSrc.PlayOneShot(poison);
                break;
            case "poisoned_once":
                audioSrc.PlayOneShot(poisoned_once);
                break;
            case "collect":
                audioSrc.PlayOneShot(collect);
                break;
            case "Power_up":
                audioSrc.PlayOneShot(Power_up);
                break;
        }
    }*/

    

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            myRigidbody.isKinematic = true;
            myRigidbody.velocity = Vector3.zero;
            animator.SetBool("moving", false);
            myRigidbody.isKinematic = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            if (sanitize)
            {
                if (sound)
                {
                    SoundManager.PlaySound("Monster_death");
                }
                    StartCoroutine("KillMonster" ,other);
                
            }
            else
            {
                if (infected == false && masked == false)
                {

                    if (sound)
                    {
                        SoundManager.PlaySound("Monster_death");
                        SoundManager.PlaySound("poisoned_once");
                    }
                        infected = true;
                    animator.SetBool("tookdamage", true);
                    StartCoroutine("Infection");
                }
            }

            

            
        }
        if (other.CompareTag("doctor"))
        {
            
            if (infected == true)
            {
                if (sound)
                {
                    SoundManager.PlaySound("Heal");
                }
                    infected = false;
                if (health + 5 > 20)
                {
                    health = 20;
                }
                else
                {
                    health += 5;
                }
          
            }
        }

        if (other.CompareTag("civ"))
        {
            if (infected == false && !masked)
            {
                var b = other.gameObject.GetComponent<NormalPeople>().infected;
                if (b)
                {
                    if (sound)
                    {
                        SoundManager.PlaySound("poisoned_once");

                    }
                    infected = true;
                    StartCoroutine("Infection");
                }
            }
            //Debug.Log("hit");
            //Vector2 diff = transform.position - other.transform.position;
            //transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);

        }

        if (other.CompareTag("mask"))
        {
            if (masked == false)
            {
                

                
                    other.gameObject.SetActive(false);
                    masked = true;
                if (sound)
                {
                    SoundManager.PlaySound("collect");
                }
                    masktime = Maxmasktime;
                    StartCoroutine("RemoveMask");
                
            }
        }

        if (other.CompareTag("sanitizer"))
        {
            if (sanitize == false)
            {
                


                    other.gameObject.SetActive(false);

                if (sound)
                {
                    SoundManager.PlaySound("collect");
                }
                    sanitize = true;
                    sanitime = Maxsanitime;
                    StartCoroutine("RemoveSanitizer");
                
            }
        }

        if (other.CompareTag("collectable"))
        {

            if (sound)
            {
                SoundManager.PlaySound("collect");
            }
            other.gameObject.SetActive(false);
            score++;
        }

        if (other.CompareTag("grocery_col"))
        {

            if (sound)
            {
                SoundManager.PlaySound("collect");
            }
            other.gameObject.SetActive(false);
            gameScript.grocery_col++;
            grocery_col++;
        }

        if (other.CompareTag("medicine_col"))
        {
            if (sound)
            {

                SoundManager.PlaySound("collect");
            }
            other.gameObject.SetActive(false);
            gameScript.medicine_col++;
            medicine_col++;
        }

        


    }
    IEnumerator RemoveMask()
    {
        for(int i = 0; i < Maxmasktime; i++)
        {
            
            yield return new WaitForSeconds(1f);
            masktime--;
        }
        
        masked = false;


    }

    IEnumerator RemoveSanitizer()
    {
        for (int i = 0; i < Maxsanitime; i++)
        {

            yield return new WaitForSeconds(1f);
            sanitime--;
        }

        sanitize = false;


    }

    IEnumerator KillMonster(Collider2D other)
    {

        other.gameObject.GetComponent<Animator>().SetBool("die", true);
        yield return new WaitForSeconds(0.3f);
        other.gameObject.SetActive(false);
    }

    IEnumerator Infection()
    {
        for(int i = 0; i < 30; i++)
        {
            if(infected == false) {
                yield break;
            }
            if (health - 1 < 0)
            {
                health = 0;
            }
            else
            {

                health--;
            }
            yield return new WaitForSeconds(1.5f);
        }
        infected = false;

        animator.SetBool("tookdamage", false);

    }

    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
            );
    }
}
