using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class armorSlot : MonoBehaviour {

	public int armorSlotId;
	public int addId;
	public int tempId;
	public int characterId;
	Characters character;
	GameObject Characters;

	public bool afterSelecting;

	Inventory inv;
	GameObject inventory;


	// Use this for initialization
	void Start () {

		Characters = GameObject.Find("Characters");
		character = Characters.GetComponent<Characters>();

		inventory = GameObject.Find("Inventory");
		inv = inventory.GetComponent<Inventory>();


		Debug.Log(armorSlotId);

		afterSelecting = false;

	}

	// Update is called once per frame
	void Update () {

		if(afterSelecting)
		{
			inv.refreshItemList();
		}

	}

	public void setEquipId(int profileid)
	{
		characterId = profileid;

		addId = character.characters[characterId].Armor;
		Debug.Log("Character id for Equip set to: " + characterId );
	}

	public void loadEquip()
	{

		armorSlotId = character.characters[characterId].Armor;
		Debug.Log(armorSlotId);

		for(int i = 0 ; i < inv.items.Count; i++)
		{
			if(inv.items[i].ID == armorSlotId)
			{
				this.transform.GetChild(0).GetComponent<Text>().text = inv.items[i].Title.ToString();
			}

		}

	}

	public void showEquip(int charId)
	{

		armorSlotId = character.characters[charId].Armor;
		Debug.Log("CURRENT WPN ID"+armorSlotId);


		Item arm = inv.sendItemData(armorSlotId);

			Debug.Log("FOUND ITEM: " +arm.Title);
			Debug.Log("CURRENTLY EQUIPPED: " + arm.Title);
			Debug.Log("CHARACTERS CURRENT ARMOR ID: "+ character.characters[characterId].Armor);
		this.transform.GetChild(0).GetComponent<Text>().text = arm.Title.ToString();

		/*
		for(int i = 0 ; i < inv.items.Count; i++)
		{
			if(inv.items[i].ID == weaponSlotId)
			{
				Debug.Log(inv.items[i].Title);
				this.transform.GetChild(0).GetComponent<Text>().text = inv.items[i].Title.ToString();
			} else
			{
				Debug.Log("ID WAS NOT FOUND ADERP");

			}


		}
		*/


	}

	public void addEquippedItem()
	{


			/*
		for(int i = 0 ; i < inv.items.Count ; i++)
		{
			if(inv.items[i].ID == addId)
			{
				inv.AddItem(inv.items[i].ID);
				Debug.Log(inv.items[i].Title);
			}



		}
		*/
		inv.AddItem(addId);
		addId = tempId;
	}


	public void getArmorId(int weaponid)
	{

		Debug.Log(weaponid);

		armorSlotId = weaponid;

		character.characters[characterId].Armor = weaponid;
		Debug.Log("CHARACTERS CURRENT ARMOR ID: "+ character.characters[characterId].Armor);

		tempId = weaponid;

	}

	public void refresh()
	{

		inv.refreshItemList();

	}


	}
