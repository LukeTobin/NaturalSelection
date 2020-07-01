using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public int health;
    public bool canInteract;

    [Header("Don't change - Gather Switch")]
    public bool startGather;

    public void PickBreak(GameObject drop, GameObject from) // pick berries / break trees etc..
    {

            float randX = Random.Range(-2f, 2f);
            float randY = Random.Range(-2f, 2f);

            GameObject PickupSpawn = Instantiate(drop);
            PickupSpawn.transform.position = new Vector2(from.transform.position.x + randX, from.transform.position.y + randY);
    }
}
