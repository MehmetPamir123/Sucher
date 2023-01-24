using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftClickFire : MonoBehaviour
{
    public float rotateSpeed;

    float t;
    Vector3 startPosition;
    public Vector3 targetPosition;
    float timeToReachTarget;
    public void SetDestination(Vector3 destination, float time)
    {

        t = 0;
        startPosition = transform.position;
        timeToReachTarget = time;
        targetPosition = destination;
        StartCoroutine(DeleteMe(time));
        Debug.Log("I am ailve at " + startPosition + " for " + timeToReachTarget + " seconds. My goal is " + targetPosition);
    }
    void Update()
    {
        this.gameObject.transform.GetChild(0).transform.Rotate(Time.deltaTime * rotateSpeed, 0, 0);
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startPosition, targetPosition, t);
    }
    IEnumerator DeleteMe(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }
}
