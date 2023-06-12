using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    public GameObject coin;
    public int numberOfCoins = 3;
    public float coinSpawnHeight = 1.0f;
    public float coinSpawnDelay = 0.1f;
   // public AudioClip coinSound;

    private bool isActive = true;
    private int coinsLeft;
  //  private AudioSource audioSource;

    void Start()
    {
        coinsLeft = numberOfCoins;
      //  audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mario") && isActive)
        {
            if (coinsLeft > 0)
            {
                StartCoroutine(SpawnCoins());
            //    audioSource.PlayOneShot(coinSound);
                coinsLeft--;
            }
            else
            {
                isActive = false;
            }
        }
    }

    IEnumerator SpawnCoins()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + coinSpawnHeight, transform.position.z);
            GameObject newCoin = Instantiate(coin, spawnPosition, Quaternion.identity);
            newCoin.GetComponent<Rigidbody>().AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
            yield return new WaitForSeconds(coinSpawnDelay);
        }
    }
}
