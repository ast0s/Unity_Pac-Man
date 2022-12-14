using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Pacman pacman;
    public Ghost[] ghosts;
    public Transform pellets;

    public Text gameOverText;
    public Text scoreText;
    public Text livesText;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start() => NewGame();

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
            NewGame();
    }

    private void NewRound()
    {
        gameOverText.enabled = false;

        foreach (Transform pellet in pellets) 
            pellet.gameObject.SetActive(true);

        ResetState();
    }

    private void ResetState()
    {
        ResetGhostMultiplier();

        foreach (Ghost ghost in ghosts) 
            ghost.ResetState();

        pacman.ResetState();
    }

    private void GameOver()
    {
        gameOverText.enabled = true;

        foreach (Ghost ghost in ghosts)
            ghost.gameObject.SetActive(false);

        pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);

        ghostMultiplier++;
    }
    public void PacmanEaten()
    {
        pacman.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0)
            Invoke(nameof(ResetState), 3.0f);
        else
            GameOver();
    }
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }
    public void PowerPelletEaten(PowerPellet pellet)
    {
        foreach (Ghost ghost in ghosts)
            ghost.frightened.Enable(pellet.duration);

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }
    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
            if (pellet.gameObject.activeSelf) 
                return true;

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
}