using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : MonoBehaviour
{

    public GameObject tree;

    private PlayerController player;

    private float regrowTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 2);

        regrowTime = Random.Range(60f, 180f); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (regrowTime <= 0)
        {
            GameObject newTree = Instantiate(tree);
            newTree.transform.position = transform.position;
            Destroy(gameObject);
        }
        else
        {
            regrowTime -= Time.deltaTime;
        }
    }
}
