using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D player;
    private Vector2 playerInput;


    // Gathering Vars
    public Transform gatherPos;
    public float gatherRange;
    public LayerMask canGather;
    public LayerMask canGather2;
    public int damage;
    public Vector2 direction;
    public float MAX_POSX;
    public float MIN_POSX;
    public float MAX_POSY;
    public float MIN_POSY;
    public Animator animator;


    // STATS
    public float moveSpeed = 4.5f; //needs to be changed in crafting controller also when updated

    // Sliders
    public Slider StaminaBar;
    public Slider HungerBar;
    public Slider ThirstBar;
    public Slider TempBar;

    // Values
    public int hp;
    public float thirst;
    public float hunger;

    public float stamina;
    public float bodyTemp;

    // Regen / Degen
    public float staminaGain;
    public float staminaUsage;
    public float staminaRegen;
    public float staminaDegen;
    public float hungerDegen;
    public float thirstDegen;
    public float tempDegen;
    public float tempRegen;

    // heatSource
    public bool heatSource;

    // GameTimeManager Reference
    private GameTimeManager timeStats;
    private Inventory invent;
    private Items items;


    // Help screen pause
    public bool pause;

    //Scoresystem Reference
    private ScoreSystem scores;

    //Raycast
    public float rayReach;
    public LayerMask layermask;

    public bool addBag;

    public Vector2 newDirection;

    void Start()
    {   
        // HP
        player = GetComponent<Rigidbody2D>();
        hp = Random.Range(10, 100);
        Debug.Log("HP: " + hp);

        // Stamina
        stamina = Random.Range(30f, 100f);
        Debug.Log("Stamina " + stamina);

        StaminaBar.value = stamina;

        //When the player dies
        if (stamina <= 0)
        {
            scores.OnDeathScore();
        }

        // Hunger
        hunger = Random.Range(30, 100);
        Debug.Log("Hunger: " + hunger);
        HungerBar.value = hunger;

        // Thirst
        if (hunger < 55)
        {
            thirst = Random.Range(55, 100);
            Debug.Log("Thirst: " + thirst);
            ThirstBar.value = thirst;
        }
        else
        {
            thirst = Random.Range(30, 55);
            Debug.Log("Thirst: " + thirst);
            ThirstBar.value = thirst;
        }

        // BodyTemp

        heatSource = false; // Can set this boolean to true on collision with campfire or w/e

        bodyTemp = Random.Range(30, 100);
        Debug.Log("bodyTemp: " + bodyTemp);
        TempBar.value = bodyTemp;

        //GameTimeManager + Items Get
        timeStats = GameObject.FindGameObjectWithTag("time").GetComponent<GameTimeManager>();

        //Inventory
        invent = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        //Scoresystem
        scores = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreSystem>();

        //ExtraBag
        addBag = false;
    }

    void Update()
    {
        if (stamina < 1)
        {
            stamina = 0;
            StaminaBar.value = 0;
            timeStats.StopGameClock();
            scores.OnDeathScore();
        }

        // player movement
        if (stamina >= 1)
        {
            playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            player.velocity = playerInput.normalized * moveSpeed;

            MAX_POSX = player.position.x + 1f;
            MIN_POSX = player.position.x - 1f;
            MAX_POSY = player.position.y + 1f;
            MIN_POSY = player.position.y - 1f;
        }
        else
        {
            moveSpeed = 0;
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        float posX = mousePos.x;
        float posY = mousePos.y;

        if (mousePos.x > MAX_POSX)
        {
            posX = MAX_POSX;
        }

        if (mousePos.x < MIN_POSX)
        {
            posX = MIN_POSX;
        }

        if (mousePos.y > MAX_POSY)
        {
            posY = MAX_POSY;
        }

        if (mousePos.y < MIN_POSY)
        {
            posY = MIN_POSY;
        }

        direction = new Vector2(posX, posY);
        gatherPos.transform.position = direction;

        Vector2 movement;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Player Stamina

        if (player.velocity.magnitude == 0 && stamina < 100 && pause == false && stamina >= 1){
            // Player is not moving
            stamina += staminaRegen * Time.deltaTime;
        }
            StaminaBar.value = stamina;


        // Hunger degen
        if (hunger < 100 && hunger > 0 && pause == false && stamina >= 1){
        hunger -= hungerDegen * Time.deltaTime;
        }
        HungerBar.value = hunger;

        // Thirst degen
        if (thirst < 100 && thirst > 0 && pause == false && stamina >= 1)
        {
            thirst -= thirstDegen * Time.deltaTime;
        }
        ThirstBar.value = thirst;

        // BodyTemp
        
        // No heatSource nearby
        if (heatSource == false && bodyTemp > 0 && pause == false && stamina >= 1)
        {
            //bodyTemp -= tempDegen * Time.deltaTime;
            //TempBar.value = bodyTemp;

            //Checks to see what time of day it is, then increments the bodytemperature accordingly
            if (timeStats.isMorning)
            {
                bodyTemp += timeStats.morningTemp * Time.deltaTime;
                TempBar.value = bodyTemp;
            }
            else if (timeStats.isNoon)
            {
                bodyTemp += timeStats.noonTemp * Time.deltaTime;
                TempBar.value = bodyTemp;
            }
            else if (timeStats.isEvening)
            {
                bodyTemp -= timeStats.eveningTemp * Time.deltaTime;
                TempBar.value = bodyTemp;
            }
            else if (timeStats.isNight)
            {
                bodyTemp -= timeStats.nightTemp * Time.deltaTime;
                TempBar.value = bodyTemp;
            }

            //Looks whenever a fireplace is used, then adds up some heat
            //if (items.campfireUsed)
            //{
            //    bodyTemp += 40;
            //    TempBar.value = bodyTemp;
            //    items.campfireUsed = false;
            //}

        }
        // heatSource nearby
        if (heatSource == true && bodyTemp < 100 ){
            bodyTemp += tempRegen * Time.deltaTime;
            TempBar.value = bodyTemp;
        }


        // If hunger bar = 0 start to drain stamina
        if (hunger == 0){
            stamina -= staminaDegen * Time.deltaTime;
        }
        // If thirst bar = 0 start to drain stamina
        if (thirst == 0){
            stamina -= staminaDegen * Time.deltaTime;
        }
        // if TempBar = 0 start to drain stamina
        if (bodyTemp == 0){
            stamina -= staminaDegen * Time.deltaTime;
        }


        // attack / collect / gather
        if (Input.GetMouseButtonDown(0) && stamina >= 1)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, rayReach, layermask))
            {
                Debug.Log(hit.collider.name);
            }


            Collider2D[] interactableAxe = Physics2D.OverlapCircleAll(gatherPos.position, gatherRange, canGather);
            Collider2D[] interactablePickaxe = Physics2D.OverlapCircleAll(gatherPos.position, gatherRange, canGather2);
            
            if (invent.axeSelected)
            {
                for (int i = 0; i < interactableAxe.Length; i++)
                {

                    interactableAxe[i].GetComponent<Interactable>().health -= (damage + 10);
                }
            }
            else
            {
                for (int i = 0; i < interactableAxe.Length; i++)
                {

                    interactableAxe[i].GetComponent<Interactable>().health -= damage;
                }
            }

            if (invent.pickaxeSelected)
            {
                for (int i = 0; i < interactablePickaxe.Length; i++)
                {

                    interactablePickaxe[i].GetComponent<Interactable>().health -= (damage + 10);
                }
            }
            else
            {
                for (int i = 0; i < interactablePickaxe.Length; i++)
                {

                    interactablePickaxe[i].GetComponent<Interactable>().health -= damage;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Collider2D[] interactable = Physics2D.OverlapCircleAll(gatherPos.position, gatherRange, canGather);
            for (int i = 0; i < interactable.Length; i++)
            {
                if(interactable[i].GetComponent<Interactable>().canInteract)
                {
                    interactable[i].GetComponent<Interactable>().startGather = true;
                }
            }
        }

        // resetting to min and max values

        // HP
        if (hp > 100){
            hp = 100;
        }
        if (hp < 0){
            hp = 0;
        }

        // Stamina
        if (stamina > 100){
            stamina = 100;
        }
        if (stamina < 0){
            stamina = 0;
        }

        // Hunger
        if (hunger > 100){
            hunger = 100;
        }
        if (hunger < 0){
            hunger = 0;
        }

        // Thirst
        if (thirst > 100){
            thirst = 100;
        }
        if (thirst < 0){
            thirst = 0;
        }
        
        // BodyTemp
        if (bodyTemp > 100){
            bodyTemp = 100;
        }
        if (bodyTemp < 0){
            bodyTemp = 0;
        }
        


    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gatherPos.position, gatherRange);
    }
}
