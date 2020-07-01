using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBagCross : MonoBehaviour
{
    public GameObject ExtraSlotsCross7;
    public GameObject ExtraSlotsCross8;
    public GameObject ExtraSlotsCross9;
    public GameObject ExtraSlotsCross10;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ExtraSlotsCross7.gameObject.SetActive(false);
        ExtraSlotsCross8.gameObject.SetActive(false);
        ExtraSlotsCross9.gameObject.SetActive(false);
        ExtraSlotsCross10.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.addBag == true){
            ExtraSlotsCross7.gameObject.SetActive(true);
            ExtraSlotsCross8.gameObject.SetActive(true);
            ExtraSlotsCross9.gameObject.SetActive(true);
            ExtraSlotsCross10.gameObject.SetActive(true);
        }
    }
}
