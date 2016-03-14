using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Characters : MonoBehaviour {

	GameObject inventoryPanel;
	public GameObject profilePanel;
	GameObject stretchPanel;
	GameObject descriptionPanel;

	characterDataBase charDataBase;
	public GameObject characterSlot;
	public GameObject inventoryItem;
	public GameObject Player;
	int charSlotAmount;

	public int characterNumber;

	public List<Character> characters = new List<Character>();
	public List<GameObject> characterSlots = new List<GameObject>();


	void Awake ()
	{

		Player = GameObject.Find("Player");

		charDataBase = this.GetComponent<characterDataBase>();

		characterNumber = 0;
		charSlotAmount = 0;

		inventoryPanel = GameObject.Find("Inventory Panel");
		descriptionPanel = GameObject.Find("Description Panel");
		stretchPanel = GameObject.Find("StretchPanel");

	}

	void Start () {


			//
			/*
			profilePanel = GameObject.Find("Profile Panel");


			for(int i = 0 ; i < charSlotAmount ; i++)
			{
					characters.Add(new Character());
					characterSlots.Add(Instantiate(characterSlot));
					characterSlots[i].transform.SetParent(profilePanel.transform);
			}

      /*
			AddSlots(4);
			AddItem(0);
			AddItem(0);
			AddItem(0);
			AddItem(1);
			AddItem(2);
			AddItem(3);
			AddItem(3);
      */




			StartCoroutine(Example());

	}

	public void addCharacterSlots(int charSlotAmount)
	{
			int current;

			for(int i = 0 ; i < charSlotAmount ; i++)
			{
					current = characters.Count;
					characterSlots.Add(Instantiate(characterSlot));
					characterSlots[current].transform.SetParent(profilePanel.transform);


			}

	}

	public void AddCharacter(int id)
	{

			characters.Add(new Character());
			Character characterToAdd = charDataBase.FetchCharacterByID(id);

				for (int i = 0; i < characters.Count ; i++ )
				{

					if(characters[i].ID == -1)
					{



							characters[i] = characterToAdd;

							/*
							characterData charData = characterSlots[0].GetComponent<characterData>();
							charData.id = characters[i].ID;
							/*
							GameObject itemObj = Instantiate(inventoryItem);
							*/
							Debug.Log("Char ID:" + characters[i].ID);
							Debug.Log("Char name" + characters[i].Name);
							Debug.Log("Char lv" +characters[i].Level);
							Debug.Log("char stats:" + characters[i].Str);
							Debug.Log("cur hp " + characters[i].CurHp);



								/*
							itemObj.transform.SetParent(slots[i].transform);
							itemObj.transform.GetChild(0).GetComponent<Text>().text = itemToAdd.Title.ToString();
							itemObj.transform.GetChild(2).GetComponent<Text>().text = itemToAdd.Description.ToString();

							ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
							data.health = itemToAdd.Health;
							data.mana = itemToAdd.Mana;
							data.usable = itemToAdd.Usable;


							itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
							itemObj.transform.position = Vector2.zero;
							*/

							break;

					}

				}


	}

	bool characterExistance(Character character)
	{
			for(int i = 0 ; i < characters.Count ; i++)
			{
					if(characters[i].ID == character.ID)
					return true;

			}
			return false;

	}

	IEnumerator Example() {


		yield return new WaitForSeconds(1);
		makeCharacters();

	}

	public void makeCharacters()
	{


			AddCharacter(0);
			AddCharacter(1);

	}

	// Update is called once per frame
	void Update () {


	}
}
