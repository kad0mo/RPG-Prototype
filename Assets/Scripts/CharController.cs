using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	public float moveSpeed;
	public float walkSpeed;
	public float runSpeed;

	public bool Combat = false;
	public bool Inventory = false;

	public GameObject[] enemyLocations;
	public GameObject closest;

	public bool isDashing = false;
	public float jumpForce;
	public float rotateSpeed;
	public bool canJump = false;
	public bool canDoubleJump = false;

	private Vector3 newDirection;
	public float yGravity = -5.0f;

	private Rigidbody rb;

	private float moveHorizontal;
	private float moveVertical;
	private Vector3 movement;

	public float turnSmoothing = 15.0f;

	public Animation anim;
	public GameObject activeIcon;

	GameObject combatPanel;
	GameObject enemyPanel;


	// Use this for initialization
	void Start () {

		enemyPanel = GameObject.Find("Enemy Panel");
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animation>();
		GetComponent<Animation>()["Armature|Attackk.002"].layer = 1;
    GetComponent<Animation>()["Armature|Blerf"].layer = 1;
    GetComponent<Animation>()["Armature|Swordattack_01"].layer = 1;
    GetComponent<Animation>()["Armature|Swordattack_02"].layer = 1;
    GetComponent<Animation>()["Armature|Swordattack_02"].layer = 1;
    GetComponent<Animation>()["Armature|Jumping"].layer = 1;



	}

	// Update is called once per frame
	public void Update () {

		newDirection = Camera.main.transform.TransformDirection(Vector3.forward);
		Physics.gravity = new Vector3(0, yGravity, 0);

		if(Combat)
		{
		CombatControls();
		}
		if(Inventory)
		{
			inventoryActiveControls();
		}
		if(!Inventory && !Combat)
		{
		PlayerControls();
		}

	}

	void Awake () {


	    transform.forward = new Vector3(newDirection[0], 0, newDirection[2]);


	}



	void PlayerControls ()
	{

		moveHorizontal = Input.GetAxisRaw ("Horizontal");
		moveVertical = Input.GetAxisRaw ("Vertical");

		movement = new Vector3(moveHorizontal, 0.0f,  moveVertical);

		movement = Camera.main.transform.TransformDirection(movement);
		movement.y = 0.0f;


		if(moveHorizontal != 0 || moveVertical != 0)
		{

		Quaternion targetRotation =  Quaternion.LookRotation(movement, Vector3.up);
		Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmoothing * Time.deltaTime);

			GetComponent<Rigidbody>().MoveRotation(newRotation);

			  transform.Translate (movement * moveSpeed * Time.deltaTime, Space.World);

				if(isDashing)
				{

				anim.CrossFade("Armature|Run", 0.2f);

				} else



				{

					anim.CrossFade("Armature|Walking_block");

				}

		} else {


					anim.CrossFade("Armature|Idle", 0.2f);

		}

		 if(Input.GetButtonDown("Dashbutton") && canJump)
		 {

			 isDashing = true;
			 moveSpeed = runSpeed;

				//Kiihdytys?

		 }

	  if(Input.GetButtonUp("Dashbutton"))
	  {

			 isDashing = false;
			 moveSpeed = walkSpeed;

  	}

		if(Input.GetButton("Jump") && canJump)
		{
			/*
			GetComponent<Rigidbody>().velocity.y += jumpForce;
			*/
			GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
			canJump = false;
			anim.CrossFade("Armature|Jumping");
			canDoubleJump = true;

		}
		/*
		if(Input.GetButton("Jump") && canDoubleJump) {

			canDoubleJump = false;
			this.GetComponent.<Rigidbody>().velocity.y += jumpForce;

		}
		*/



	}

	public void activateInventory()
	{
		Inventory = true;
	}

	public void deActivateInventory()
	{
		Inventory = false;
	}

	public void endCombat()
	{

		Combat = false;

	}

	public void engageCombat ()
	{
		Combat = true;
		enemyPanel.SetActive(true);
	}

/*
	public void enemyTargeting()
	{
		if (closest != null && Combat)
		{
		activeIcon.SetActive(true);
		activeIcon.transform.position = new Vector3(closest.transform.position.x, closest.transform.position.y + 5, closest.transform.position.z);

		} else
		{
			activeIcon.SetActive(false);
		}
	}

*/
	public void inventoryActiveControls ()
	{

		anim.CrossFade("Armature|Idle", 0.2f);
		/*
		FindClosestEnemy();
		enemyTargeting();
		*/
	}

	public void CombatControls ()
	{

		anim.CrossFade("Armature|Swordidle", 0.2f);
		/*
		FindClosestEnemy();
		enemyTargeting();
		*/
	}

/*
	GameObject FindClosestEnemy ()
	{

			enemyLocations = GameObject.FindGameObjectsWithTag("enemy");

			float distance = Mathf.Infinity;
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
			return closest;
	}
*/

		 void OnCollisionEnter(Collision collision)
		{
			if(collision.gameObject.CompareTag("ground"))
			{
				canJump = true; //allow him to jump again
				anim.CrossFade("Armature|Idle");
				canDoubleJump = false;
			}
		}

}
