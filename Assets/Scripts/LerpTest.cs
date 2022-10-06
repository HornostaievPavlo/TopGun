using System.Collections;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public Vector3 startPosition;

    public Vector3 endPosition;

    public float duration;

    public float elapsedTime;

    // test

    public float movementTimer;

    public Vector3 newPosition;

    // test

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        //elapsedTime += Time.deltaTime;

        //float percentageComplete = elapsedTime / duration;

        //transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);

        // test

        if (transform.position != endPosition)
        {
            elapsedTime += Time.deltaTime;

            float percentageComplete = elapsedTime / duration;

            transform.position = Vector3.Lerp(startPosition, endPosition, percentageComplete);
        }
        else
        {
            Debug.Log("Priexali");

            StartCoroutine(ChangeEndLerpVector(newPosition));
        }
    }

    // test

    private IEnumerator ChangeEndLerpVector(Vector3 vector)
    {
        yield return new WaitForSeconds(movementTimer);

        endPosition = vector;

        elapsedTime = 0;

        startPosition = transform.position;
    }
}