using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoSol : MonoBehaviour
{
    public Vector3 sunrise;
    public Vector3 sunset;

    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;

    // The time at which the animation started.
    private float startTime;

    public bool comecar = false;



    void Start()
    {
        
    }

    public void comecarMovimento()
    {
        // Note the time at the start of the animation.
        startTime = Time.time;
        comecar = true;
    }

    void Update()
    {
        if (comecar)
        {
            // The center of the arc
            Vector3 center = (sunrise + sunset) * 0.5f;

            // move the center a bit downwards to make the arc vertical
            center -= new Vector3(0, 8f, 0);

            // Interpolate over the arc relative to center
            Vector3 riseRelCenter = sunrise - center;
            Vector3 setRelCenter = sunset - center;

            // The fraction of the animation that has happened so far is
            // equal to the elapsed time divided by the desired time for
            // the total journey.
            float fracComplete = (Time.time - startTime) / journeyTime;

            transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
            transform.position += center;

        }
    }
}
