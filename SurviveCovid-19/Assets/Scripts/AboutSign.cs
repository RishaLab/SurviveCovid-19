﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutSign : MonoBehaviour
{
    public GameObject dialogbox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(playerInRange)
        {
            
                dialogbox.SetActive(true);
                dialogText.text = dialog;
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogbox.SetActive(false);
        }
    }

}
