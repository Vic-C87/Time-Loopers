using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100;
    [SerializeField] public float currentHealth;

    [SerializeField] Slider mySlider;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LooseGame()
    {
        Debug.Log("Game Over!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= other.gameObject.GetComponent<Enemy>().DoDamage();
            other.gameObject.GetComponent<Enemy>().Explode();
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                LooseGame();
            }
            mySlider.value = currentHealth / maxHealth;

        }
    }
}
