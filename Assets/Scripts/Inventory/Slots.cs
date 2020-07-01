using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public Inventory inventory;
    private PlayerController player;
    public int index;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>(); // get inventory attachted to player
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (transform.childCount <= 0) // if the amount of inhabitants within a slot drops to 0, make that slot available again
        {
            inventory.items[index] = 0;
        }
    }


    public void DropItem(){
        if (player.stamina >= 1)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Spawn>().SpawnDroppedItem();
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
