using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;


public class ItemDataBase : MonoBehaviour {

	private List<Item> database = new List<Item>();
	private JsonData itemData;

	// Use this for initialization
	void Start ()
	{

			itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/Scripts/Items.json"));
			ConstructItemDatabase();

			Debug.Log(FetchItemByID(0).Description);
	}

	public Item FetchItemByID(int id)
	{
			for(int i = 0 ; i < database.Count ; i++)
			{
				if(database[i].ID == id)
					return database[i];
			}
			return null;

	}

	void ConstructItemDatabase()
	{

			for(int i = 0; i < itemData.Count; i++)
			{
				database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["amount"], (int)itemData[i]["value"],
				(int)itemData[i]["stats"]["health"], (int)itemData[i]["stats"]["mana"], itemData[i]["description"].ToString(), itemData[i]["slug"].ToString(),
				(bool)itemData[i]["stackable"], (bool)itemData[i]["usable"], (bool)itemData[i]["equippable"], (int)itemData[i]["stats"]["attack"], (int)itemData[i]["stats"]["defense"], (bool)itemData[i]["weapon"], (bool)itemData[i]["armor"]));
			}

	}

}

public class Item
{
		public int ID { get; set; }
		public string Title { get; set; }
		public int Amount{ get; set; }
		public int Value { get; set; }
		public int Health { get; set; }
		public int Mana { get; set; }
		public string Description { get; set; }
		public string Slug { get; set; }
		public bool Stackable { get; set; }
		public bool Usable { get; set; }
		public bool Equippable { get; set; }
		public int Attack { get; set; }
		public int Defense { get; set; }
		public bool Weapon { get; set; }
		public bool Armor{ get; set; }

		public Sprite Sprite { get; set; }

		public Item(int id, string title, int amount, int value, int health, int mana, string description, string slug, bool stackable, bool usable, bool equippable,
		int attack, int defense, bool weapon, bool armor)
		{
				this.ID = id;
				this.Title = title;
				this.Amount = amount;
				this.Value = value;
				this.Health = health;
				this.Mana = mana;
				this.Description = description;
				this.Slug = slug;
				this.Stackable = stackable;
				this.Usable = usable;
				this.Equippable = equippable;
				this.Attack = attack;
				this.Defense = defense;
				this.Weapon = weapon;
				this.Armor = armor;
				this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
		}

		public Item(int id, int amount, string title, int value)
		{
			this.ID = id;
			this.Amount = amount;
			this.Title = title;
			this.Value = value;
		}

		public Item()
		{

			this.ID = -1;

		}


}
