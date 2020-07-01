using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{

    // public GameObject effect;
    private PlayerController player;
    private CraftingController cc;
    private Inventory inventory;
    private CCSlots ccslots;
    private ScoreSystem scores;

    // items
    public GameObject Nbandage;
    public GameObject bandage;

    public GameObject stick;
    public GameObject Nstick;

    public GameObject stone;
    public GameObject Nstone;

    public GameObject wood;
    public GameObject Nwood;

    public GameObject campfire;

    public GameObject fiber;
    public GameObject Nfiber;

    public GameObject strings;
    public GameObject Nstrings;

    public GameObject net;
    public GameObject Nnet;
    public int recoverSpeed;

    public GameObject sap;
    public GameObject plank;

    //Please do not touch this variable
    public string itemSelect;

    private int slotNum;
    private bool destroyEmpty;
    //public bool campfireUsed;

    public int staminaUsage;

    // additional vars
    private bool isSelected;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cc = GameObject.FindGameObjectWithTag("UI").GetComponent<CraftingController>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        scores = GameObject.FindGameObjectWithTag("score").GetComponent<ScoreSystem>();
    }

    private void Update()
    {
        if (cc.items[0] == 0)
        {
            if (cc.occupying[0] != null)
            {
                Destroy(cc.newItem);
                cc.newItem = null;

                GameObject[] searchRecipe = GameObject.FindGameObjectsWithTag("recipe");
                for (int i = 0; i < searchRecipe.Length; i++)
                {
                    Destroy(searchRecipe[i]);
                }

                cc.occupying[0] = null;

                cc.CraftCheck();
            }

        }

        if (cc.items[1] == 0)
        {
            if (cc.occupying[1] != null)
            {
                Destroy(cc.newItem);
                cc.newItem = null;

                GameObject[] searchRecipe = GameObject.FindGameObjectsWithTag("recipe");
                for (int i = 0; i < searchRecipe.Length; i++)
                {
                    Destroy(searchRecipe[i]);
                }

                cc.occupying[1] = null;

                cc.CraftCheck();
            }

        }

        if (cc.items[2] == 0)
        {
            if (cc.occupying[2] != null)
            {
                Destroy(cc.newItem);
                cc.newItem = null;

                GameObject[] searchRecipe = GameObject.FindGameObjectsWithTag("recipe");
                for (int i = 0; i < searchRecipe.Length; i++)
                {
                    Destroy(searchRecipe[i]);
                }

                cc.occupying[2] = null;

                cc.CraftCheck();
            }

        }

        if (!cc.MadeCraft)
        {
            cc.CraftCheck();
        }

        if (cc.items[0] == 0 && cc.items[1] == 0 && cc.items[2] == 0 && !destroyEmpty)
        {
            Destroy(cc.newItem);
            cc.newItem = null;

            GameObject[] searchRecipe = GameObject.FindGameObjectsWithTag("recipe");
            for (int i = 0; i < searchRecipe.Length; i++)
            {
                Destroy(searchRecipe[i]);
            }

            destroyEmpty = true;
        }

        // drag & drop
        if (isSelected)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos;
        }

        if (Input.GetMouseButtonUp(0))
        { // no longer selected
            isSelected = false;
        }

    }

    private void OnMouseDown()
    {
        if (cc.CraftOpen)
        {
            isSelected = true;
        }     
    }

    // add an "N" item to the crafting table
    public void addToCrafting(GameObject added)
    {
        SoundManagerScript.PlaySound("useMenu");
        // add item to crafting table
        for (int i = 0; i < cc.items.Length; i++) // loop through each crafting space to look for an available slot
        {
            if (cc.items[i] == 0) // if it isnt full, add the item
            {
                cc.items[i] = 1; // item slot is now full
                Instantiate(added, cc.slots[i].transform, false); // allow for it to be moved back
                cc.occupying[i] = added; // set occupying slot to check if it can craft into anything
                Destroy(gameObject); // destory the item in the world, since its already in our inventory
                cc.CraftCheck(); // check if whats in the crafting table crafts into anything.
                break; // exit for loop.
            }
        }

        destroyEmpty = false;
    }

    // return item from crafting table to inventory safely
    public void returnToInventory(GameObject returned)
    {
        SoundManagerScript.PlaySound("useMenu");
        for (int i = 0; i < inventory.items.Length - inventory.minusBag; i++) // loop through each slot to look for the first available slot in your inventory
        {
            if (inventory.items[i] == 0) // if it isnt full, add the item
            {
                inventory.items[i] = 1; // item slot is now full
                Instantiate(returned, inventory.slots[i].transform, false); // allow it to be interacted with
                Destroy(gameObject); // destory the item in the world, since its already in our inventory
                break; // exit for loop.
            }
        }

        cc.MadeCraft = false;
    }


    // create item & destory two objects used to create it
    public void createItem(GameObject create)
    {
        for (int i = 0; i < inventory.items.Length - inventory.minusBag; i++) // loop through each slot to look for the first available slot in your inventory
        {
            if (inventory.items[i] == 0) // if it isnt full, add the item
            {
                inventory.items[i] = 1; // item slot is now full
                player.stamina -= staminaUsage;
                Instantiate(create, inventory.slots[i].transform, false); // allow it to be interacted with
                Destroy(gameObject); // destory the item in the world, since its already in our inventory
                cc.MadeCraft = false;
                SoundManagerScript.PlaySound("craftItem");
                scores.score += 25;
                Debug.Log(scores.score);
                break; // exit for loop.
            }
        }

        GameObject[] destroySearch = GameObject.FindGameObjectsWithTag("crafting");
        for (int i = 0; i < destroySearch.Length; i++)
        {
            Destroy(destroySearch[i]);
        }

        // empty slots
        cc.items[0] = 0;
        cc.items[1] = 0;
        cc.items[2] = 0;
    }

    public void createAxe(GameObject create)
    {
        for (int i = 0; i < inventory.items.Length - inventory.minusBag; i++) // loop through each slot to look for the first available slot in your inventory
        {
            if (inventory.items[i] == 0) // if it isnt full, add the item
            {
                inventory.items[i] = 1; // item slot is now full
                player.stamina -= staminaUsage;
                inventory.axeNum = i;
                Instantiate(create, inventory.slots[i].transform, false); // allow it to be interacted with
                Destroy(gameObject); // destory the item in the world, since its already in our inventory
                cc.MadeCraft = false;
                SoundManagerScript.PlaySound("craftItem");
                scores.score += 25;
                Debug.Log(scores.score);
                inventory.CheckSlotInfo();
                break; // exit for loop.
            }
        }

        GameObject[] destroySearch = GameObject.FindGameObjectsWithTag("crafting");
        for (int i = 0; i < destroySearch.Length; i++)
        {
            Destroy(destroySearch[i]);
        }

        // empty slots
        cc.items[0] = 0;
        cc.items[1] = 0;
        cc.items[2] = 0;
    }

    public void createPickaxe(GameObject create)
    {
        for (int i = 0; i < inventory.items.Length - inventory.minusBag; i++) // loop through each slot to look for the first available slot in your inventory
        {
            if (inventory.items[i] == 0) // if it isnt full, add the item
            {
                inventory.items[i] = 1; // item slot is now full
                player.stamina -= staminaUsage;
                inventory.pickaxeNum = i;
                Instantiate(create, inventory.slots[i].transform, false); // allow it to be interacted with
                Destroy(gameObject); // destory the item in the world, since its already in our inventory
                cc.MadeCraft = false;
                SoundManagerScript.PlaySound("craftItem");
                scores.score += 25;
                Debug.Log(scores.score);
                inventory.CheckSlotInfo();
                break; // exit for loop.
            }
        }

        GameObject[] destroySearch = GameObject.FindGameObjectsWithTag("crafting");
        for (int i = 0; i < destroySearch.Length; i++)
        {
            Destroy(destroySearch[i]);
        }

        // empty slots
        cc.items[0] = 0;
        cc.items[1] = 0;
        cc.items[2] = 0;
    }

    public void createFishingRod(GameObject create)
    {
        for (int i = 0; i < inventory.items.Length - inventory.minusBag; i++) // loop through each slot to look for the first available slot in your inventory
        {
            if (inventory.items[i] == 0) // if it isnt full, add the item
            {
                inventory.items[i] = 1; // item slot is now full
                player.stamina -= staminaUsage;
                inventory.fishingNum = i;
                Instantiate(create, inventory.slots[i].transform, false); // allow it to be interacted with
                Destroy(gameObject); // destory the item in the world, since its already in our inventory
                cc.MadeCraft = false;
                SoundManagerScript.PlaySound("craftItem");
                scores.score += 25;
                Debug.Log(scores.score);
                inventory.CheckSlotInfo();
                break; // exit for loop.
            }
        }

        GameObject[] destroySearch = GameObject.FindGameObjectsWithTag("crafting");
        for (int i = 0; i < destroySearch.Length; i++)
        {
            Destroy(destroySearch[i]);
        }

        // empty slots
        cc.items[0] = 0;
        cc.items[1] = 0;
        cc.items[2] = 0;
    }

    // Bandages
    public void UseBandage()
    {
        if (cc.CraftOpen)
        {
            addToCrafting(Nbandage);
        }
        else
        {
            if (player.stamina >= 1)
            {
                player.stamina += player.staminaGain;
                Destroy(gameObject);
                //SoundManagerScript.PlaySound("soundSecret");
                SoundManagerScript.PlaySound("eatBerry");
                //StartCoroutine(BandagePlayer(recoverSpeed, 10f));
            }
        }

    }


    public void UseStick()
    {
        if (cc.CraftOpen)
        {
            addToCrafting(Nstick);
        }
        else
        {
            // nothing w/stick
        }
    }


    public void UseStone()
    {
        if (cc.CraftOpen)
        {
            addToCrafting(Nstone);
        }
        else
        {
            // nothing w/stone
        }
    }

    public void UseWood()
    {
        if (cc.CraftOpen)
        {
            addToCrafting(Nwood);
        }
        else
        {
            // nothing w/stick
        }
    }

    public void UseCampfire()
    {
        if (cc.CraftOpen)
        {
           // addToCrafting(Nwood);
        }
        else
        {
            if (player.stamina >= 1)
            {
                Instantiate(campfire);
                Destroy(gameObject);

                /*
                player.bodyTemp += 50;
                player.TempBar.value = player.bodyTemp;
                Destroy(gameObject);
                //campfireUsed = true;*/
            }
        }
    }

    public void UseFiber()
    {
        if (cc.CraftOpen)
        {
             addToCrafting(Nfiber);
        }
        else
        {
            // nothing w/stick
            //Destroy(gameObject);
        }
    }

    public void UseString()
    {
        if (cc.CraftOpen)
        {
            addToCrafting(Nstrings);
        }
        else
        {
            // nothing w/string
         
        }
    }

    public void UseNet()
    {
        if (cc.CraftOpen)
        {
           addToCrafting(Nnet);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    public void UseAxe()
    {
        if (cc.CraftOpen)
        {
           //Nothing yet
        }
            else
       {
            SoundManagerScript.PlaySound("soundSecret");
            //itemSelect = "Axe";
        }
    }

    public void UsePickaxe()
    {
        if (cc.CraftOpen)
        {
            //Nothing yet
        }
        else
        {
            SoundManagerScript.PlaySound("soundSecret");
            //itemSelect = "Axe";
        }
    }

    public void UseSapling()
    {
        if (cc.CraftOpen)
        {
            
        }
        else
        {
            Debug.Log("placed sapling");
            Instantiate(sap);
            Destroy(gameObject);
        }
    }

    public void UsePlanks()
    {
        if (cc.CraftOpen)
        {

        }
        else
        {
            IEnumerator co = PlacePlanks();
            StartCoroutine(co);
        }
    }


    public void AddBag()
    {
        inventory.minusBag = 0;
        for (int i = 0; i < inventory.bagSlot.Length; i++)
        {
            inventory.bagSlot[i].SetActive(true);
        }
        Destroy(gameObject);
        player.addBag = true;
    }

    #region Scripts
    IEnumerator BandagePlayer(int heal, float time)
    {
        int countdown = heal;
        while (true)
        { // loops forever...
            if (player.stamina <= 100 && countdown > 0)
            {
                player.stamina += 3; // increase health and wait the specified time
                countdown -= 3;
                // yield return new WaitForSeconds(1);
            }
            else
            { // if health >= 100, just yield 
                Debug.Log(player.stamina);
                yield return null;
            }
        }
        yield return new WaitForSeconds(time);
    }

    IEnumerator PlacePlanks()
    {
        while (true)
        { // loops forever...
            if (Input.GetMouseButtonDown(0))
            {
                player.newDirection = player.direction;
                Instantiate(plank);
                Destroy(gameObject);
                break;
            }

            yield return null;
        }
    }
    #endregion

}
