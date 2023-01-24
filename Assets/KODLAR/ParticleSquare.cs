using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ParticleSquare : MonoBehaviour
{
    public float range;
    public Light2D light2d;
    float c = 1;
    float smooth = 5.0f;
    float tiltAngle = 60.0f;
    private void Start()
    {
        smooth = Random.Range(1f, 10f);
        Vector2 randomPos = new Vector2(Random.Range(transform.position.x - range, transform.position.x + range), Random.Range(transform.position.y - range, transform.position.y + range));
        transform.position = randomPos;
        transform.rotation = new Quaternion(0, 0, Random.Range(0,360),0);

    }
    private void Update()
    {
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        if (c <= 0.01)
        {
            Destroy(this.gameObject);
        }
        c -= Time.deltaTime;
        transform.localScale = new Vector3(c,c,transform.localScale.z);
        light2d.intensity -= Time.deltaTime *2;

    }
}
