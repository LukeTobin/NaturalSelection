using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wether : MonoBehaviour

{
    public GameObject[] WeatherParticles;
    private int particleRandom;
    private float chanceOfRain = 2f;
    private float RainLength;
    private float dice;
    private bool isSnowing;
    private float timer = 0f;
    void Update()
    {
        if(!isSnowing){
            dice = Random.Range(0f,10f);
            if(dice < chanceOfRain){
                particleRandom = Random.Range(0, WeatherParticles.Length);
                snow(particleRandom);
                isSnowing = true;
                timer = Random.Range(20f,30f);

            }
        }
        if(isSnowing){
            timer -= Time.deltaTime;
            if(timer <= 0){
                isSnowing = false;
                Stopsnow(particleRandom);
            }
        }
        
    }
    private void snow(int particleRandom)
    {
        WeatherParticles[particleRandom].SetActive(true);
    }
    private void Stopsnow( int particleRandom){
        WeatherParticles[particleRandom].SetActive(false);
    }
}
