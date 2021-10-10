using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudent_Movement : MonoBehaviour
{
    float elapsedTime = 0f;
    int position = 1;
    float duration = 1.5f;
    private Animator PacAnimator;
    Vector2 position1 = new Vector2(-0.1f, 0.3f);
    Vector2 position2 = new Vector2(-1.07f, 0.3f);
    Vector2 position3 = new Vector2(-1.07f, 1.03f);
    Vector2 position4 = new Vector2(-0.1f, 1.03f);

    // Start is called before the first frame update
    void Start()
    {
        PacAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (position == 1)
        {
            if (Vector2.Distance(transform.position, position2) > 0)
            {
                float t = elapsedTime / duration;
                transform.position = Vector2.Lerp(position1, position2, t);
                elapsedTime += Time.deltaTime;
            } else
            {
                PacAnimator.SetInteger("Direction", 90);
                position = 2;
                elapsedTime = 0;  
            }
        } else if (position == 2)
        {
            if (Vector2.Distance(transform.position, position3) > 0)
            {
                float t = elapsedTime / duration;
                transform.position = Vector2.Lerp(position2, position3, t);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                PacAnimator.SetInteger("Direction", 180);
                position = 3;
                elapsedTime = 0;
            }
        } else if (position == 3)
        {
            if (Vector2.Distance(transform.position, position4) > 0)
            {
                float t = elapsedTime / duration;
                transform.position = Vector2.Lerp(position3, position4, t);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                PacAnimator.SetInteger("Direction", 270);
                position = 4;
                elapsedTime = 0;
            }
        }  else if (position == 4)
        {
            if (Vector2.Distance(transform.position, position1) > 0)
            {
                float t = elapsedTime / duration;
                transform.position = Vector2.Lerp(position4, position1, t);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                PacAnimator.SetInteger("Direction", 0);
                position = 1;
                elapsedTime = 0;
            }
        }
    }

  
}
