﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public bool isAi;
    public float m_StartingHealth = 100f;          
    public float flashRedTime = 0.1f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;

    public bool isinvincible = false;


    private AudioSource m_ExplosionAudio;          
    private ParticleSystem m_ExplosionParticles;   
    private float m_CurrentHealth;  
    private bool m_Dead;            


    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }
    public void add(float amount)
    {
        m_CurrentHealth += amount;
        if(m_CurrentHealth > m_StartingHealth)
        {
            m_CurrentHealth = m_StartingHealth;
        }
        SetHealthUI();
    }
    public void TakeDamage(float amount,string attacker)
    {
        if (isinvincible == false)
        {
            // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
            m_CurrentHealth -= amount;

            if (!isAi)
            {
                Camera.main.GetComponent<Animator>().SetTrigger("Shake");
                GameManager.instance.flash();
            }

            SetHealthUI();
            if (m_CurrentHealth <= 0f && !m_Dead)
            {
                if (!isAi)
                {
                    GameManager.instance.fail();
                }
                OnDeath();
                print(attacker);
                if (attacker.Equals("Player"))
                    GameManager.instance.hitToDeath();
            }
        }
    }


    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        m_Slider.value = m_CurrentHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }

    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        gameObject.SetActive(false);
    }
}