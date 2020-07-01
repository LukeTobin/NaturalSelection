using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCSlots : MonoBehaviour
{

    // same script as for inventories slots, but this is to handle crafting slots.

    private CraftingController cc;
    public int index;

    private void Start()
    {

        cc = GameObject.FindGameObjectWithTag("UI").GetComponent<CraftingController>();
    }

    private void Update()
    {
        if (transform.childCount <= 0) 
        {
            cc.items[index] = 0;
        }
    }

    public void ForceUpdate()
    {
        if (transform.childCount <= 0)
        {
            cc.items[index] = 0;
        }
    }
}
