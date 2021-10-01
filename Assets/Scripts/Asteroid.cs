using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    GameObject prefabRock;

    //[SerializeField]
    //Sprite sprite0;
    //[SerializeField]
    //Sprite sprite1;
    //[SerializeField]
    //Sprite sprite2;
    List<Sprite> spriteList = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        spriteList.Add(Resources.Load<Sprite>(@"Sprites\greenrock"));
        spriteList.Add(Resources.Load<Sprite>(@"Sprites\magentarock"));
        spriteList.Add(Resources.Load<Sprite>(@"Sprites\whiterock"));
        SpriteRenderer chosenSprite = gameObject.GetComponent<SpriteRenderer>();
        int spriteNum = Random.Range(0, 3);
        switch (spriteNum)
        {
            case 0:
                chosenSprite.sprite = spriteList[0];
                break;
            case 1:
                chosenSprite.sprite = spriteList[1];
                break;
            case 2:
                chosenSprite.sprite = spriteList[2];
                break;
            default:
                print("how did it even get here");
                break;
        }
    }

    public void Initialize(Direction direction, Vector3 spawnLocation)
    {
        Vector3 worldLocation = Camera.main.ScreenToWorldPoint(spawnLocation);
        GameObject spawnedRock = Instantiate<GameObject>(prefabRock, worldLocation, Quaternion.identity);
        
        float angle = 0;
        switch (direction)
        {
            case Direction.Right:
                angle = Random.Range(-15, 16) * Mathf.Deg2Rad;
                break;
            case Direction.Up:
                angle = Random.Range(75, 106) * Mathf.Deg2Rad;
                break;
            case Direction.Left:
                angle = Random.Range(165, 196) * Mathf.Deg2Rad;
                break;
            case Direction.Down:
                angle = Random.Range(255, 286) * Mathf.Deg2Rad;
                break;
            default:
                print("Not happening");
                break;
        }
        Asteroid pebble = spawnedRock.GetComponent<Asteroid>();
        pebble.StartMoving(angle);
    }

    public void StartMoving(float angle)
    {
        const float MinImpForce = 2f;
        const float MaxImpForce = 5f;
        float impForce = Random.Range(MinImpForce, MaxImpForce);

        Vector2 moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        GetComponent<Rigidbody2D>().AddForce(moveDirection * impForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if(gameObject.transform.localScale.x < 0.5)
            {
                Destroy(gameObject);
            }
            else
            {
                GameObject asteroid1 = Instantiate(prefabRock, transform.position, Quaternion.identity);
                Vector3 currSize = gameObject.transform.localScale;
                currSize.x /= 2;
                currSize.y /= 2;
                asteroid1.transform.localScale = currSize;
                float angle = Random.Range(0, 2 * Mathf.PI);
                Asteroid rock = asteroid1.GetComponent<Asteroid>();
                rock.StartMoving(angle);

                GameObject asteroid2 = Instantiate(prefabRock, transform.position, Quaternion.identity);
                Vector3 currSize2 = gameObject.transform.localScale;
                currSize2.x /= 2;
                currSize2.y /= 2;
                asteroid2.transform.localScale = currSize2;
                float angle2 = Random.Range(0, 2 * Mathf.PI);
                Asteroid rock2 = asteroid2.GetComponent<Asteroid>();
                rock2.StartMoving(angle2);

                Destroy(gameObject);
                AudioManager.Play(AudioClipName.AsteroidHit);
            }
        }
    }
}
