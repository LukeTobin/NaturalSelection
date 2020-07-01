using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    
    private Image bar;
    private StaminaController stamina;



    void Start()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        stamina = new StaminaController();
    }

    private void Update()
    {
        stamina.Update();

        bar.fillAmount = stamina.GetStamina();
    }
}

public class StaminaController
{
    public const int MAX_STAMINA = 100;

    private float stamina;
    private float staminaRegen;

    public StaminaController()
    {
        stamina = Random.Range(30f, 101f);
        Debug.Log("Stamina " + stamina);
        staminaRegen = 1f;
    }

    public void Update()
    {
        stamina += staminaRegen * Time.deltaTime;
        stamina = Mathf.Clamp(stamina, 0f, MAX_STAMINA);
    }

    public float GetStamina()
    {
        return stamina / MAX_STAMINA;
    }
}
