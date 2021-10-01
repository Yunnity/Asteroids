using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    GameObject prefabBullet;

    Rigidbody2D shipBody;
    Vector2 thrustDirection = new Vector2(1, 0);
    const float ThrustForce = 4;
    const float DegreesPerSec = 250;

    // Start is called before the first frame update
    void Start()
    {
        shipBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float thrustPotential = Input.GetAxis("Thrust");
        if(thrustPotential > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(thrustDirection * ThrustForce, ForceMode2D.Force);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Rotate") != 0)
        {
            float rotationAmount = DegreesPerSec * Time.deltaTime;
            if (Input.GetAxis("Rotate") < 0)
            {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);
            float angleRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            float xDirection = Mathf.Cos(angleRotation);
            float yDirection = Mathf.Sin(angleRotation);
            thrustDirection = new Vector2(xDirection, yDirection);
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject shot = Instantiate<GameObject>(prefabBullet, gameObject.transform.position, Quaternion.identity);
            Bullet bullet = shot.GetComponent<Bullet>();
            bullet.ApplyForce(thrustDirection);
            AudioManager.Play(AudioClipName.PlayerShot);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Rock")
        {
            Destroy(gameObject);
            AudioManager.Play(AudioClipName.PlayerDeath);
            HUD.running = false;
        }
    }
}
