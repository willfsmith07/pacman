using UnityEngine;

public class GameOverlay : MonoBehaviour
{
    public Ghost[] ghosts;

    public Pacman pacman;

    public Transform pellets;

    public int score { get; private set; }

    public int lives { get; private set; }
}
