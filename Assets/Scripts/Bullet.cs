using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float deathTimer = 2;
    Timer timer;
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = deathTimer;
        timer.Run();
    }

    void Update()
    {
        if(timer.Finished)
        {
            Destroy(gameObject);
        }
    }
    public void ApplyForce(Vector2 direction)
    {
        const float Magnitude = 5;
        gameObject.GetComponent<Rigidbody2D>().AddForce(Magnitude * direction, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
