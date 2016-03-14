using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Halla : MonoBehaviour {

	CharController charControl;
	CombatController combat;
	Characters chara;
	GameObject characters;
	GameObject player;
	public Transform enemytrans;
	public Animation anim;

	public Slider turnBar;
	public GameObject _turnBar;

	public bool setUp = false;

	public float actionTimer;

	// Use this for initialization
	void Awake () {



		player = GameObject.Find("Player");
		charControl = player.GetComponent<CharController>();
		combat = player.GetComponent<CombatController>();
		anim = GetComponent<Animation>();

		characters = GameObject.Find("Characters");
		chara = characters.GetComponent<Characters>();





	}

	// Update is called once per frame
	void Update () {

		if(charControl.Combat)
		{
			CombatControls();
			actionTiming(turnBar, chara.characters[1].Spd);
		}

	}

	public void CombatControls ()
	{

		anim.CrossFade("Armature|Swordidle", 0.2f);

		if(!setUp)
		{

		_turnBar = GameObject.Find("TurnBar2");
		turnBar = _turnBar.GetComponent<Slider>();
		actionTimer = 100;

		enemytrans = combat.closest.GetComponent<Transform>();
		transform.LookAt(enemytrans);
		setUp = true;
		}

		/*
		FindClosestEnemy();
		enemyTargeting();
		*/
	}

	public void actionTiming(Slider slider, int speed)
	{
		if(combat.setActionTimer)
		{
			actionTimer -= Time.deltaTime * speed;
			slider.value = actionTimer;
		}
		if(actionTimer <= 0 && !combat.afterDeathTimer && !combat.enemy1canAttack && !combat.enemy2canAttack)
		{
			_turnBar.SetActive(false);
			combat.combatPanel.SetActive(true);
			combat.setActionTimer = false;
			combat.playerTurnActive = true;

		}

	}
}
