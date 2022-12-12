using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraRotation : MonoBehaviour
{
   
   [Range(0.01f, 5f)]
   [SerializeField] private float _mouseSensitivity = 1f;
   
   [SerializeField] private float _distanceFromTarget = 3f;
   [SerializeField] private float _smoothTime = 0.3f;
   [Range(0.0001f, 2f)]
   [SerializeField] private float _drag = 0.1f;
   [SerializeField] private Vector2 _clampedDistance = new Vector2(0.5f, 4f);
   [SerializeField] private Transform _target;
   private bool inTrigger = false;
   private float _rotationY;
   private float _rotationX;
   

   private Vector3 _currentRotation;
   private Vector3 _smoothVelocity = Vector3.zero;


   public void Start()
   {
      Time.timeScale = 1f;
   }

   public void ReloadScene()
   {
      SceneManager.LoadScene(0);
   }
   private void Update()
   {
      if (Input.GetMouseButton(0))
      {
         float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
         float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;
         _rotationY += mouseX;
         _rotationX -= mouseY;
      }
      if(Input.GetMouseButton(1))
      {
         float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
         _distanceFromTarget = Mathf.Clamp(_distanceFromTarget - 0.06f * mouseX, _clampedDistance.x, _clampedDistance.y);
      }
      
      _rotationX = Mathf.Clamp(_rotationX, -90, 90);
      
      Vector3 nextRotation = new Vector3(_rotationX, _rotationY);
      
      _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
      transform.localEulerAngles = _currentRotation;
      
      transform.position =  Vector3.Lerp(transform.position, _target.position - transform.forward * _distanceFromTarget, (1/_drag)*Time.deltaTime);
   }
   
}
