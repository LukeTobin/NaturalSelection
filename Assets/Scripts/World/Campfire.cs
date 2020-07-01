using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    private PlayerController player;
    private SpriteRenderer sr;

    public Sprite unlit;

    private float endTime;
    private bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 2);

        endTime = Random.Range(15, 40);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(player.transform.position, transform.position) <= 3 && !stop)
        {
            Debug.Log("Close to campfire");
        }

        if (!stop)
        {
            GameObject[] searchTree = GameObject.FindGameObjectsWithTag("tree");
            for (int i = 0; i < searchTree.Length; i++)
            {
                if (Vector2.Distance(searchTree[i].transform.position, transform.position) <= 2)
                {
                    searchTree[i].GetComponent<Tree>().isFire = true;
                }
            }
        }
        

        if (endTime <= 0)
        {
            stop = true;
            sr.sprite = unlit;
        }
        else
        {
            endTime -= Time.deltaTime;
        }
    }
}
