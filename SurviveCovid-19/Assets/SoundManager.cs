using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour

    
{

    public static AudioClip collect, poisoned, poisoned_once, Power_up, npc3, Heal, Lose, Monster_death;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        

        collect = Resources.Load<AudioClip>("collect");
        poisoned = Resources.Load<AudioClip>("poisoned");
        poisoned_once = Resources.Load<AudioClip>("poisoned_once");
        Power_up = Resources.Load<AudioClip>("Power_up");
        npc3 = Resources.Load<AudioClip>("npc3");
        Heal = Resources.Load<AudioClip>("Heal");
        Lose = Resources.Load<AudioClip>("Lose");
        Monster_death = Resources.Load<AudioClip>("Monster_death");


        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "poisoned":
                audioSrc.PlayOneShot(poisoned);
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
            case "npc3":
                audioSrc.PlayOneShot(npc3);
                break;
            case "Heal":
                audioSrc.PlayOneShot(Heal);
                break;
            case "Lose":
                audioSrc.PlayOneShot(Lose);
                break;
            case "Monster_death":
                audioSrc.PlayOneShot(Monster_death);
                break;
        }
    }
}
