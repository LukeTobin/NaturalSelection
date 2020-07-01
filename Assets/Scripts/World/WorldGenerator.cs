using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    // vars for script
    public Transform[] startingPos;
    public GameObject[] maps;
    public GameObject water;

    private int direction;
    public float moveAmount;

    private int randStart;

    private float waitTime;
    private float startWait = 0.05f;

    public float maxX;
    public float minX;
    public float minY;

    public float maxX2;
    public float minX2;
    public float minY2;

    private bool stopGenerationBottom;
    public bool stopGenerationAll;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        randStart = Random.Range(0, startingPos.Length);
        transform.position = startingPos[randStart].position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); // update players position.
        player.transform.position = transform.position;
        Instantiate(maps[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);


    }

    private void Update()
    {
        if(waitTime <= 0 && !stopGenerationBottom)
        {
            MoveDown();
            waitTime = startWait;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }

        if(waitTime <= 0 && stopGenerationBottom && !stopGenerationAll)
        {
            MoveUp();
            waitTime = startWait;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }

    #region Desert Generation
    private void MoveDown()
    {
        if(direction == 1 || direction == 2)
        {
            if(transform.position.x < maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, maps.Length);
                Instantiate(maps[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if(direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
            
        }
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, maps.Length);
                Instantiate(maps[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else if(direction == 5)
        {
            if(transform.position.y > minY)
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(0, maps.Length);
                Instantiate(maps[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                direction = 6;
                stopGenerationBottom = true;
            }
            
        }
    }

    private void MoveUp()
    {
        if (direction == 1 || direction == 2)
        {
            if (transform.position.x < maxX2)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, maps.Length);
                Instantiate(maps[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 2;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }

        }
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX2)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, maps.Length);
                Instantiate(maps[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        else if (direction == 5)
        {
            if (transform.position.y < minY2)
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y + moveAmount);
                transform.position = newPos;

                int rand = Random.Range(0, maps.Length);
                Instantiate(maps[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                stopGenerationAll = true;
            }

        }
        else if(direction == 6)
        {
            Vector2 newPos = new Vector2(startingPos[randStart].position.x, startingPos[randStart].position.y + moveAmount);
            transform.position = newPos;

            int rand = Random.Range(0, maps.Length);
            Instantiate(maps[rand], transform.position, Quaternion.identity);

            direction = Random.Range(1, 6);
        }

    }
    #endregion
}
