#pragma strict

import UnityEngine.SceneManagement;

var score:int = 0;

function Start () {
	
}

function Update () {
	
}

function OnControllerColliderHit(hit: ControllerColliderHit) {
    if (hit.collider.gameObject.tag == "pick_me") {
        score++;
        Destroy(hit.collider.gameObject);
    }
    if (score == 4) {
        SceneManagement.SceneManager.LoadScene("collisionScene");
    }
}