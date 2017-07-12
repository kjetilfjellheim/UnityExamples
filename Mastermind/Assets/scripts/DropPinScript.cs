using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropPinScript : MonoBehaviour, IDropHandler {

	public Image image;

	public void OnDrop(PointerEventData data) 
	{
		if (data.pointerDrag != null) 
		{
			GameObject gameObject = data.pointerDrag;
			Image pinImage = gameObject.GetComponentInChildren<Image> ();
			image.sprite = pinImage.sprite;

		}
	}

}
