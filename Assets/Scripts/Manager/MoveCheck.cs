using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCheck : MonoBehaviour
{

    public CraftingController CraftMenu;
    public HelpScreen helpScreen;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        helpScreen = GameObject.FindGameObjectWithTag("UI").GetComponent<HelpScreen>();
        CraftMenu = GameObject.FindGameObjectWithTag("UI").GetComponent<CraftingController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CraftMenu.CraftOpen == true || helpScreen.HelpOpen == true){
            player.moveSpeed = 0f;
        }
        else {
            player.moveSpeed = 4.5f;
        }
    }
}
