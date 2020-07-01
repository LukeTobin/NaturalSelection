using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private PlayerController player;
    private Interactable interact; // attatch object script
    public Animator animator1;

    private SpriteRenderer sr;
    public Sprite bareBush;
    public Sprite berryBush;

    public GameObject berry;
    public GameObject stick;
    public GameObject plantfiber;

    private int curHealth;
    public bool bare;

    private float regrowTime;
    public float startRegrowTime;

    private float endTime;
    public float startEndTime;

    private ScoreSystem scores;

    

    private void Start()
    {
        interact = GetComponent<Interactable>(); // get the object script that this is attatched to
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        curHealth = interact.health;

        scores = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreSystem>();

        startRegrowTime = Random.Range(0f, 20f);
        regrowTime = startRegrowTime;
        bare = true;
        animator1.SetBool("isBare", true);
        sr.sprite = bareBush;
    }

    private void Update()
    {
        if (interact.health < curHealth)
        {
            SoundManagerScript.PlaySound("chopBush");
            animator1.SetInteger("hit", 1);
            endTime = startEndTime;
            curHealth = interact.health;
            player.stamina -= player.staminaUsage;
            scores.score += 1;
        }
        
        if (interact.health <= 0)
        {
            SoundManagerScript.PlaySound("fatalBush");
            SpawnDrops(1);
            Destroy(gameObject);
            scores.score += 10;
        }

        if (interact.canInteract && interact.startGather && !bare)
        {
            Debug.Log("gathering..");
            interact.startGather = false;

            int x = Random.Range(1, 3);

            for (int i = 0; i < x; i++)
            {
                float randX = Random.Range(-2f, 2f);
                float randY = Random.Range(-2f, 2f);

                GameObject PickupSpawn = Instantiate(berry);
                PickupSpawn.transform.position = new Vector2(interact.transform.position.x + randX, interact.transform.position.y + randY);
            }


            startRegrowTime = Random.Range(30f, 150f);

            sr.sprite = bareBush;
            bare = true;
            animator1.SetBool("isBare", true);

        }

        if (regrowTime <= 0 && bare)
        {
            regrowTime = startRegrowTime;
            animator1.SetBool("isBare", false);
            bare = false;
            sr.sprite = berryBush;
        }
        else
        {
            regrowTime -= Time.deltaTime;
        }

        if (endTime <= 0)
        {
            endTime = startEndTime;
            animator1.SetInteger("hit", 0);
        }
        else
        {
            endTime -= Time.deltaTime;
        }
    }

    // spawn drops w/a type [in case we have some mechanic that changes drop rates]
    public void SpawnDrops(int type)
    {
        
        if (type == 1)
        {

            int x = Random.Range(0, 5);
            if(x == 1)
            {
                float randX = Random.Range(-2f, 2f);
                float randY = Random.Range(-2f, 2f);

                GameObject PickupSpawn = Instantiate(berry);
                PickupSpawn.transform.position = new Vector2(interact.transform.position.x + randX, interact.transform.position.y + randY);
            }



            float randXz = Random.Range(-2f, 2f);
            float randYz = Random.Range(-2f, 2f);

            GameObject PickupSpawnz = Instantiate(stick);
            PickupSpawnz.transform.position = new Vector2(interact.transform.position.x + randXz, interact.transform.position.y + randYz);


            int y = Random.Range(1, 3);
            for (int i = 0; i < y; i++)
            {
                float randX = Random.Range(-2f, 2f);
                float randY = Random.Range(-2f, 2f);

                GameObject PickupSpawn = Instantiate(plantfiber);
                PickupSpawn.transform.position = new Vector2(interact.transform.position.x + randX, interact.transform.position.y + randY);
            }
        }
    }
}
