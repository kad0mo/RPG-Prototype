using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;


public class characterDataBase : MonoBehaviour {

	private List<Character> charDatabase = new List<Character>();
	private JsonData characterData;

	// Use this for initialization
	void Start ()
	{

			characterData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/Characters.json"));
			ConstructCharacterDatabase();


	}

	public Character FetchCharacterByID(int id)
	{
			for(int i = 0 ; i < charDatabase.Count ; i++)
			{
				if(charDatabase[i].ID == id)
					return charDatabase[i];
			}
			return null;

	}

	void ConstructCharacterDatabase()
	{
			/*
			Debug.Log(characterData.Count);
			*/
			for(int i = 0; i < characterData.Count; i++)
			{
				charDatabase.Add(new Character((int)characterData[i]["id"], characterData[i]["name"].ToString(), (int)characterData[i]["level"], (int)characterData[i]["exp"],
				(int)characterData[i]["stats"]["hp"], (int)characterData[i]["stats"]["curhp"], (int)characterData[i]["stats"]["mana"], (int)characterData[i]["stats"]["curmana"],
				(int)characterData[i]["stats"]["str"], (int)characterData[i]["stats"]["mag"],
			  (int)characterData[i]["stats"]["spd"], (int)characterData[i]["stats"]["attack"], (int)characterData[i]["equip"]["weapon"], (int)characterData[i]["equip"]["armor"], (int)characterData[i]["equip"]["clothes"],
				 (int)characterData[i]["equip"]["ring"] , characterData[i]["slug"].ToString()));
			}

	}

}

public class Character
{
		public int ID { get; set; }
		public string Name { get; set; }
		public int Level { get; set; }
		public int Exp { get; set; }
		public int Hp { get; set; }
		public int CurHp { get; set; }
		public int Mana { get; set; }
		public int CurMana { get; set; }
		public int Str { get; set; }
		public int Mag { get; set; }
		public int Spd { get; set; }
		public int Attack { get; set; }
		public int Weapon { get; set; }
		public int Armor { get; set; }
		public int Clothes { get; set; }
		public int Ring { get; set; }
		public string Slug { get; set; }
		public Sprite Sprite { get; set; }

		public Character(int id, string name, int level, int exp, int hp, int curhp, int mana, int curmana, int str, int mag, int spd, int attack, int weapon,
		int armor, int clothes, int ring, string slug)
		{
				this.ID = id;
				this.Name = name;
				this.Level = level;
				this.Exp = exp;
				this.Hp = hp;
				this.CurHp = curhp;
				this.Mana = mana;
				this.CurMana = curmana;
				this.Str = str;
				this.Mag = mag;
				this.Spd = spd;
				this.Attack = attack;
				this.Weapon = weapon;
				this.Armor = armor;
				this.Clothes = clothes;
				this.Ring = ring;

				this.Slug = slug;
				this.Sprite = Resources.Load<Sprite>("Sprites/Characters/" + slug);
		}

		public Character(int id, string name)
		{
			this.ID = id;
			this.Name = name;
		}

		public Character()
		{
			this.ID = -1;
		}


}
