using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour {

	public Transform Target;
	public bool locked = false;
	public bool enterCombat = false;
	public float moveSpeed;
	public Vector3 attackLine;
	public int enemyHealth;

	public float distance;

	public Animation anim;

	public	CharController controls;
	public CombatController combat;
	public GameObject player;
  public StatController statControl;

	public float actionTime;
	public float actionTimer;
	public bool setActionTimer;
	public bool canAttack;
	public bool canAttackLock;
	public bool startAttackSession;

	public float attackTimer;
	public float attackTime;
	public bool setTimer;
	public Vector3 combatSpot;

	public bool allowAttack;




	// Use this for initialization
	void Start () {

		GetComponent<Animation>()["Armature|Combat_Guard"].layer = 2;
		GetComponent<Animation>()["Armature|Blerf"].layer = 1;
		GetComponent<Animation>()["Armature|Combat_Idle"].layer = 1;
		anim = GetComponent<Animation>();

		canAttack = false;
		canAttackLock = false;
		startAttackSession = false;
		setTimer = false;
		setActionTimer = false;
	 	player = GameObject.Find("Player");
		combat = player.GetComponent<CombatController>();
		statControl = player.GetComponent<StatController>();

		allowAttack = false;
	}

	// Update is called once per frame
	void Update ()
	{

		distance = Vector3.Distance(transform.position, Target.transform.position);

		if(locked) {

				Combat();
				AttackSession();

		} else {

				anim.CrossFade("Armature|Idle");

		}

		startAttackTime();

	}

  	public void attackSession()
		{

			startAttackSession = true;
			this.attackTimer = this.attackTime;
			setTimer = true;
			combatSpot = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		}

		void OnTriggerEnter(Collider other)
		{
			if(other.gameObject.CompareTag("Player"))
			{
				locked = true;
				actionTimer = actionTime;
				setActionTimer = true;
				controls.engageCombat();
				combat.engageCombat();

			}
		}

		public void takeDamage(int damage)
		{

			enemyHealth = enemyHealth - damage;
			anim.CrossFade("Armature|Combat_Guard");

			if(enemyHealth < 1)
			{
				combat.enemyDies();
				combat.getEnemyExp(50);
				Debug.Log("Enemy Dies");
				Destroy(gameObject);

			}

		}

		public void startAttackTime()
		{

			if(setTimer)
			{

				attackTimer -= Time.deltaTime;
			}

		}

		public bool checkIfReady()
		{

			if(canAttack && !canAttackLock)
			{
				canAttackLock = true;

				return true;

			} else
			{
				return false;

			}


		}

		public void startAttack()
		{

			allowAttack = true;
			attackTimer = attackTime;
			setTimer = true;
			combatSpot = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		}

		public void AttackSession()
		{
			if(allowAttack)
			{


				Vector3 delta = Target.transform.position - transform.position;
				delta.Normalize();

				if(distance > 3 && attackTimer < 2.0)
				{

						Debug.Log(this + "is Attacking..");
						moveSpeed = 20;
						transform.position = transform.position + (delta * moveSpeed * Time.deltaTime);
						anim.CrossFade("Armature|Combat_Idle");

				}
				if(attackTimer < 0)
				{

						Debug.Log("Attack is over");
						transform.position = combatSpot;
						allowAttack = false;
						canAttack = false;
						setTimer = false;
						canAttackLock = false;
						actionTimer = actionTime;
						setActionTimer = true;
						statControl.takeDamage(8, 0);

				}
			}
		}

		void Combat ()
		{

			  transform.LookAt(Target);

			  Vector3 delta = Target.transform.position - transform.position;
				delta.Normalize();

				if(setActionTimer)
				{
					actionTimer -= Time.deltaTime;

				}
				if(actionTimer <= 0 && !canAttack)
				{
					Debug.Log(this + "Can attack");
					canAttack = true;
					combat.enemyAttackReady(this.ToString());
					setActionTimer = false;
				}

				if(distance > 13)
				{
						moveSpeed = 15;
						transform.position = transform.position + (delta * moveSpeed * Time.deltaTime);
						anim.CrossFade("Armature|Walking_block");
				}
				if(distance < 13)
				{

						moveSpeed = 0;
						anim.CrossFade("Armature|Combat_Idle");
				}

				/*
				if(distance > 3 && startAttackSession)
				{

						Debug.Log(this + "is Attacking..");
						moveSpeed = 20;
						transform.position = transform.position + (delta * moveSpeed * Time.deltaTime);
						anim.CrossFade("Armature|Combat_Idle");
				}
				if(attackTimer < 0 && startAttackSession)
				{
					startAttackSession = false;
					setTimer = false;
					canAttack = false;
					transform.position = combatSpot;
					actionTimer = actionTime;
					setActionTimer = true;
					Debug.Log("Return to position");
					combat.enemyAttackDone();
					return;

				}
				*/

		}

}
