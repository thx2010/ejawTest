using UnityEngine;

public class Particle : MonoBehaviour
{
    public float minSpeed = .2f;
    
    private float _minSpeedSquared;
    private Rigidbody2D _rigidBody2D;
    
    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _minSpeedSquared = minSpeed * minSpeed;
    }

    private void FixedUpdate()
    {
        
        var currentSpeedSquared = _rigidBody2D.velocity.sqrMagnitude;
        var minSpeedLocal = minSpeed;
        Vector2 direction;

        if (currentSpeedSquared > _minSpeedSquared) return;

        if (currentSpeedSquared < 0.01)
        {
            direction = Vector2.left * (Random.Range(0, 2) == 0 ? -1 : 1);
        }
        else
        {
            direction = _rigidBody2D.velocity.normalized * Vector2.right;
        }

        _rigidBody2D.velocity += minSpeedLocal * direction ;
    } 
}