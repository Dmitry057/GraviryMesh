using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MKS : GravitySimulation
{


    public GameObject particles;
    public float startSpeed = 36f;
    public bool isDrawing = true;
    public bool isRunning = false;
    public TextMeshProUGUI text;
    
    private Color _green = new Color (0,0.8f, 0.4f, 0.2f); 
    private Color _red = new Color (0.8f, 0, 0.4f, 0.2f);
    
    public float _height = 5f;
    public float _longitude = 0;
    public float _latitude = 90;

    public void GetXForce(string X) => _startForce.x = float.TryParse(X, out _startForce.x) ? _startForce.x : 0;
    public void GetYForce(string Y) => _startForce.y = float.TryParse(Y, out _startForce.y) ? _startForce.y : 0;
    public void GetZForce(string Z) => _startForce.z = float.TryParse(Z, out _startForce.z) ? _startForce.z : 0;
    
    public void GetHeight(string height)
    {
        _height = float.TryParse(height, out _height) ? float.Parse(height) : 0;
        ConvertFromMeridianToVector3();
    }
    

    public void GetLongitude(string longitude)
    {
        _longitude = float.TryParse(longitude, out _longitude) ? float.Parse(longitude) : 0;
        ConvertFromMeridianToVector3();
    }
    
    public void GetLatitude(string latitude)
    {
        _latitude = float.TryParse(latitude, out _latitude) ? float.Parse(latitude) : 0;
        ConvertFromMeridianToVector3();
    }
    
    public override void Awake()
    {
        base.Awake();
        
        ConvertFromMeridianToVector3();
        
        rb.isKinematic = true;
    }
    public void ConvertFromMeridianToVector3()
    {
        _height = Math.Clamp(_height, 3f, 1000f);
        // convert latitude and longitude to vector3
        float x = _height * Mathf.Cos(_latitude * Mathf.Deg2Rad) * Mathf.Cos(_longitude * Mathf.Deg2Rad);
        float y = _height * Mathf.Sin(_latitude * Mathf.Deg2Rad);
        float z = _height * Mathf.Cos(_latitude * Mathf.Deg2Rad) * Mathf.Sin(_longitude * Mathf.Deg2Rad);
        transform.position = new Vector3(x, y, z);
    }

    public void SetTime(string time)
    {
        float t = Time.timeScale;
        Time.timeScale = float.TryParse(time,out t) ? float.Parse(time) : 1;
    }

    public void RunMKS()
    {   
        isRunning = true;
        rb.isKinematic = false;
        rb.AddForce(_startForce);
        
    }
  
    public void DrawLines(GravitySimulation attractedObj, float dist)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (attractedObj.transform.position - transform.position), out hit, 10))
        {
           
            Debug.DrawLine(transform.position, hit.point, _green*5/dist);
          
            Debug.DrawLine(hit.point,attractedObj.transform.position, _red*5/dist);
                

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        particles.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    

    public override void AttractUpdately()
    {
        if (isRunning)
        {
            base.AttractUpdately();
        }
        
    }

    public override void Attract(GravitySimulation attractedObj)
    {
        
        base.Attract(attractedObj);
        var distance = Vector3.Distance(transform.position, attractedObj.transform.position);
        if (distance > 40)
        {
            StartCoroutine(Die());
        }
        if (isDrawing)
            DrawLines(attractedObj, distance);
    }
}
