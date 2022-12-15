using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class OrbitParser : MonoBehaviour
{
    public GameObject planetPrefab;
    private GameObject currentPlanet;
    private float h = 5;
    private float lo = 0;
    private float la = 0;
    private Vector3 force = new Vector3();
    private float timer = 0;
    public void Start()
    {
        Time.timeScale = 0.01f;
    }
    public void Update()
    {
        //Spawn massive of planets with different h, lo, la, and force
        
        if (timer >= 4f && timer < 4.01f && currentPlanet)
        {
            
            print("h: " + h + " lo: " + lo + " la: " + la + " force: " + force +  " planet: " + currentPlanet.name);
        }

        if (timer >= 8 && currentPlanet)
        {
            print("h: " + h + " lo: " + lo + " la: " + la + " force: " + force + " planet IS REALLY NICE: " + currentPlanet.name + "time: " + timer);
            
        }
        if (currentPlanet == null)
        {
            currentPlanet = Instantiate(planetPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            
            h = Random.Range(5, 20);
            lo = Random.Range(-180,1890);
            la = Random.Range(25,50);
            force.x = Random.Range(-22000,100000);
            force.y = Random.Range(-6000,10000);
            force.z = Random.Range(-22000, 14000);
            currentPlanet.GetComponent<MKS>()._height = h;
            currentPlanet.GetComponent<MKS>()._longitude = lo;
            currentPlanet.GetComponent<MKS>()._latitude = la;
            currentPlanet.GetComponent<MKS>()._startForce = force;
            currentPlanet.GetComponent<MKS>().ConvertFromMeridianToVector3();
            currentPlanet.GetComponent<MKS>().RunMKS();
            timer = 0;
        }

        timer += Time.deltaTime;
    }
    
    private float looper(float x, float step, float min, float max)
    {
        x += step;
        if (x > max)
            return min;
        return x;
    }

   
}
