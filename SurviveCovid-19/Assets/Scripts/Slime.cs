using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    private Rigidbody2D myRigidbody;
    public Transform target;
    public Transform avoid;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public float MoveSpeed;
    private Animator anim;
    private bool moveLeft;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        
        if (Vector3.Distance(avoid.position, transform.position) <= chaseRadius)
        {
            anim.SetBool("move", true);

            Vector3 temp = Vector3.MoveTowards(transform.position, -avoid.position*2, MoveSpeed *1.5f * Time.deltaTime);
            changeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);


        }
        else
        {
            if (Vector3.Distance(target.position, transform.position) <= chaseRadius
               && Vector3.Distance(target.position, transform.position) > attackRadius)
            {
                anim.SetBool("move", true);

                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
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
        if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if(dir.x > 0)
            {
                anim.SetFloat("moveX", 1);
                anim.SetFloat("moveY", 0);
            }
            else if(dir.x < 0)
            {

                anim.SetFloat("moveX", -1);
                anim.SetFloat("moveY", 0);
            }
        }else if(Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
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
        anim.SetBool("die", true);
        StartCoroutine(deathCo());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("weapon"))
        {
            Debug.Log("hit");
            Vector2 diff = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + diff.x, transform.position.y + diff.y);
        }

  
    }

    IEnumerator deathCo()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
