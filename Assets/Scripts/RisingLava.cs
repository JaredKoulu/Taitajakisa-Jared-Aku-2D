using UnityEngine;

public class RisingLava : MonoBehaviour
{
    public float riseSpeed = 2f;         // laavan nousunopeus
    private bool shouldRise = false;     // nouseeko lava
    private float targetY;               // haluttu korkeus, ei pakollinen

    void Update()
    {
        if (shouldRise)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }

    // Kutsutaan triggeristä
    public void StartRising()
    {
        shouldRise = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Lose();
        }
    }

}
