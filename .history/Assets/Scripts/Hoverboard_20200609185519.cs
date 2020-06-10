﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hoverboard : MonoBehaviour
{
  public float m_MoveForce = 5f;
  public float m_TorqueForce = 5f;
  public float m_HoverHeight = 4f;
  // The force applied per unit of distance below the desired height.
  public float m_HoverForce = 5f;
  // The amount that the lifting force is reduced per unit of upward speed.
  // This damping tends to stop the object from bouncing after passing over
  // something.
  public float m_HoverDamp = 0.5f;
  public Rigidbody m_RigidBody;
  private GameObject[] m_HoverboardPoints;

  public void Move(float horizontal, float vertical)
  {
    m_RigidBody.AddForce(vertical * m_MoveForce * Camera.main.transform.forward);
    m_RigidBody.AddTorque(horizontal * m_TorqueForce * Vector3.up);
  }
  private void Awake()
  {
    m_RigidBody = GetComponent<Rigidbody>();
    m_HoverboardPoints = GameObject.FindGameObjectsWithTag("HoverboardPoint");

  }

  // Update is called once per frame
  private void FixedUpdate()
  {
    Debug.Log("RigidBodyCenterOfMass" + m_RigidBody.centerOfMass);
    RaycastHit hit;

    foreach (GameObject point in m_HoverboardPoints)
    {
      Ray downRay = new Ray(point.transform.position, Vector3.down);
      Debug.DrawRay(point.transform.position, Vector3.down, Color.red);
      // Raycast downward
      if (Physics.Raycast(downRay, out hit))
      {
        float hoverError = m_HoverHeight - hit.distance;
        if (hoverError > 0)
        {
          // Subtract the damping from the lifting force and apply it to
          // the rigidbody.
          float upwardSpeed = m_RigidBody.velocity.y;
          float lift1 = m_HoverForce * Mathf.Pow((1f - (hit.distance / m_HoverHeight)), 1.7f);
          float lift2 = hoverError * m_HoverForce - upwardSpeed * m_HoverDamp;
          Debug.Log("lift1" + lift1 * Vector3.up);
          Debug.Log("lift2" + lift2 * Vector3.up);
          // todo
          // drift
          // center of mass
          // bounce factor

          m_RigidBody.AddForceAtPosition(lift2 * Vector3.up, point.transform.position);
        }
      }
    }
  }
}
