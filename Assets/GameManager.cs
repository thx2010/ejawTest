using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ParticleManager particleManager;
    public ParticleCollector particleCollector;
    public PipeManager pipeManager;
    
    public int timeLimitSeconds = 10;
    private int _timeLeftCentiSeconds;
    
    public Text timeDisplay;
    public Text[] scoreDisplays;

    public UnityEvent onGameOver;
    private bool _gameIsPlaying = false;


    private void Start()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        _timeLeftCentiSeconds = timeLimitSeconds * 100;
        _gameIsPlaying = true;
        particleCollector.particlesCollected = 0;
        
        Time.timeScale = 1;
        particleManager.StartSpawning();
        pipeManager.Init();
    }

    private void FixedUpdate()
    {
        if ((int) Time.timeScale == 0 || _gameIsPlaying == false) return;
        
        _timeLeftCentiSeconds -= (int) (Time.deltaTime * 100);

        timeDisplay.text = $"Time: {_timeLeftCentiSeconds}";

        foreach (var scoreDisplay in scoreDisplays)
        {
            scoreDisplay.text = $"Score: {particleCollector.particlesCollected}";
        }

        if (_timeLeftCentiSeconds > 0) return;
        
        _gameIsPlaying = false;
        onGameOver?.Invoke();
        Time.timeScale = 0;
    }

    public void TogglePauseGame()
    {
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }
}
