var talkLines:String[];
var talkText:UnityEngine.UI.Text;
var ScrollSpeed:int;

private var talking:boolean = false;
private var currentLine:int;
private var textIsScrolling:boolean = true;
public var dialoguepanel: GameObject;

GameObject.Find("dialoguepanel");

function OnTriggerEnter(other:Collider) {

    if(other.gameObject.CompareTag("Player"))
    {
        Debug.Log("Dialog active");
        talking = true;
        currentLine = -1;

        //talkTextGUI.text = talkLines[currentLine];

    }
}

    function Start() {

        dialoguepanel = GameObject.Find("DialoguePanel");
        dialoguepanel.SetActive(false);

    }

    function Update () {


        if(talking && Input.GetButtonDown("Action")) {

            dialoguepanel.SetActive(true);

            //display next line
            if(currentLine < talkLines.Length - 1) {

                currentLine++;
                //talkTextGUI.text = talkLines[currentLine];
                startScrolling();

            }
            else {
                currentLine = 0;
                talkText.text = "";
                talking = false;
                dialoguepanel.SetActive(false);

            }
        }




    }



    function startScrolling()
    {
        var displayText:String = "";

        for(var i:int = 0 ; i < talkLines[currentLine].Length; i++)
        {
            Debug.Log("hurp");
            displayText += talkLines[currentLine][i];
            talkText.text = displayText;
            yield WaitForSeconds(1/ScrollSpeed);

        }
}
