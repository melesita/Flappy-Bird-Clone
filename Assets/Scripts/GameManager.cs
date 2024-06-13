using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI scoreText;
    public GameObject playButton;
    public GameObject gameOver;

    private Button playButtonComponent;
    private int score;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        
        Pause();

        // Get the Button component from the playButton GameObject
        playButtonComponent = playButton.GetComponent<Button>();
        
        // Ensure the Button component is not null
        if (playButtonComponent != null)
        {
            // Add the Play method to the button's onClick event
            playButtonComponent.onClick.AddListener(Play);
        }
        else
        {
            Debug.LogError("Button component not found on playButton GameObject");
        }
    }

    public void Play()
    {
        // Debug.Log("Play method called");

        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        // make sure all pipes are destroyed
        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        // Debug.Log("Play method execution completed");

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);

        Pause();
    }

}
