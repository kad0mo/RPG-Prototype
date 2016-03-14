using UnityEngine;
using System.Collections;

public class ItemData : MonoBehaviour {

		public Item item;
		public int amount;
		public int health;
		public int mana;
		public bool usable;
		public bool equippable;
		public bool weapon;
		public bool armor;
		public bool cloth;
		public bool ring;
		public int attack;
		public int defense;
		public int id;

		public bool reducable;



	// Use this for initialization
	void Start () {

		reducable = false;


	}

	// Update is called once per frame
	void Update () {


	}

	public void allowReduce()
	{
		reducable = true;
	}

	public void cancelReduce()
	{
		reducable = false;
	}






}
