using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    private Inventory inventory; // access the inventory script

    public GameObject itemButton; // attach this game object

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>(); // set the inventory script to be attatched to the "player", while they have the Inventory script
    }

    private void OnTriggerEnter2D(Collider2D other) // when X collides with Y
    {
        if (other.CompareTag("Player")) // if Y is not the Player 
        {
            for (int i = 0; i < inventory.items.Length - inventory.minusBag; i++) // loop through each slot to look for the first available slot in your inventory
            {
                if (inventory.items[i] == 0) // if it isnt full, add the item
                {
                    SoundManagerScript.PlaySound("grabItem");
                    inventory.items[i] = 1; // item slot is now full
                    Instantiate(itemButton, inventory.slots[i].transform, false); // allow it to be interacted with
                    Destroy(gameObject); // destory the item in the world, since its already in our inventory
                    break; // exit for loop.
                }
            }
        }
    }

}
