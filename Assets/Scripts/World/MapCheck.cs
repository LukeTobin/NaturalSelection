using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCheck : MonoBehaviour
{
    // check that map type and have an option to destroy.

        // Mostly used for circlecollider to attatch too.

    public int type;

    public void DestroyMap()
    {
        Destroy(gameObject);
    }
}
