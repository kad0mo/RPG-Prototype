#pragma strict

function Start () {

}

function Update () {


}


function OnTriggerEnter(other:Collider) {

    if(other.gameObject.CompareTag("npc")) 
    {
        if(Input.GetButton("Action")) {


            GetComponent.<Renderer>().enabled = true;

        } 


    } else {

        GetComponent.<Renderer>().enabled = false;

    }

}   