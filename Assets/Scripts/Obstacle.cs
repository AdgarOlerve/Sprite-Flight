using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject impactEffect;
    public float minSize = 0.5f;
    public float maxSize = 2f;
    public float minSpeed = 50f;
    public float maxSpeed = 200f;
    public float maxSpinSpeed = 100f;
    public float minTimeToNextPush = 3f;
    public float maxTimeToNextPush = 10f;
    private float timeToNextPush;
    private float elapsedTime;
    private float randomSize;
    private float randomSpeed;
    private float randomTorque;
    private Vector2 randomDirection;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        RandomizeAtribiutes();

        transform.localScale = new Vector3(randomSize, randomSize, 1f); // ustawia rozmiar
        rb.AddForce(randomDirection * (randomSpeed/randomSize)); //porusza obiektem w losowym kierunku z losową prędkością(uzależnioną od rozmiaru)
        rb.angularVelocity = randomTorque; //nadaje losowe obroty.
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= timeToNextPush)
        {
            RandomizeAtribiutes();
            rb.AddForce(randomDirection * randomSpeed); //porusza obiektem w losowym kierunku z losową siłą
            rb.angularVelocity = randomTorque; //nadaje losowe obroty.
            elapsedTime = 0;
        }
    }
    private void RandomizeAtribiutes()
    {
        randomDirection = Random.insideUnitCircle;  //ustala losowy kierunek
        randomSize = Random.Range(minSize, maxSize);    //ustala losową wielkość z danego przedziału.
        randomSpeed = Random.Range(minSpeed, maxSpeed); // ustaw losową prędkość z danego przedziału uzależnioną od wielkości.
        randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed); // ustaw losową prędkość obrotu
        timeToNextPush = Random.Range(minTimeToNextPush, maxTimeToNextPush); //ustawia losowy czas do następnego "pchnięcia" obiektu.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.GetContact(0).point;
        GameObject bounceEffect = Instantiate(impactEffect, contactPoint, Quaternion.identity);
        Destroy(bounceEffect, 1f);
    }
}
