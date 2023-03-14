using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    public const int rows = 4;
    public const int columns = 4;

    public const float Xspace = 4f;
    public const float Yspace = -2f;

    private int matchesMade = 0;

    [SerializeField] private MainImageScript startObject;
    [SerializeField] private Sprite[] images;

//Function to randomize cards
private int[] Randomizer(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for(int i=0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }

        return array;
    }

//Function to start game populating card locations
    private void Start()
    {
        int[] locations = {0,0,0,0,1,1,1,1,2,2,2,2,3,3,3,3};
        locations = Randomizer(locations);

        Vector3 startPosition = startObject.transform.position;

        for(int i=0; i < columns; i++)
        {
            for(int j=0; j< rows; j++)
            {
                MainImageScript gameImage;
                if(i==0 && j==0)
                {
                    gameImage = startObject;
                } else
                {
                    gameImage = Instantiate(startObject) as MainImageScript;
                }

                int index = j * columns + i;
                int id = locations[index];
                gameImage.changeSprite(id, images[id]);

                float positionX = (Xspace * i) + startPosition.x;
                float positionY = (Yspace * j) + startPosition.y;

                gameImage.transform.position = new Vector3(positionX, positionY, startPosition.z);
            }
        }
    }

    private MainImageScript firstOpen;
    private MainImageScript secondOpen;

    private int score = 0;
    private int attempts = 0;

    [SerializeField] private TextMesh scoreText;
    [SerializeField] private TextMesh attemptText;

    public bool canOpen
    {
        get { return secondOpen == null; }
    }

//Function to check if one card is already flipped
    public void imageOpened(MainImageScript startObject)
    {
        if(firstOpen == null)
        {
            firstOpen = startObject;
        } else
        {
            secondOpen = startObject;
            StartCoroutine(CheckGuessed());
        }
    }

//Function to check if correct pairs
    private IEnumerator CheckGuessed()
    {
        if (firstOpen.getSpriteID == secondOpen.getSpriteID)
        {
            score++;
            scoreText.text = "Score: " + score;
            matchesMade++;
        if (matchesMade == (rows * columns) / 2)
        {
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetInt("Attempts", attempts);
            SceneManager.LoadScene("GameOverScene");
        }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            firstOpen.Close();
            secondOpen.Close();
        }
        attempts++;
        attemptText.text = "Attempts: " + attempts;
        firstOpen = null;
        secondOpen = null;   
    }

}
