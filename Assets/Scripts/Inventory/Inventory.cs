using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int[] items; // how many items we can store (in an array)
    public GameObject[] slots; // where the slots for the item to go are.

    public int scrollOn;
    public Image Hover;

    [Header("Don't change - Bag Check")]
    public GameObject[] bagSlot;
    public int minusBag = 4;

    public int axeNum = -1;
    public int pickaxeNum = -1;
    public int fishingNum = -1;

    public bool axeSelected;
    public bool pickaxeSelected;
    public bool fishingRodSelected;



    private void Start()
    {
        bagSlot = GameObject.FindGameObjectsWithTag("slot");
        for (int i = 6; i < bagSlot.Length; i++)
        {
            bagSlot[i].SetActive(false);
        }
        scrollOn = 0;
        Hover.transform.position = new Vector2(bagSlot[0].transform.position.x, bagSlot[0].transform.position.y + 30);
    }

    private void Update()
    {

        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            scrollOn--;

            if (scrollOn < 0)
            {
                scrollOn = (items.Length - minusBag - 1);
            }

            if (scrollOn > (items.Length - minusBag) - 1)
            {
                scrollOn = 0;
            }

            Hover.transform.position = new Vector2(bagSlot[scrollOn].transform.position.x, bagSlot[scrollOn].transform.position.y + 30);

            CheckSlotInfo();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            scrollOn++;

            if (scrollOn < 0)
            {
                scrollOn = (items.Length - minusBag - 1);
            }

            if (scrollOn > (items.Length - minusBag) - 1)
            {
                scrollOn = 0;
            }
            Hover.transform.position = new Vector2(bagSlot[scrollOn].transform.position.x, bagSlot[scrollOn].transform.position.y + 30);

            CheckSlotInfo();
        }
    }

    public void CheckSlotInfo()
    {
        if (axeNum == scrollOn)
        {
            Debug.Log("Axe!");
            axeSelected = true;
        }
        else if (pickaxeNum == scrollOn)
        {
            Debug.Log("Pickaxe!");
            pickaxeSelected = true;
        }
        else if (fishingNum == scrollOn)
        {
            Debug.Log("Fishing rod!");
            fishingRodSelected = true;
        }
    }
}
