using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillArea : MonoBehaviour
{
    /*
     * Script to fill any empty areas in a grid with water.
     */
    public LayerMask whatIsMap;
    public WorldGenerator world;

    void Update()
    {
        Collider2D mapCheck = Physics2D.OverlapCircle(transform.position, 1, whatIsMap);
        if(mapCheck == null && world.stopGenerationAll)
        {
            //int rand = Random.Range(0, world.water.Length);
            Instantiate(world.water, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
