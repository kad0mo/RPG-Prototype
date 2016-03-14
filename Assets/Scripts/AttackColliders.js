#pragma strict

function Start () {

  GetComponent.<Collider>().enabled = false;

}

function OnTriggerEnter(other:Collider) {

    if(other.gameObject.CompareTag("enemyhitbox"))
    {
        Debug.Log("Collided with enemy");
        GetComponent.<Collider>().enabled = false;

    }

}

function enableColliders()

  {
    GetComponent.<Collider>().enabled = true;
  }
function disableColliders()
{
    GetComponent.<Collider>().enabled = false;
}
