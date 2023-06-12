using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    public GameObject smallCube;
    public int numberOfCubes = 6;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mario"))
        {
            Break();
        }
    }

    private void Break()
    {
        for (int i = 0; i < numberOfCubes; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere;
            GameObject cube = Instantiate(smallCube, spawnPosition, Quaternion.identity);
            cube.GetComponent<Rigidbody>().AddExplosionForce(100.0f, transform.position, 10.0f);
        }
        Destroy(gameObject);
    }
}
