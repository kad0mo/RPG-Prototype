using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {

	GameObject inventoryPanel;
	public GameObject slotPanel;
	GameObject stretchPanel;
	GameObject descriptionPanel;

	ItemDataBase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;
	public GameObject Player;
	int slotAmount;

	public int itemNumber;

	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	public bool choosingWeapon;
	public bool choosingArmor;

	// Use this for initialization
	void Start ()
	{

			Player = GameObject.Find("Player");

			database = GetComponent<ItemDataBase>();

			itemNumber = 0;
			slotAmount = 0;
			inventoryPanel = GameObject.Find("Inventory Panel");
			descriptionPanel = GameObject.Find("Description Panel");
			stretchPanel = GameObject.Find("StretchPanel");

			slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
			inventoryPanel.SetActive(false);
			choosingWeapon = false;
			choosingArmor = false;

			for(int i = 0 ; i < slotAmount ; i++)
			{
					items.Add(new Item());
					slots.Add(Instantiate(inventorySlot));
					slots[i].transform.SetParent(slotPanel.transform);
			}

			//Testaus-setti itemeitä.

			AddItem(0);
			AddItem(0);
			AddItem(0);
			AddItem(1);
			AddItem(2);
			AddItem(3);
			AddItem(3);
			AddItem(5);
			AddItem(4);
			AddItem(6);
			AddItem(7);


	}

	public void AddSlots(int slotAmount)
	{
			int current;

			for(int i = 0 ; i < slotAmount ; i++)
			{
					current = slots.Count;


					items.Add(new Item());
					slots.Add(Instantiate(inventorySlot));
					slots[current].transform.SetParent(slotPanel.transform);
			}

	}

	public Item sendItemData(int id)
	{
		Item itemToSend = database.FetchItemByID(id);

		return itemToSend;
	}

	public void AddItem(int id)
	{

			Item itemToAdd = database.FetchItemByID(id);
			if(itemToAdd.Stackable && ItemExistance(itemToAdd))
			{

					for(int i = 0; i < items.Count ; i++)
					{

							if(items[i].ID == id)
							{


								ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
								items[i].Amount++;

								data.amount++;
								data.transform.GetChild(1).GetComponent<Text>().text = data.amount.ToString();
								break;
							}

					}


			}
			else
			{
				AddSlots(1);

				for (int i = 0; i < items.Count ; i++ )
				{

					if(items[i].ID == -1)
					{



							items[i] = itemToAdd;
							GameObject itemObj = Instantiate(inventoryItem);

							itemObj.transform.SetParent(slots[i].transform);
							itemObj.transform.GetChild(0).GetComponent<Text>().text = itemToAdd.Title.ToString();
							itemObj.transform.GetChild(2).GetComponent<Text>().text = itemToAdd.Description.ToString();

							ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
							data.health = itemToAdd.Health;
							data.amount = itemToAdd.Amount;
							data.mana = itemToAdd.Mana;
							data.usable = itemToAdd.Usable;
							data.equippable = itemToAdd.Equippable;
							data.weapon = itemToAdd.Weapon;
							data.armor = itemToAdd.Armor;

							data.attack = itemToAdd.Attack;
							data.defense = itemToAdd.Defense;
							data.id = itemToAdd.ID;


							itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
							itemObj.transform.position = Vector2.zero;


							break;

					}

				}
			}

	}

	public void sortWeapons()
	{

		choosingWeapon = true;

		for(int i = 0 ; i < slots.Count ; i++)
		{

			ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();

			if(!data.weapon)
			{
				Debug.Log("Derp" + i);
				slots[i].SetActive(false);
			}
			if(data.weapon)
			{
			EventSystem.current.SetSelectedGameObject(slots[i]);
			}

		}
	}

	public void sortArmor()
	{

		choosingArmor = true;

		for(int i = 0 ; i < slots.Count ; i++)
		{

			ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();

			if(!data.armor)
			{
				Debug.Log("Derp" + i);
				slots[i].SetActive(false);
			}
			if(data.armor)
			{
			EventSystem.current.SetSelectedGameObject(slots[i]);
			}

		}
	}

	public void refreshItemList()
	{
		for(int i = 0 ; i < slots.Count ; i++)
		{
			if(slots[i] == null)
			{
				Debug.Log("FOUND ONE");
				slots.RemoveAt(i);
				items.RemoveAt(i);
			}
		}
	}

	public void normalizeItemSet()
	{
		if(choosingWeapon || choosingArmor)
		{

			for(int i = 0 ; i < slots.Count ; i++)
			{
				slots[i].SetActive(true);

				if(slots[i] == null)
				{
					ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
					Debug.Log("FOUND ONE" + data.id);
					slots.RemoveAt(i);
					items.RemoveAt(i);
				}

			}
		}

		choosingWeapon = false;
		choosingArmor = false;
	}

	public bool ItemExistance(Item item)
	{
			for(int i = 0 ; i < items.Count ; i++)
			{
					if(items[i].ID == item.ID)
					return true;

			}
			return false;

	}

	public void getstorageInfo()
	{

	for (int i = 0 ; i < items.Count ; i ++ )
		{

			Debug.Log("ID:"+items[i].ID);
			Debug.Log("Title:" + items[i].Title);
			Debug.Log("And AMOUNT:" + items[i].Amount);

			if(items[i].ID == 0) {

				items.RemoveAt(i);

			}
		}



	}

	// Update is called once per frame
	void Update () {



	}
}
