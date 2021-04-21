using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPeople : MonoBehaviour
{

    // Start is called before the first frame update
    public bool infected;





    private Rigidbody2D myRigidbody;
    public Transform target;
    public Transform avoid;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public float MoveSpeed;
    private Animator anim;
    private bool moveLeft;
    public GameObject poison;
    private GameHandler gameScript;


    public GameObject handler;


    // Start is called before the first frame update
    void Start()
    {
        gameScript = handler.GetComponent<GameHandler>();

        if (Random.Range(-1f, 1f) < -0.8f)
        {
            infected = true;
            gameScript.num_infect++;
        }
        else
        {
            infected = false;
            
        }

        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        if (infected)
        {
            poison.gameObject.SetActive(true);
        }
        else
        {
            poison.gameObject.SetActive(false);
        }
    }

    void CheckDistance()
    {

        if (Vector3.Distance(avoid.position, transform.position) <= chaseRadius)
        {
            anim.SetBool("move", true);

            Vector3 temp = Vector3.MoveTowards(transform.position, -avoid.position * 2, MoveSpeed * 1.5f * Time.deltaTime);
            changeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);


        }
        else
        {
            if (Vector3.Distance(target.position, transform.position) <= chaseRadius
               )
            {
                anim.SetBool("move", true);

                Vector3 temp = Vector3.MoveTowards(transform.position, transform.position + Vector3.Normalize(-target.position+transform.position),  MoveSpeed * Time.deltaTime);
                Vector3 temp2 = new Vector3(-transform.position.x + target.position.x + 0.1f, -transform.position.y + target.position.y + 0.1f, transform.position.z);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);


            }
            else
            {
                anim.SetBool("move", false);
            }

        }

    }

    private void changeAnim(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                anim.SetFloat("moveX", 1);
                anim.SetFloat("moveY", 0);
            }
            else if (dir.x < 0)
            {

                anim.SetFloat("moveX", -1);
                anim.SetFloat("moveY", 0);
            }
        }
        else if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
        {
            if (dir.y > 0)
            {

                anim.SetFloat("moveY", 1);
                anim.SetFloat("moveX", 0);
            }
            else if (dir.y < 0)
            {

                anim.SetFloat("moveY", -1);
                anim.SetFloat("moveX", 0);
            }
        }

    }

    public void Die()
    {
        //anim.SetBool("die", true);
        StartCoroutine(deathCo());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!infected)
            {
                
                var b = other.gameObject.GetComponent<PlayerMovement>().infected;
                var m = other.gameObject.GetComponent<PlayerMovement>().masked;

                if (b && !m)
                {

                    SoundManager.PlaySound("npc3");
                    gameScript.infected_by_player++;
                    gameScript.num_infect++;
                    infected = true;
                }
            }
            //Debug.Log("hit");
            //Vector2 diff = transform.position - other.transform.position;
            //transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);

        }
        if (other.CompareTag("civ"))
        {
            if (!infected)
            {
                var b = other.gameObject.GetComponent<NormalPeople>().infected;
                if (b)
                {
                    gameScript.num_infect++;
                    infected = true;
                }

            }
            //Debug.Log("hit");
            //Vector2 diff = transform.position - other.transform.position;
            //transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);

        }


    }

    IEnumerator deathCo()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
