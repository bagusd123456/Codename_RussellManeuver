using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    public static Action OnPlayerAttack;
    [Header("Player Attack")]
    [SerializeField]
    private float attackRadius = 1f;
    private float attackSpeed = 1f;
    private float lastAttackTime;

    private void OnEnable()
    {
        OnPlayerAttack += AttackExecute;
    }

    private void OnDisable()
    {
        OnPlayerAttack -= AttackExecute;
    }

    // Start is called before the first frame update
    new void Start()
    {
        State = CharacterState.Idle;
        base.Start();
    }

    private void Update()
    {
        if (lastAttackTime > 0)
            lastAttackTime -= Time.deltaTime;
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        Debug.Log($"Player: {gameObject.name} Taken {amount} Damage");
    }

    public override void Die()
    {
        base.Die();
        GameManager.OnStateLoseCondition?.Invoke();
    }

    public void AttackExecute()
    {
        Attack(1);
    }

    public void Attack(int amount)
    {
        if (lastAttackTime > 0) return;

        Debug.Log($"Player: {gameObject.name} Attacking");
        lastAttackTime = attackSpeed;
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius);

        foreach (Collider item in colliders)
        {
            BaseCharacter character;
            item.TryGetComponent(out character);

            if (character != null && character != this)
            {
                character.TakeDamage(1);
            }

            if(item.GetComponent<Box>() != null)
            {
                item.GetComponent<Box>().Blow();
            }
        }
    }
}
