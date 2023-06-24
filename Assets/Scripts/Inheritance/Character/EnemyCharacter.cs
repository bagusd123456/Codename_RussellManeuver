using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : BaseCharacter
{
    [Space]
    public Patrol patrol;
    public AnimationHandler animationHandler;
    // Start is called before the first frame update
    new void Start()
    {
        State = CharacterState.Idle;
        base.Start();
    }

    private void Update()
    {
        if(animationHandler != null)
            animationHandler.moveSpeed = patrol.currentSpeed;
    }

    public override void Die()
    {
        base.Die();

        patrol.active = false;
        Destroy(gameObject,1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<PlayerCharacter>().TakeDamage(1);
        }
    }
}
