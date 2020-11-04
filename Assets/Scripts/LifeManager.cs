using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int startingLives;
    private int lifeCounter;

    private Text theText;

    public GameObject gameOverScreen;

    public PlayerController player;

    public string mainMenu;

    public float waitAfterGameOver;

    void Start()
    {
        theText = GetComponent<Text>();
        lifeCounter = startingLives;
        // Get the player object
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeCounter < 0)
        {
            // Activate game over screen
            gameOverScreen.SetActive(true);
            // TODO: De-activate player
            // player.SetActive(false);
        }
        theText.text = "x " + lifeCounter;

        if (gameOverScreen.activeSelf)
        {
            waitAfterGameOver -= Time.deltaTime;
        }
        // Start the game over again
        if (waitAfterGameOver < 0)
        {
            Application.LoadLevel(mainMenu);
        }
    }

    public void GiveLife()
    {
        lifeCounter++;
    }
    public void TakeLife()
    {
        lifeCounter--;
    }

}
