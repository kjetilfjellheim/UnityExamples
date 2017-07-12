using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour {

	public const int MAX_GUESSES = 10;

	public const int COLOR_GUESSES = 4;

    public enum Color { BLUE, CYAN, RED, GREEN, PURPLE, YELLOW };

    public Button newGameBtn;

	private Sprite[] resultSprites = new Sprite[COLOR_GUESSES];

	public Sprite emptyResultImage;

	public Sprite emptyImage;

	public Image[] guessResult;

    public Image blueImage;
    public Image purpleImage;
    public Image cyanImage;
    public Image redImage;
    public Image yellowImage;
    public Image greenImage;

	public Sprite correctPlaceResultImage;
	public Sprite wrongPlaceResultImage;

    private int guessNumber = 1;

    public Button guessButton;

    void Start ()
    {
        Debug.Log("Start.");
        newGameBtn.onClick.AddListener(delegate { NewGame(); });
		guessButton.onClick.AddListener(delegate { Guess(); });
		NewGame ();
    }
	
	void Update ()
    {
		
	}

    public void NewGame()
    {
        Debug.Log("New game");
		ResetBoard();
    }

    public void Guess()
    {
		if (CanGuess()) 
		{
	        Debug.Log("Guessing");
			bool solved = SetResultPins();
			if (solved) 
			{
				ShowResult ();
				HideGuessButton ();
			}
	        guessNumber++;        
			if (guessNumber > MAX_GUESSES) {   
				ShowResult ();
				HideGuessButton ();
			} else {
				SetGuessButtonPosition ();
			}
			SetAllowPinDrag ();
		}
    }

	public void ShowResult()
    {
        Debug.Log("Shows result.");
		for (int i = 0; i< COLOR_GUESSES; i++) 
		{
			guessResult [i].sprite = resultSprites [i];
		}
    }

	public void SetGuessButtonPosition() 
	{
		float newY = GameObject.Find("Panel" + guessNumber).transform.position.y;
		Vector3 vector = guessButton.transform.position;
		vector.y = newY;
		guessButton.transform.position = vector;
	}

	public bool CanGuess() 
	{
		if (guessNumber > MAX_GUESSES || !HasFilledPins()) 
		{
			return false;
		}
		return true;
	}

	public void ResetBoard()
	{
		System.Random random = new System.Random ();
		for (int i = 0; i < COLOR_GUESSES;i++) { 
			System.Array values = System.Enum.GetValues(typeof(Color));
			Color color = (Color)values.GetValue(random.Next(values.Length));
			this.guessResult [i].sprite = this.emptyImage;
			if (color == Color.BLUE)
			{								
				resultSprites[i] = blueImage.sprite;
			}
			else if (color == Color.CYAN)
			{
				resultSprites[i] = cyanImage.sprite;
			}
			else if (color == Color.GREEN)
			{
				resultSprites[i] = greenImage.sprite;
			}
			else if (color == Color.PURPLE)
			{
				resultSprites[i] = purpleImage.sprite;
			}
			else if (color == Color.RED)
			{
				resultSprites[i] = redImage.sprite;
			}
			else
			{
				resultSprites[i] = yellowImage.sprite;
			}
		}
		for (int panelCnt = 1;panelCnt <= MAX_GUESSES; panelCnt++) 
		{
			for (int pinCnt = 1;pinCnt <= COLOR_GUESSES; pinCnt++ ) {
				GameObject.Find ("Choice_" + panelCnt + "_" + pinCnt).GetComponentInChildren<Image>().sprite = emptyImage;
				GameObject.Find ("Result_" + panelCnt + "_" + pinCnt).GetComponentInChildren<Image>().sprite = emptyImage;
			}
		}
		guessNumber = 1;
		SetGuessButtonPosition ();		
		SetAllowPinDrag ();
	}	

	public void HideGuessButton() 
	{
		float newY = GameObject.Find ("ResultPanel").transform.position.y + 1000;
		Vector3 vector = guessButton.transform.position;
		vector.y = newY;
		guessButton.transform.position = vector;
	}

	public bool HasFilledPins()
	{
		int pinsFilled = 0;
		for (int i = 1; i <= COLOR_GUESSES; i++) 
		{
			if (!GameObject.Find ("Choice_" + this.guessNumber + "_" + i).GetComponentInChildren<Image> ().sprite.Equals(emptyImage)) 
			{
				pinsFilled++;
			}
		}
		return (pinsFilled == COLOR_GUESSES);
	}

	public bool SetResultPins()
	{
		int numWhite = 0;
		int numBlack = 0;
		bool[] guessUsedPins = new bool[COLOR_GUESSES]{false, false, false, false};
		bool[] resultUsedPins = new bool[COLOR_GUESSES]{false, false, false, false};
		for (int pinCnt = 1; pinCnt <= COLOR_GUESSES; pinCnt++) 
		{
			Sprite guessSprite = GameObject.Find ("Choice_" + this.guessNumber + "_" + pinCnt).GetComponentInChildren<Image> ().sprite;
			if (guessSprite.Equals(resultSprites[pinCnt - 1])) 
			{
				numBlack++;
				guessUsedPins [pinCnt - 1] = true;
				resultUsedPins [pinCnt - 1] = true;
			}
		}
		for (int guessPinCnt = 1; guessPinCnt <= COLOR_GUESSES; guessPinCnt++) 
		{
			for (int resultPinCnt = 1; resultPinCnt <= COLOR_GUESSES; resultPinCnt++) 
			{
				if (!resultUsedPins[resultPinCnt - 1] && !guessUsedPins[guessPinCnt - 1])  
				{
					Sprite guessSprite = GameObject.Find ("Choice_" + this.guessNumber + "_" + guessPinCnt).GetComponentInChildren<Image> ().sprite;
					if (guessSprite.Equals (resultSprites [resultPinCnt - 1])) 
					{
						guessUsedPins [guessPinCnt - 1] = true;
						resultUsedPins [resultPinCnt - 1] = true;
						numWhite++;
					}
				}
			}
		}
		for (int blackPinCnt = 1; blackPinCnt <= numBlack; blackPinCnt++) 
		{
			Image resultSprite = GameObject.Find ("Result_" + this.guessNumber + "_" + blackPinCnt).GetComponentInChildren<Image> ();
			resultSprite.sprite = this.correctPlaceResultImage;
		}
		for (int whitePinCnt = (numBlack + 1); whitePinCnt <= (numBlack + numWhite); whitePinCnt++) 
		{
			Image resultSprite = GameObject.Find ("Result_" + this.guessNumber + "_" + whitePinCnt).GetComponentInChildren<Image> ();
			resultSprite.sprite = this.wrongPlaceResultImage;		
		}
		Debug.Log ("guessNumber black: " + numBlack);
		Debug.Log ("guessNumber white: " + numWhite);
		return (numBlack == COLOR_GUESSES);
	}

	public void SetAllowPinDrag() 
	{
		for (int panelCnt = 1;panelCnt <= MAX_GUESSES; panelCnt++) 
		{
			for (int pinCnt = 1;pinCnt <= COLOR_GUESSES; pinCnt++ ) {
				GameObject.Find ("Choice_" + panelCnt + "_" + pinCnt).GetComponentInChildren<Image>().raycastTarget = (this.guessNumber == panelCnt);
			}
		}
	}

}

