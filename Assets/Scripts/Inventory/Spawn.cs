using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject Item;
    public Transform player;
    public Slots slots;
    public Inventory inventory; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        slots = GameObject.FindGameObjectWithTag("UI").GetComponent<Slots>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>(); 
    }


        public void SpawnDroppedItem(){
        Vector2 playerPos = new Vector2(player.position.x + Random.Range(-2f, 2f), player.position.y + -1f);
        Instantiate(Item, playerPos, Quaternion.identity);
    }
}
