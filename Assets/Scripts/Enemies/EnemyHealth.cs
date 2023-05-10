using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> onEnemyKilled;
    public static Action<Enemy> onEnemyHit;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;
    [SerializeField] private float initialHealth = 50f;
    [SerializeField] private float maxHealth = 50f;

    public float CurrentHealth { get; set; }

    private Image _healthBar;
    private Enemy _enemy;

    private void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;

        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DealDamage(5f);
        }
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);
    }

    public void DealDamage(float damageReceived)
    {
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            onEnemyKilled?.Invoke(_enemy); // Adicionado chamada ao evento onEnemyKilled
        }
        else
        {
            onEnemyHit?.Invoke(_enemy);
        }
    }

    private void CreateHealthBar()
    {
        GameObject healthBarObject = Instantiate(healthBarPrefab, barPosition);
        _healthBar = healthBarObject.GetComponent<Image>();
    }

    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
    }
}
