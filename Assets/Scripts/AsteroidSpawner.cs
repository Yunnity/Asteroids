using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabAsteroid;

    // Start is called before the first frame update
    void Start()
    {
        float zCoord = -Camera.main.transform.position.z;
        GameObject asteroid = Instantiate<GameObject>(prefabAsteroid);
        Asteroid spawner = asteroid.GetComponent<Asteroid>();
        CircleCollider2D collider = asteroid.GetComponent<CircleCollider2D>();
        float rockRadius = collider.radius;
        spawner.Initialize(Direction.Up, new Vector3(Screen.width / 2, rockRadius, zCoord));
        spawner.Initialize(Direction.Right, new Vector3(rockRadius, Screen.height / 2, zCoord));
        spawner.Initialize(Direction.Left, new Vector3(Screen.width - rockRadius, Screen.height / 2, zCoord));
        spawner.Initialize(Direction.Down, new Vector3(Screen.width / 2, Screen.height - rockRadius, zCoord));
        Destroy(asteroid);
    }
}
