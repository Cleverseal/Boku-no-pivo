using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpManager : MonoBehaviour {
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public delegate void PlayerHpManagerHandler();
    public event PlayerHpManagerHandler PlayerHasDied;

    public void GetDamage(uint damage)
    {
        currentHealth -= (int)damage;
        if (currentHealth <= 0) PlayerHasDied();
    }

    public void GetHeal(int heal)
    {
        currentHealth = (currentHealth + heal <= maxHealth) ? currentHealth + heal : maxHealth;
    }

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
    }
}
