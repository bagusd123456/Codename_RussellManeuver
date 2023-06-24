using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public enum CharacterState
    {
        Idle,
        Walk,
        Run,
        Jump,
        Attack,
        Hit,
        Die
    }

    public CharacterState m_State;
    public CharacterState State
    {
        get { return m_State; }
        set { m_State = value; }
    }

    public int maxHealth = 100;
    public int currentHealth;

    public void Start()
    {
        currentHealth = maxHealth;
        if (currentHealth <= 0)
            Die();
    }

    public virtual void TakeDamage(int amount)
    {
        // Kurangi health character
        currentHealth -= amount;
        if(currentHealth <0) // jika hasilnya negatif, ubah jadi 0
            currentHealth = 0;

        // Cek apakah health sudah 0 atau kurang
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        State = CharacterState.Die;
        Debug.Log(gameObject.name + " has Died.");
    }
}
