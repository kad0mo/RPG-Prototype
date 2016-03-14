using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatController : MonoBehaviour {

	public int currentHealth;
  private int health;
	public int currentMana;
	private int mana;

	public int attackPower;
	public int armor;

	public Slider healthSlider;
	public Slider manaSlider;

	public Slider profileManaSlider;
	public Slider profileHealthSlider;

	public Text curHealth;
	public Text totalHealth;
	public Text curMana;
	public Text totalMana;

	public Text profileCurrentExp;
	public Text profileReqExp;

	public Text level;

	public int currentExperience;
	public int currentLevel;

	public int levelAmount = 100;

	public Animation anima;

	public GameObject Characters;
	Characters character;

	characterData charData;
	private int id;

	// Use this for initialization

	void Awake()
	{



	}

	void Start () {

	Characters = GameObject.Find("Characters");
	character = Characters.GetComponent<Characters>();
  charData = this.GetComponent<characterData>();
	id = charData.id;
	Debug.Log("This characters id is " + id);


	anima = GetComponent<Animation>();



	this.curHealth.text = character.characters[id].CurHp.ToString();
	this.totalHealth.text = character.characters[id].Hp.ToString();
	this.curMana.text = character.characters[id].CurMana.ToString();
	this.totalMana.text = character.characters[id].Mana.ToString();

	/*
	profileCurrentExp.text = "EXP : " + currentExperience.ToString();
	profileReqExp.text = "REQ : " + levelAmount.ToString();
	level.text = "LEVEL " + currentLevel.ToString();
	*/


	this.healthSlider.value = character.characters[id].CurHp;
	this.manaSlider.value = character.characters[id].CurMana;

	this.profileManaSlider.value = character.characters[id].CurMana;
	this.profileHealthSlider.value = character.characters[id].CurHp;


	Characters = GameObject.Find("Characters");
	character = Characters.GetComponent<Characters>();



	}

	// Update is called once per frame
	void Update () {


		this.healthSlider.value = character.characters[id].CurHp;
		this.manaSlider.value = character.characters[id].CurMana;


		if(character.characters[id].CurHp > character.characters[id].Hp)
		{
			character.characters[id].CurHp = character.characters[id].Hp;

		}


		profileManaSlider.value = character.characters[id].CurMana;
		profileHealthSlider.value = character.characters[id].CurHp;

		curHealth.text = character.characters[id].CurHp.ToString();
		totalHealth.text = character.characters[id].Hp.ToString();
		curMana.text = character.characters[id].CurMana.ToString();
		totalMana.text = character.characters[id].Mana.ToString();

		/*
		profileCurrentExp.text = "EXP : " + currentExperience.ToString();
		profileReqExp.text = "REQ : " + levelAmount.ToString();
		level.text = "LEVEL " + currentLevel.ToString();
		*/

	}

	public void updateSliders()
	{
		this.healthSlider.value = character.characters[id].CurHp;
		this.manaSlider.value = character.characters[id].CurMana;
		profileManaSlider.value = character.characters[id].CurMana;
		profileHealthSlider.value = character.characters[id].CurHp;
	}

	public void takeDamage(int damage, int id)
	{

		currentHealth = currentHealth - damage;
		anima.CrossFade("Armature|Combat_Guard");

		if(currentHealth == 0)
		{

			Debug.Log("Kuolit.");
			Destroy(gameObject);

		}

	}

		public void newExperience(int exp, int id)
	{
		 currentExperience += exp;
		 if(currentExperience >= nextLvlExp(currentLevel))
		 {
				currentLevel += 1;
				Debug.Log(currentLevel);
				levelAmount = levelAmount * 2 * currentLevel;
				getNewStats();
		 }
	}

		public int nextLvlExp(int currentLevel)
		{
			return levelAmount;
		}

	  public void	getNewStats()
	 {
			 getHealth(currentLevel);
			 getMana(currentLevel);
	 }

	 public void getMana(int lvl)
	 {
		 mana = mana + lvl*lvl*3;

		 curMana.text = currentMana.ToString();
		 totalMana.text = mana.ToString();
	 }

	 public void getHealth(int lvl)
	 {
		 int addAmount = (lvl*lvl*2);

		 	health = health + addAmount;

			curHealth.text = currentHealth.ToString();
			totalHealth.text = health.ToString();
	 }

	 public void addStatsFromItems(int gainedhealth, int gainedmana)
	 {
		 currentHealth += gainedhealth;
		 currentMana += gainedmana;

	 }
}
