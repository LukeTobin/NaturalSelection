using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    private PlayerController player;
    public LayerMask whatIsMap;
    public Collider2D[] results;

    float bestDis = 100000f;
    GameObject bestTile;
    int bestNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        transform.position = new Vector2(player.newDirection.x, player.newDirection.y + 1);
        
        Collider2D[] mapCheck = Physics2D.OverlapCircleAll(transform.position, 0.6f, whatIsMap);
        if(mapCheck == null)
        {
            Debug.Log("Null!");
        }
        else
        {
            Debug.Log("Found Collider!");
            if(mapCheck.Length > 0)
            {
                for (int i = 0; i < mapCheck.Length; i++)
                {
                    if (Vector2.Distance(mapCheck[i].transform.position, transform.position) < bestDis)
                    {

                        bestTile = mapCheck[i].gameObject;
                        bestDis = Vector2.Distance(mapCheck[i].transform.position, transform.position);
                        bestNum = i;
                    }
                }

                transform.position = mapCheck[bestNum].transform.position;
                Destroy(mapCheck[bestNum]);
                Debug.Log("Placed at: " + bestNum);
            }
        }
    }
}
