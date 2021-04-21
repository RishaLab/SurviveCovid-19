using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public GameObject player;
    public Image healthbar;
    public Image shieldbar;
    public Image sanibar;
    private PlayerMovement playerScript;

    public int num_infect = 0;
    public int infected_by_player = 0;
    private int init_infected;
    //private NormalPeople poepleScript;

    public Text placeText;
    public Text infectedPeople;
    public Text InfectedDirectly;

    public Text infTimer;
    public Text maskTimer;
    public Text washTimer;




    public Text Gro;
    public Text Med;

    public int Max_med;
    public int Max_gro;

    public int medicine_col;
    public int grocery_col;

    // Start is called before the first frame update
    void Start()
    {
        Max_med = Random.Range(2, 5);
        Max_gro = Random.Range(5, 10);

        init_infected = num_infect;
        playerScript = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        placeText.text = "Score: " + playerScript.score.ToString();
        infectedPeople.text = "Total Infected: "+ num_infect.ToString();
        InfectedDirectly.text = "Infected Directly: "+ infected_by_player.ToString();

        Med.text = "Medicine: " + medicine_col.ToString() + " / " + Max_med.ToString();
        Gro.text = "Groceries: " + grocery_col.ToString() + " / " + Max_gro.ToString();

        infTimer.text = "InfectionTimer: " + playerScript.health.ToString();

        maskTimer.text = "MaskTimer: " + playerScript.masktime.ToString();

        washTimer.text = "HandWashTimer: " + playerScript.sanitime.ToString();

        float temp = playerScript.health / 20f;
        healthbar.rectTransform.localScale = new Vector3(temp, 1, 1);
        float temp2 = playerScript.masktime / (float) playerScript.Maxmasktime;
        shieldbar.rectTransform.localScale = new Vector3(temp2, 1, 1);
        float temp3 = playerScript.sanitime / (float)playerScript.Maxsanitime;
        sanibar.rectTransform.localScale = new Vector3(temp3, 1, 1);
    }
}
