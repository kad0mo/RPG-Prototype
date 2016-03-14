#pragma strict

var Target: GameObject;
var locked: boolean = false;
var moveSpeed: float = 10.0;
var attackLine: Vector3;
var enterCombat: boolean = false;
var attackTimer:float = 0.0f;
var attackRate:float = 4.0f;
var enemyHealth:int = 4;
var enemyAudio : AudioSource;
var damageClip : AudioClip;

var rb: Rigidbody;
var knockForce: float = 2.0;

var distance:float;


function Start () {

  rb = GetComponent.<Rigidbody>();
  GetComponent.<Animation>()["Armature|Combat_Guard"].layer = 1;

}

function Update () {

    distance = Vector3.Distance(transform.position, Target.transform.position);



    if(locked) {


        GetComponent.<Animation>().CrossFade("Armature|Walking_block", 0.2);
        Combat();



    } else {

        GetComponent.<Animation>().CrossFade("Armature|Idle", 0.2);

    }

}

  function OnTriggerStay(other:Collider)
  {
    if(other.gameObject.CompareTag("Player"))
    {

        locked = true;
    }
  }


function OnTriggerEnter(other:Collider) {

    if(other.gameObject.CompareTag("Player"))
    {

        locked = true;

    }


}

    function OnTriggerExit(other:Collider) {

        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has exited field of vision. Return to non combat state.");
            locked = false;

        }


    }

    function takeDamage(damage) {

      Debug.Log(damage);
      enemyHealth -= 1;
      rb.AddForce(-transform.forward * knockForce);
      GetComponent.<Animation>().CrossFade("Armature|Combat_Guard", 0.2);


      enemyAudio.Play ();

      if(enemyHealth < 1) {

        Debug.Log("Enemy Dies");
        Destroy(gameObject);

      }


    }

        function Combat () {

            if(!enterCombat) {

                transform.LookAt(Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));

                var delta = Target.transform.position - transform.position;
                delta.Normalize();

                if(distance > 3) {
                    transform.position = transform.position + (delta * 2 * Time.deltaTime);

                }
            }

            if(distance < 3) {

            attackTimer += Time.deltaTime;

            GetComponent.<Animation>().CrossFade("Armature|Combat_Guard", 0.2);

            if(attackTimer > attackRate) {

                enterCombat = true;
                GetComponent.<Animation>().CrossFade("Armature|Blerf", 0.05);
                /*
                attackLine = Vector3(0.0f, transform.position.y, Target.transform.position.z);
                transform.Translate(attackLine * moveSpeed * Time.deltaTime);
                */
                yield WaitForSeconds(0.5);
                enterCombat = false;
                attackTimer = 0;
            }

          }

        }
