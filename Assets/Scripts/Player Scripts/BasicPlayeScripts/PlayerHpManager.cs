using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public class PlayerHpManager : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;

        public event PlayerEventHandler PlayerDied;

        public void GetDamage(uint damage)
        {
            currentHealth -= (int)damage;
            if (currentHealth <= 0) PlayerDied();
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
}