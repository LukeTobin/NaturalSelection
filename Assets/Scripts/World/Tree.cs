using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private PlayerController player;
    private Interactable interact; // attatch object script
    public GameObject stick;
    public GameObject wood;
    public GameObject sapling;
    public GameObject stump;

    public GameObject fire;

    private int curHealth;

    private ScoreSystem scores;
    private Inventory inventory;
    public Animator animator;

    private float endTime;
    public float startEndTime;

    private float destroyTime;

    public bool isFire = false;
    bool spawnFlames;
    GameObject blaze;


    private void Start()
    {
        interact = GetComponent<Interactable>(); // get the object script that this is attatched too
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        curHealth = interact.health;
        animator.SetBool("hit", false);

        scores = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreSystem>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        destroyTime = Random.Range(3, 12);
    }

    private void Update()
    {

        if(interact.health < curHealth)
        {
            SoundManagerScript.PlaySound("chopTree");
            animator.SetBool("hit", true);
            curHealth = interact.health;
            player.stamina -= player.staminaUsage;
            scores.score += 1;         
        }

        // when the tree dies, spawn drops and destory the tree
        if(interact.health <= 0)
        {
            SoundManagerScript.PlaySound("fatalTree");
            SpawnDrops(1); 
            Destroy(gameObject);

            scores.score += 10;
        }

        if (endTime <= 0)
        {
            endTime = startEndTime;
            animator.SetBool("hit", false);
        }
        else
        {
            endTime -= Time.deltaTime;
        }


        if (destroyTime <= 0 && isFire)
        {
            // play animation
            animator.SetBool("hit", false);
            GameObject replace = Instantiate(stump);
            replace.transform.position = transform.position;
            Destroy(blaze);
            Destroy(gameObject);
        }
        else if(isFire && destroyTime > 0)
        {
            if (!spawnFlames)
            {
                
                blaze = Instantiate(fire);
                blaze.transform.position = new Vector2(transform.position.x, transform.position.y);
                spawnFlames = true;
            }
            animator.SetBool("hit", true);
            destroyTime -= Time.deltaTime;
        }

    }

    // spawn drops w/a type [in case we have some mechanic that changes drop rates]
    public void SpawnDrops(int type)
    {
        if(type == 1)
        {
            // destroy
            int x = Random.Range(1, 4);

            // get a random number called x between 1 & 3. spawn them around the object that was destoried.
            for (int i = 0; i < x; i++)
            {
                float randX = Random.Range(-2f, 2f);
                float randY = Random.Range(-2f, 2f);

                GameObject PickupSpawn = Instantiate(stick);
                PickupSpawn.transform.position = new Vector2(interact.transform.position.x + randX,interact.transform.position.y + randY);
            }

            GameObject PickupSpawn2 = Instantiate(wood);
            PickupSpawn2.transform.position = new Vector2(interact.transform.position.x, interact.transform.position.y);

            int y = Random.Range(1, 4);
            if(y != 1)
            {
                float randX = Random.Range(-2f, 2f);
                float randY = Random.Range(-2f, 2f);

                GameObject PickupSpawn3 = Instantiate(sapling);
                PickupSpawn3.transform.position = new Vector2(interact.transform.position.x + randX, interact.transform.position.y + randY);
            }
        }
    }
}
