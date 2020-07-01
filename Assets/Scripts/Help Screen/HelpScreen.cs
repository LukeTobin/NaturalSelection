using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HelpScreen : MonoBehaviour
{

    public Button OpenClose;
    public Button HelpClose;
    private PlayerController player;
    public GameTimeManager timeManager;
    int counter;
    
    public bool HelpOpen;
    public GameObject HelpOverlay;
    public bool pause;


    // Start is called before the first frame update
    void Start()
    {
        OpenClose.onClick.AddListener(showHide);
        HelpClose.onClick.AddListener(showHide);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        timeManager = GameObject.FindGameObjectWithTag("time").GetComponent<GameTimeManager>();
        pause = false;  
    }

    // Update is called once per frame
    void Update()
    {   
        // Show/hide HelpScreen on F1 press
        if (Input.GetKeyDown(KeyCode.F1) && player.stamina >= 1)
        {
            showHide();
        }

        // Close HelpScreen when it's open and escape is pressed
        if (HelpOpen == true){
            if (Input.GetKeyDown(KeyCode.Escape)){
                showHide();
            }
        }
    }

    void showHide()
    {
        counter++;
        if (counter % 2 == 1 && player.stamina >= 1) 
        {
            HelpOverlay.gameObject.SetActive(true);
            HelpOpen = true;
            player.pause = true;
            timeManager.clockPaused = true;
            //player.moveSpeed = 0f;
        }
        else
        {
            HelpOverlay.gameObject.SetActive(false);
            HelpOpen = false;
            player.pause = false;
            timeManager.clockPaused = false;
            //player.moveSpeed = 4.5f;
        }
    }
}
