using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour {

    public enum Color { BLUE, CYAN, RED, GREEN, PURPLE, YELLOW };

    public System.Random random = new System.Random();

    public Color[] resultColors = new Color[4];

    public Button newGameBtn;

    public Image[] guessResult;

    public Image blueImage;
    public Image purpleImage;
    public Image cyanImage;
    public Image redImage;
    public Image yellowImage;
    public Image greenImage;

    private int guessNumber = 0;

    public Button[] guessButton;

    // Use this for initialization
    void Start ()
    {
        newGameBtn.onClick.AddListener(delegate { NewGame(); });
        Debug.Log("Start resultscript");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void NewGame()
    {
        Debug.Log("New game");
        resultColors = new Color[4];
        for (int i = 0; i < 4;i++) { 
            System.Array values = System.Enum.GetValues(typeof(Color));
            Color color = (Color)values.GetValue(random.Next(values.Length));
            resultColors[i] = color;
            if (color == Color.BLUE)
            {
                guessResult[i].sprite = blueImage.sprite;
            }
            else if (color == Color.CYAN)
            {
                guessResult[i].sprite = cyanImage.sprite;
            }
            else if (color == Color.GREEN)
            {
                guessResult[i].sprite = greenImage.sprite;
            }
            else if (color == Color.PURPLE)
            {
                guessResult[i].sprite = purpleImage.sprite;
            }
            else if (color == Color.RED)
            {
                guessResult[i].sprite = redImage.sprite;
            }
            else
            {
                guessResult[i].sprite = yellowImage.sprite;
            }
        } 
        for (int i = 0;i < 10; i++)
        {
            guessButton[i].enabled = false;
        }
    } 

}
