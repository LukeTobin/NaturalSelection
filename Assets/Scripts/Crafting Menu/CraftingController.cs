using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour
{

    public Recipes[] recipes;
    public GameObject[] occupying;
    public GameObject newItem;


    public GameObject CraftMenu;
    private PlayerController player;
    private Inventory inventory;
    private Items _item;
    public Button OpenClose;
    public Button Close;

    public bool CraftOpen;
    public bool MadeCraft;

    public int[] items; // how many items we can store (in an array)
    public GameObject[] slots;
    public GameObject result;
    public string[] type;

    public GameObject[] invenOpts;

    int counter;


    void Start()
    {
        OpenClose.onClick.AddListener(showHide);
        Close.onClick.AddListener(showHide);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        recipes = Resources.LoadAll<Recipes>("Recipe");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player.stamina >= 1)
        {
            showHide();
        }

        if (CraftOpen == true){
            if (Input.GetKeyDown(KeyCode.Escape)){
                showHide();
            }
        }
    }

    void showHide()
    {
        counter++;
        if (counter % 2 == 1 && player.stamina >= 1)
        {
            CraftMenu.gameObject.SetActive(true);
            CraftOpen = true;
            //player.moveSpeed = 0f;
        }
        else
        {
            DestroySlots();
            CraftMenu.gameObject.SetActive(false);
            CraftOpen = false;
            //player.moveSpeed = 4.5f;
        }
    }

    public void DestroySlots()
    {
        GameObject[] destroySearch = GameObject.FindGameObjectsWithTag("crafting");


        for (int i = 0; i < destroySearch.Length; i++)
        {
            for (int j = 0; j < invenOpts.Length; j++)
            {
                if (destroySearch[i].name == invenOpts[j].name + " 2(Clone)")
                {
                    for (int x = 0; x < inventory.items.Length - inventory.minusBag; x++)
                    {
                        if (inventory.items[x] == 0) 
                        {
                            inventory.items[x] = 1; 
                            Instantiate(invenOpts[j], inventory.slots[x].transform, false);
                            break;
                        }
                    }
                    Destroy(destroySearch[i]);
                    Debug.Log("added: " + invenOpts[j].name);
                }
            }
            
        }

        items[0] = 0;
        items[1] = 0;
        items[2] = 0;
    }

    public void CraftCheck()
    {
        if (items[0] == 1 && items[1] == 1 && items[2] == 0)
        {
            foreach (Recipes recipe in recipes)
            {
                if (recipe.values == 2)
                {
                    if (recipe.input1 == occupying[0] && recipe.input2 == occupying[1] ||
                    recipe.input1 == occupying[1] && recipe.input2 == occupying[0])
                    {
                        Debug.Log("Craft Successful ! - " + recipe.name);
                        
                        newItem = Instantiate(recipe.result, result.transform, false);
                        MadeCraft = true;
                        break;
                    }
                    else
                    {
                        MadeCraft = false;
                        SetNull();
                    }
                }
                else
                {
                    MadeCraft = false;
                    SetNull();
                }

            }
        }
        else if (items[0] == 0 && items[1] == 1 && items[2] == 1)
        {
            foreach (Recipes recipe in recipes)
            {
                if (recipe.values == 2)
                {
                    if (recipe.input1 == occupying[2] && recipe.input2 == occupying[1] ||
                    recipe.input1 == occupying[1] && recipe.input2 == occupying[2])
                    {
                        Debug.Log("Craft Successful ! - " + recipe.name);
                        
                        newItem = Instantiate(recipe.result, result.transform, false);
                        MadeCraft = true;
                        break;
                    }
                    else
                    {
                        MadeCraft = false;
                        SetNull();
                    }
                }
                else
                {
                    MadeCraft = false;
                    SetNull();
                }
            }
        }
        else if (items[0] == 1 && items[1] == 0 && items[2] == 1)
        {
            foreach (Recipes recipe in recipes)
            {
                if (recipe.values == 2)
                {
                    if (recipe.input1 == occupying[2] && recipe.input2 == occupying[0] ||
                    recipe.input1 == occupying[0] && recipe.input2 == occupying[2])
                    {
                        Debug.Log("Craft Successful ! - " + recipe.name);
                        
                        newItem = Instantiate(recipe.result, result.transform, false);
                        MadeCraft = true;
                        break;
                    }
                    else
                    {
                        MadeCraft = false;
                        SetNull();
                    }
                }
                else
                {
                    MadeCraft = false;
                    SetNull();
                }
            }
        }
        else if (items[0] == 1 && items[1] == 1 && items[2] == 1)
        {
            foreach (Recipes recipe in recipes)
            {
                if (recipe.values == 3)
                {

                    if (recipe.input1 == occupying[0] && recipe.input2 == occupying[1] && recipe.input3 == occupying[2] ||
                        recipe.input1 == occupying[1] && recipe.input2 == occupying[0] && recipe.input3 == occupying[2] ||
                        recipe.input1 == occupying[1] && recipe.input2 == occupying[2] && recipe.input3 == occupying[0] ||
                        recipe.input1 == occupying[0] && recipe.input2 == occupying[2] && recipe.input3 == occupying[1] ||
                        recipe.input1 == occupying[2] && recipe.input2 == occupying[1] && recipe.input3 == occupying[0] ||
                        recipe.input1 == occupying[2] && recipe.input2 == occupying[0] && recipe.input3 == occupying[1])
                    {
                        Debug.Log("Craft Successful ! - " + recipe.name);
                        
                        newItem = Instantiate(recipe.result, result.transform, false);
                        MadeCraft = true;
                        break;
                    }
                    else
                    {
                        MadeCraft = false;
                        SetNull();
                    }
                }
                else
                {
                    MadeCraft = false;
                    SetNull();            
                }
            }
        }
        else if (items[0] == 1 && items[1] == 0 && items[2] == 0)
        {
            foreach (Recipes recipe in recipes)
            {
                if (recipe.values == 1)
                {
                    if (recipe.input1 == occupying[0])
                    {
                        Debug.Log("Craft Successful ! - " + recipe.name);
                        SetNull();
                        
                        newItem = Instantiate(recipe.result, result.transform, false);
                        MadeCraft = true;
                        break;
                    }
                    else
                    {
                        MadeCraft = false;
                        SetNull();
                    }
                }
                else
                {
                    MadeCraft = false;
                    SetNull();
                }

            }
        }
        else if (items[0] == 0 && items[1] == 1 && items[2] == 0)
        {
            foreach (Recipes recipe in recipes)
            {
                if (recipe.values == 1)
                {
                    if (recipe.input1 == occupying[1])
                    {
                        Debug.Log("Craft Successful ! - " + recipe.name);
                        SetNull();
                        
                        newItem = Instantiate(recipe.result, result.transform, false);
                        MadeCraft = true;
                        break;
                    }
                    else
                    {
                        MadeCraft = false;
                        SetNull();
                    }
                }
                else
                {
                    MadeCraft = false;
                    SetNull();
                }

            }
        }
        else if (items[0] == 0 && items[1] == 0 && items[2] == 1)
        {
            foreach (Recipes recipe in recipes)
            {
                if (recipe.values == 1)
                {
                    if (recipe.input1 == occupying[2])
                    {
                        Debug.Log("Craft Successful ! - " + recipe.name);
                        SetNull();
                        
                        newItem = Instantiate(recipe.result, result.transform, false);
                        MadeCraft = true;
                        break;
                    }
                    else
                    {
                        MadeCraft = false;
                        SetNull();
                    }
                }
                else
                {
                    MadeCraft = false;
                    SetNull();
                }

            }
        }
        else
        {
            MadeCraft = false;
        }


    }

    // needs to be fixed to check if a single or double item combo is in use.
    public void SetNull()
    {
        Destroy(newItem);
        newItem = null;

        GameObject[] searchRecipe = GameObject.FindGameObjectsWithTag("recipe");
        for (int i = 0; i < searchRecipe.Length; i++)
        {
            Destroy(searchRecipe[i]);
        }
    }

}
