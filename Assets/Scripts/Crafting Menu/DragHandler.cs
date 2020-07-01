using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private CraftingController cc;
    Vector3 origin;

    public void OnDrag(PointerEventData eventData)
    {
        if (cc.CraftOpen)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (cc.CraftOpen)
        {
            GameObject[] posSearch = GameObject.FindGameObjectsWithTag("slot");
            GameObject[] posSearch2 = GameObject.FindGameObjectsWithTag("bag");

            Vector2 transPos = transform.position;
            for (int i = 0; i < posSearch.Length; i++)
            {
                float distance = Vector3.Distance(transPos, posSearch[i].transform.position);
                if (distance < 90)
                {
                    //transform.position = posSearch[i].transform.position;
                    for (int x = 0; x < cc.items.Length; x++) // loop through each crafting space to look for an available slot
                    {
                        if (cc.items[x] == 0) // if it isnt full, add the item
                        {
                            cc.items[x] = 1; // item slot is now full
                            Instantiate(gameObject, cc.slots[x].transform, false); // allow for it to be moved back
                            cc.occupying[x] = gameObject; // set occupying slot to check if it can craft into anything
                            Destroy(gameObject); // destory the item in the world, since its already in our inventory
                            cc.CraftCheck(); // check if whats in the crafting table crafts into anything.
                            Debug.Log("moved to slot " + x);
                            break; // exit for loop.
                        }
                    }
                   // Debug.Log("moved to slot " + i);
                    //break;
                }
                else
                {
                    transform.position = origin;
                }
            }

            for (int i = 0; i < posSearch2.Length; i++)
            {
                float distance = Vector3.Distance(transPos, posSearch2[i].transform.position);
                if (distance < 90)
                {
                    for (int x = 0; x < cc.items.Length; x++) // loop through each crafting space to look for an available slot
                    {
                        if (cc.items[x] == 0) // if it isnt full, add the item
                        {
                            cc.items[x] = 1; // item slot is now full
                            Instantiate(gameObject, cc.slots[x].transform, false); // allow for it to be moved back
                            cc.occupying[x] = gameObject; // set occupying slot to check if it can craft into anything
                            Destroy(gameObject); // destory the item in the world, since its already in our inventory
                            cc.CraftCheck(); // check if whats in the crafting table crafts into anything.
                            Debug.Log("moved to slot " + x);
                            break; // exit for loop.
                        }
                    }
                }
                else
                {
                    transform.position = origin;
                }
            }

        }
        else
        {
            transform.position = origin;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        cc = GameObject.FindGameObjectWithTag("UI").GetComponent<CraftingController>();
    }
}
