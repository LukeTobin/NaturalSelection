using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip menuUse;
    public static AudioClip treeChop;
    public static AudioClip bushChop;
    public static AudioClip itemGrab;
    public static AudioClip itemCraft;
    public static AudioClip craftOpen;
    public static AudioClip craftClose;
    public static AudioClip berryEat;
    public static AudioClip treeFatal;
    public static AudioClip bushFatal;
    public static AudioClip rockHit;
    public static AudioClip rockFatal;
    public static AudioClip grassWalk;

    public static AudioClip secretSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        menuUse = Resources.Load<AudioClip>("menuUse");
        treeChop = Resources.Load<AudioClip>("treeChop");
        bushChop = Resources.Load<AudioClip>("bushChop");
        itemGrab = Resources.Load<AudioClip>("itemGrab");
        itemCraft = Resources.Load<AudioClip>("itemCraft");
        craftOpen = Resources.Load<AudioClip>("craftOpen");
        craftClose = Resources.Load<AudioClip>("craftClose");
        berryEat = Resources.Load<AudioClip>("berryEat");
        treeFatal = Resources.Load<AudioClip>("treeFatal");
        bushFatal = Resources.Load<AudioClip>("bushFatal");
        rockHit = Resources.Load<AudioClip>("rockHit");
        rockFatal = Resources.Load<AudioClip>("rockFatal");
        grassWalk = Resources.Load<AudioClip>("grassWalk");

        secretSound = Resources.Load<AudioClip>("secretSound");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "useMenu":
                audioSrc.PlayOneShot(menuUse);
                break;
            case "chopTree":
                audioSrc.PlayOneShot(treeChop);
                break;
            case "chopBush":
                audioSrc.PlayOneShot(bushChop);
                break;
            case "grabItem":
                audioSrc.PlayOneShot(itemGrab);
                break;
            case "craftItem":
                audioSrc.PlayOneShot(itemCraft);
                break;
            case "openCraft":
                audioSrc.PlayOneShot(craftOpen);
                break;
            case "closeCraft":
                audioSrc.PlayOneShot(craftClose);
                break;
            case "eatBerry":
                audioSrc.PlayOneShot(berryEat);
                break;
            case "soundSecret":
                audioSrc.PlayOneShot(secretSound);
                break;
            case "fatalTree":
                audioSrc.PlayOneShot(treeFatal);
                break;
            case "fatalBush":
                audioSrc.PlayOneShot(bushFatal);
                break;
            case "hitRock":
                audioSrc.PlayOneShot(rockHit);
                break;
            case "fatalRock":
                audioSrc.PlayOneShot(rockFatal);
                break;
            case "walkGrass":
                audioSrc.PlayOneShot(grassWalk);
                break;
        }
    }
}
