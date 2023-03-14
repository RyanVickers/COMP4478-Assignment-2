using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainImageScript : MonoBehaviour
{
    [SerializeField] private GameObject image_blank;
    [SerializeField] private GameControllerScript gameController;

    public void OnMouseDown()
    {
        if (image_blank.activeSelf && gameController.canOpen){
            image_blank.SetActive(false);
            gameController.imageOpened(this);
        }
    }

    private int spriteID;

    public int getSpriteID
    {
        get {return spriteID;}
    }

    public void changeSprite(int id, Sprite image)
    {
        spriteID = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void Close()
    {
        image_blank.SetActive(true);
    }
}
