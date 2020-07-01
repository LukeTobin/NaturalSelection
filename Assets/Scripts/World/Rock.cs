using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private PlayerController player;
    private Interactable interact; // attatch object script
    public GameObject stone;

    private int curHealth;

    private ScoreSystem scores;

    private void Start()
    {
        interact = GetComponent<Interactable>(); // get the object script that this is attatched too
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        curHealth = interact.health;

        scores = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreSystem>();
    }

    private void Update()
    {

        if (interact.health < curHealth)
        {
            /*
            Vector2 newPos = new Vector2(transform.position.x + 10, transform.position.y);   
            transform.position = Vector2.Lerp(transform.position, newPos, Time.deltaTime);*/
            SoundManagerScript.PlaySound("hitRock");
            curHealth = interact.health;
            player.stamina -= player.staminaUsage;
            scores.score += 1;
        }

        // when the tree dies, spawn drops and destory the tree
        if (interact.health <= 0)
        {
            //SoundManagerScript.PlaySound("fatalTree");
            SpawnDrops(1);
            Destroy(gameObject);
            SoundManagerScript.PlaySound("fatalRock");
            scores.score += 10;
        }
    }

    // spawn drops w/a type [in case we have some mechanic that changes drop rates]
    public void SpawnDrops(int type)
    {
        if (type == 1)
        {
            // destroy
            int x = Random.Range(1, 4);

            // get a random number called x between 1 & 3. spawn them around the object that was destoried.
            for (int i = 0; i < x; i++)
            {
                float randX = Random.Range(-2f, 2f);
                float randY = Random.Range(-2f, 2f);

                GameObject PickupSpawn = Instantiate(stone);
                PickupSpawn.transform.position = new Vector2(interact.transform.position.x + randX, interact.transform.position.y + randY);
            }
        }
    }
}
