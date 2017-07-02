using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Button[] buttonList;
    public Text currentPlayer;

    public void Start()
    {
        foreach (Button button in buttonList)
        {
            button.onClick.AddListener(delegate { clickEvent(button); });
        }
    }

    public void clickEvent(Button eventButton)
    {
        string currentValue = eventButton.GetComponentInChildren<Text>().text;
        if (eventButton.interactable == true && currentValue.Equals(""))
        {
            eventButton.GetComponentInChildren<Text>().text = currentPlayer.text;
            nextPlayer();
            eventButton.interactable = false;
        } 
    }

    public void nextPlayer()
    {
        if (currentPlayer.text.Equals("X"))
        {
            currentPlayer.text = "O";
        }
        else
        {
            currentPlayer.text = "X";
        }

    }


}
