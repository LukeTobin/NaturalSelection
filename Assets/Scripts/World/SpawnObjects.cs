using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] objects;

    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        GameObject origin = (GameObject)Instantiate(objects[rand], transform.position, Quaternion.identity);
        origin.transform.parent = transform;
    }

}
