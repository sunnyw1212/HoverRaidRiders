﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace HoverRaidRiders
{
  public class PlayerHoverboardRiderController : MonoBehaviour
  {
    private Hoverboard m_Hoverboard;
    private GameObject m_RiderGameObject; // maybe we can delete later
    private Rider m_Rider;

    void Awake()
    {
      m_Hoverboard = GetComponent<Hoverboard>();
      m_RiderGameObject = GameObject.FindGameObjectWithTag("Rider");
      m_Rider = m_RiderGameObject.GetComponent<Rider>();
    }

    // Update is called once per frame
    void Update()
    {
      if (CrossPlatformInputManager.GetButtonDown("Jump"))
      {
        print("jump");
        m_Hoverboard.Jump();
      }
      m_Rider.Aim(Input.mousePosition);
      print("Mouse " + Input.mousePosition);
    }
    void FixedUpdate()
    {
      float vertical = CrossPlatformInputManager.GetAxis("Vertical");
      float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");

      m_Hoverboard.Move(horizontal, vertical);


    }
  }
}