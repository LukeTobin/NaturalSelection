using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class Recipes : ScriptableObject
{

    public string name; 

    public GameObject input1;
    public GameObject input2;
    public GameObject input3;

    public int values; // how many inputs it contains

    public GameObject result;

}
