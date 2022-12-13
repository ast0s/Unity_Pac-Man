using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Pacman pacman;
    public Ghost[] ghosts;
    public Transform pellets;

    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void GameOver()
    {
        foreach (Transform pellet in pellets)
        { pellet.gameObject.SetActive(false); }

        foreach (Ghost ghost in ghosts)
        { ghost.gameObject.SetActive(false); }

        pacman.gameObject.SetActive(false);
    }

    private void NewRound()
    {
        foreach(Transform pellet in pellets) 
        { pellet.gameObject.SetActive(true); }

        ResetState();
    }

    private void ResetState()
    {
        foreach (Ghost ghost in ghosts)
        { ghost.gameObject.SetActive(true); }

        pacman.gameObject.SetActive(true);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }
    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(score + ghost.points);
    }
    public void PacmanEaten(Ghost ghost)
    {
        pacman.gameObject.SetActive(false);

        SetLives(lives - 1);

        if (lives > 0)
            Invoke(nameof(ResetState), 3.0f);
        else
            GameOver();
    }
}