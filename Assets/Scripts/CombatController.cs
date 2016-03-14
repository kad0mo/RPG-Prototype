using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatController : MonoBehaviour {

public GameObject[] enemyLocations;
public GameObject[] characters;
public GameObject[] array;
public GameObject closest;
public GameObject activeIcon;
public float shiftSpeed;
public CanvasGroup _canvasGroup;
Vector3 attackLine;
public bool locked;
public bool canAttack;
public bool target;
public bool attackTrigger;

private float moveVertical;

public Animation anim;
public Transform enemytrans;

public bool setTimer;
public bool afterDeathTimer;
public float attackTimer;
public float attackTime;

public Vector3 combatSpot;

GameObject enemyPanel;
GameObject enemy;
GameObject enemy2;
GameObject enemyName;
GameObject enemyName2;
Text setName;
Text setName2;
public Slider turnBar;
GameObject _turnBar;
GameObject _turnBar2;

CharController control;
EnemyCombat enemyCombat;

EnemyCombat enemyTurn1;
EnemyCombat enemyTurn2;
public bool enemy1canAttack;
public bool enemy2canAttack;

public float actionTime;
public float actionTimer;
public bool setActionTimer;
public bool Combat;

public bool enemyAttackSession;
public bool enemyAttacking;
public bool sendAttackMessage;
public bool playerAttacking;

public bool playerTurnActive;

public GameObject combatPanel;
public float playerDistance;
public float playerActiveDistance;

StatController statControl;
public int attackPower;

public int totalBattleExp;

Characters character;
GameObject chara;

public float gridX = 1f;
public float gridY = 1f;
public float spacing = 4f;


	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animation>();
		combatPanel = GameObject.Find("Combat Panel");
		combatPanel.SetActive(false);
		_turnBar = GameObject.Find("TurnBar");
		_turnBar.SetActive(false);

		_turnBar2 = GameObject.Find("TurnBar2");
		_turnBar2.SetActive(false);

		control = this.GetComponent<CharController>();

		chara = GameObject.Find("Characters");
		character = chara.GetComponent<Characters>();

		playerAttacking = false;

		totalBattleExp = 0;

		Combat = false;
		playerTurnActive = false;
		setTimer = false;
		afterDeathTimer = false;

		enemy1canAttack = false;
		enemy2canAttack = false;

		locked = false;
		canAttack = false;
		target = false;
		attackTrigger = false;
		GetComponent<Animation>()["Armature|Swordattack_01"].layer = 2;
		GetComponent<Animation>()["Armature|Swordattack_02"].layer = 2;

		enemyAttackSession = false;
		enemyAttacking = false;
		sendAttackMessage = false;

		array = GameObject.FindGameObjectsWithTag("enemy");
		GetInactiveInRadius();

		statControl = this.GetComponent<StatController>();

		//VAIHDOIN VÄLIAIKAISESTI TÄN statControl.attackPower; --> 10 !! HHUOOM
		//VAIHDOIN VÄLIAIKAISESTI TÄN statControl.attackPower; --> 10 !! HHUOOM

		attackPower = 10;


	}

	// Update is called once per frame
	void Update ()
	 {

		 GetActiveInRadius();


		 if(Combat)
	{
			 actionTiming(turnBar, character.characters[0].Spd);


		if(playerTurnActive)
		{

			if(locked)
			{
				combatPanel.SetActive(false);
				attackSession();
				Debug.Log("Attacking");
			}
			if(target)
			{
				findNextEnemy();
			}

			if(Input.GetButtonDown("Attack") && canAttack)
			{
				attackTargeting();
			}

			/*

			if(enemyAttackSession && !playerAttacking)
			{

				if(sendAttackMessage)
				{
					sendAttackMessage = false;

					enemyAttacking = true;
					Debug.Log("Moi");
					enemyCombat.attackSession();
				}

			}

			*/

			startAttackTime();
			enemyDeathTimer();




		}

		if(enemyTurn1.checkIfReady() && enemyTurn1 != null)
		{

				enemy1canAttack = true;
		} else
		{

		}
		if(enemyTurn2.checkIfReady() && enemyTurn2 != null)
		{
			enemy2canAttack = true;


		}
		else
		{


		}

		if(!playerTurnActive)
		{
			if(enemy1canAttack && !enemy2canAttack)
			{

					enemyTurn1.startAttack();
					StartCoroutine(MyMethod());

					enemy1canAttack = false;
			}
			if(!enemy1canAttack && enemy2canAttack)
			{

				enemyTurn2.startAttack();
				StartCoroutine(MyMethod());

				enemy2canAttack = false;
			}
			if(enemy1canAttack && enemy2canAttack)
			{

				enemyTurn1.startAttack();
				enemyTurn2.startAttack();

				enemy2canAttack = false;
				enemy1canAttack = false;
			}

		}

	}

	}

	IEnumerator MyMethod() {

  yield return new WaitForSeconds(3);

 }

	 void GetInactiveInRadius()
	 {
		 foreach (GameObject obj in array)
		 {
				obj.SetActive(false);
		 }
	}

	void GetActiveInRadius()
	{
		foreach (GameObject obj in array)
		{
			if(obj)
			{
				float objectdistance = Vector3.Distance(transform.position,obj.transform.position);



				if(objectdistance < playerActiveDistance)

				 obj.SetActive(true);
			}
		}
 }

	public void enemyAttackReady(string enemy)
	{
		if(!sendAttackMessage)
		{

			enemyAttackSession = true;
			sendAttackMessage = true;
		}

	}

	public void enemyAttackDone()
	{
		enemyAttacking = false;
		enemyAttackSession = false;

	}

	public void engageCombat ()
	{
		if(!Combat)
		{

			for(int i = 0 ; i < character.characters.Count ; i++)
			{
				if(character.characters[i].ID != 0)
				{


						Vector3 pos = new Vector3(this.transform.position.x + 5, this.transform.position.y , this.transform.position.z - 8);
						Instantiate(characters[i], pos, Quaternion.identity);


				}
			}

			FindClosestEnemy();
			enemytrans = closest.GetComponent<Transform>();
			transform.LookAt(enemytrans);



			actionTimer = actionTime;
			setActionTimer = true;
			Combat = true;
			totalBattleExp = 0;
			_turnBar.SetActive(true);
			_turnBar2.SetActive(true);
		}
}



	public void actionTiming(Slider slider, int speed)
	{
		if(setActionTimer)
		{
			actionTimer -= Time.deltaTime * speed;
			slider.value = actionTimer;
		}
		if(actionTimer <= 0 && !afterDeathTimer && !enemy1canAttack && !enemy2canAttack)
		{
			_turnBar.SetActive(false);
			combatPanel.SetActive(true);
			setActionTimer = false;
			playerTurnActive = true;

		}

	}


	public void getEnemyNames()
	{
		enemyPanel = GameObject.Find("Enemy Panel");
		enemy = enemyPanel.transform.FindChild("Enemy1").gameObject;
		enemy2 = enemyPanel.transform.FindChild("Enemy2").gameObject;
		enemyName = enemy.transform.FindChild("Name").gameObject;
		enemyName2 = enemy2.transform.FindChild("Name").gameObject;
		Debug.Log(enemyName);
		setName = enemyName.GetComponent<Text>();
		setName2 = enemyName2.GetComponent<Text>();

		if(enemyLocations.Length < 2) {

			setName.text = closest.ToString();
			enemy2.SetActive(false);
		}
	  else
		{
		setName.text = enemyLocations[0].ToString();
		setName2.text = enemyLocations[1].ToString();
		}

	}

	public void getEnemyTurns()
	{
		if(enemyLocations[0] == null)
		{

		}
		else
		{
		enemyTurn1 = enemyLocations[0].GetComponent<EnemyCombat>();
		}
		if(enemyLocations.Length < 2)
		{

		}
		else
		{
		enemyTurn2 = enemyLocations[1].GetComponent<EnemyCombat>();
		}


		Debug.Log(enemyTurn1, enemyTurn2);
	}

	public void getEnemyExp(int experience)
	{
		Debug.Log("adding" + experience + "exp");
		totalBattleExp += experience;
	}


	GameObject FindClosestEnemy ()
	{



			enemyLocations = GameObject.FindGameObjectsWithTag("enemy");
			float distance = Mathf.Infinity;

			if(enemyLocations.Length == 0)
			{


					Combat = false;
					playerAttacking = false;
					playerTurnActive = false;
					enemyAttackSession = false;
					combatPanel.SetActive(false);
					enemyPanel.SetActive(false);
					_turnBar.SetActive(false);
					control.endCombat();
					statControl.newExperience(totalBattleExp, 0);



			}

			Vector3 position = transform.position;

			foreach(GameObject go in enemyLocations)
			{
				 Vector3 diff = go.transform.position - position;
				 float curDistance = diff.sqrMagnitude;
						if (curDistance < distance)
						{
								closest = go;
								distance = curDistance;
						}

			}



			Debug.Log(enemyLocations.Length);
			Debug.Log(closest);
			getEnemyTurns();
			getEnemyNames();
			return closest;
	}

	public void findNextEnemy()
	{
		/*
		moveVertical = Input.GetAxisRaw ("Vertical");
		*/
		enemytrans = closest.GetComponent<Transform>();
		transform.LookAt(enemytrans);


		if(Input.GetButtonUp("NextTarget"))
		{

				for(int i = 0 ; i < enemyLocations.Length ; i++)
				{

						if(enemyLocations[i] != closest)
						{

								Debug.Log("Found next enemy:" + enemyLocations[i]);
								closest = enemyLocations[i];
								activeIcon.transform.position = new Vector3(closest.transform.position.x, closest.transform.position.y + 5, closest.transform.position.z);
								break;
						}

				}
		}
	}

	public void findAfterDeath()
	{
		/*
		moveVertical = Input.GetAxisRaw ("Vertical");
		*/
		enemytrans = closest.GetComponent<Transform>();
		transform.LookAt(enemytrans);


				for(int i = 0 ; i < enemyLocations.Length ; i++)
				{

						if(enemyLocations[i] != closest)
						{

								Debug.Log("Found next enemy:" + enemyLocations[i]);
								closest = enemyLocations[i];
								activeIcon.transform.position = new Vector3(closest.transform.position.x, closest.transform.position.y + 5, closest.transform.position.z);
								break;
						}

				}

	}

	public void enemyDies ()
	{

			attackTimer = attackTime;
			setTimer = true;
			locked = false;
			canAttack = false;
			target = false;
			attackTrigger = false;
			enemyLocations = null;
			activeIcon.SetActive(false);
			combatPanel.SetActive(false);


			afterDeathTimer = true;

	}

	public void enemyDeathTimer()
	{
		if(afterDeathTimer)
		{

			if(attackTimer < 0)
			{
				setTimer = false;
				afterDeathTimer = false;
				transform.position = combatSpot;
				FindClosestEnemy();
				playerTurnActive = false;
				actionTimer = actionTime;
				setActionTimer = true;
				_turnBar.SetActive(true);
				_turnBar2.SetActive(true);
				Debug.Log(enemyLocations);
				//Sitten kun pelihahmoilla on ajastimet niin tätä pitää muuttaa.
				_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
			}

		}

	}



	public void enemyCancel()
	{
		enemyLocations = null;
		_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
		activeIcon.SetActive(false);
		locked = false;
		canAttack = false;
		target = false;
	}

	public void startAttackTime()
	{
		if(setTimer)
		{
			attackTimer -= Time.deltaTime;
		}

	}

	public void attackSession()
	{



		if(locked)
		{
			playerAttacking = true;
			enemyCombat = closest.GetComponent<EnemyCombat>();


			float distance = Vector3.Distance(transform.position, closest.transform.position);

	    if(closest != null)
	    {
				if(distance > 3.5)
				{

	      attackLine = new Vector3 (0.0f, 0.0f, closest.transform.position.z);
	      transform.Translate(attackLine * shiftSpeed * Time.deltaTime);



				}
				if(distance < 3.5)
				{



					if(!attackTrigger )
					{
						attackTimer = attackTime;
						setTimer = true;

						attackTrigger = true;
						enemyCombat.takeDamage(attackPower);
						anim.CrossFade("Armature|Swordattack_01");
						anim.CrossFade("Armature|Swordattack_02");
						combatPanel.SetActive(false);

					}
					if(attackTimer < 0)
					{
						setTimer = false;
						playerTurnActive = false;
						transform.position = combatSpot;
						locked = false;
						canAttack = false;
						target = false;
						attackTrigger = false;
						activeIcon.SetActive(false);
						actionTimer = actionTime;
						setActionTimer = true;
						_turnBar.SetActive(true);
						_turnBar2.SetActive(true);
						FindClosestEnemy();

						Debug.Log(enemyLocations);
						//Sitten kun pelihahmoilla on ajastimet niin tätä pitää muuttaa.
						_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
						return;
					}

				}

	    }
		}
	}


	public void attackTargeting ()
	{
		if(Input.GetButtonDown("Attack") && target)
		{
			locked = true;
		}

		if(Input.GetButtonDown("Attack"))
		{
			Debug.Log("Now choose target");
			target = true;

		}



	}


	public void enemyTargeting()
	{
		if(target)
		{
			return;
		}
		FindClosestEnemy();

		 combatSpot = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
		_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;

		if (closest != null)
		{
		canAttack = true;
		activeIcon.SetActive(true);
		activeIcon.transform.position = new Vector3(closest.transform.position.x, closest.transform.position.y + 5, closest.transform.position.z);
		/*
		activeIcon.transform.position.x = closest.transform.position.x;
		activeIcon.transform.position.z = closest.transform.position.z;
		*/
		} else
		{
			activeIcon.SetActive(false);
			canAttack = false;
		}
	}

}
