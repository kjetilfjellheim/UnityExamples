using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveOn : MonoBehaviour {

    public Button MoveOnBtn;

    // Use this for initialization
    void Start () {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(() => { MoveToNextScene(); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void MoveToNextScene()
    {
        Application.LoadLevel("mainscene");
    }

}
