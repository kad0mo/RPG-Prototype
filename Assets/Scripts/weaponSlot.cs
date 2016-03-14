using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class weaponSlot : MonoBehaviour {

	public int weaponSlotId;
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


		Debug.Log(weaponSlotId);

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
		addId = character.characters[characterId].Weapon;
		Debug.Log("Character id for Equip set to: " + characterId );
	}

	public void loadEquip()
	{

		weaponSlotId = character.characters[characterId].Weapon;
		Debug.Log(weaponSlotId);

		for(int i = 0 ; i < inv.items.Count; i++)
		{
			if(inv.items[i].ID == weaponSlotId)
			{
				this.transform.GetChild(0).GetComponent<Text>().text = inv.items[i].Title.ToString();
			}

		}

	}

	public void showEquip(int charId)
	{

		weaponSlotId = character.characters[charId].Weapon;
		Debug.Log("CURRENT WPN ID"+weaponSlotId);

		Item wpn = inv.sendItemData(weaponSlotId);

		Debug.Log(wpn.Title);

		this.transform.GetChild(0).GetComponent<Text>().text = wpn.Title.ToString();

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


	public void getWeaponId(int weaponid)
	{

		Debug.Log(weaponid);

		weaponSlotId = weaponid;

		character.characters[characterId].Weapon = weaponid;

		tempId = weaponid;

	}

	public void refresh()
	{

		inv.refreshItemList();

	}


}
