using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapping : MonoBehaviour
{
    float hitBoxRadius;
    // Start is called before the first frame update
    void Start()
    {
        hitBoxRadius = GetComponent<CircleCollider2D>().radius;
    }
    void OnBecameInvisible()
    {
        Vector2 currPosition = transform.position;
        if (hitBoxRadius + currPosition.y > ScreenUtils.ScreenTop)
        {
            //currPosition.y = ScreenUtils.ScreenBottom + shipRadius; 
            currPosition.y *= -1;
        }
        else if (currPosition.y - hitBoxRadius < ScreenUtils.ScreenBottom)
        {
            //currPosition.y = ScreenUtils.ScreenTop - shipRadius;
            currPosition.y *= -1;
        }
        if (hitBoxRadius + currPosition.x > ScreenUtils.ScreenRight)
        {
            //currPosition.x = shipRadius + ScreenUtils.ScreenLeft;
            currPosition.x *= -1;
        }
        else if (currPosition.x - hitBoxRadius < ScreenUtils.ScreenLeft)
        {
            //currPosition.x = ScreenUtils.ScreenRight - shipRadius;
            currPosition.x *= -1;
        }
        transform.position = currPosition;
    }
}
