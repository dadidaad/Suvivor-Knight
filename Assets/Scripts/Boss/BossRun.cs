using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
	public float speed = 10f;
	public float attackRange = 1f;
	public bool resetAttack = false;
	Transform player;
	Rigidbody2D rb;
    Boss boss;
	BossAttack attack;
	float timeCoolDown = 0f;
	BossHealth health;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
		attack = animator.GetComponent<BossAttack>();
		health = animator.GetComponent<BossHealth>();
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        timeCoolDown += Time.deltaTime;
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
		Vector2 newPos = Vector2.MoveTowards(rb.position, player.transform.position, speed * Time.fixedDeltaTime);
		rb.MovePosition(newPos);

		if (Vector2.Distance(player.position, rb.position) <= attackRange)
		{
            if (!resetAttack)
			{
                animator.SetTrigger("Attack");
				if (health.isEarn)
				{
					attack.EnragedAttack();
				}
				else
				{
					attack.Attack();
				}
            }
		}
		if(timeCoolDown >= 2f)
		{
			resetAttack = false;
			timeCoolDown = 0;
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.ResetTrigger("Attack");
		resetAttack = true;
	}
}
