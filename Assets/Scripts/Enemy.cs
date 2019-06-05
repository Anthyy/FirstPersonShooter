using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Namespace for implementing UI references (i.e, Sliders, Toggles, Buttons, etc)

public class Enemy : MonoBehaviour
{
    public GameObject healthBarUIPrefab; // Prefab to spawn in health bar parent
    public Transform healthBarParent; // Prefab parent to store health bar UI
    public Transform healthBarPoint;
    public int maxHealth = 100;

    private Renderer rend;
    private Slider healthSlider;
    private int health = 0;   

    // Start is called before the first frame update
    void Start()
    {
        // Set health to max health (100)
        health = maxHealth;
        // Spawn HealthBar UI into parent
        GameObject clone = Instantiate(healthBarUIPrefab, healthBarParent);
        healthSlider = clone.GetComponent<Slider>();
        // Get the Renderer component from this GameObject
        rend = GetComponent<Renderer>();
    }

    private void OnDestroy()
    {
        Destroy(healthSlider.gameObject);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Is the Renderer (MeshRenderer in this case) within the Camera's view
        if (rend.isVisible)
        {
            // Enable the health slider
            healthSlider.gameObject.SetActive(true);
            // Update position of health bar
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(healthBarPoint.position); // + offset
            healthSlider.transform.position = screenPosition;
        }
        else
        {
            // Disable the health slider
            healthSlider.enabled = false;
        }
        // Update value of slider
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        // Update value of slider
        healthSlider.value = health / maxHealth; // Converts 0-100 to 0-1 (current/max)
        // If health is zero
        if(health < 0)
        {
            // Destroy GameObject
            Destroy(gameObject);
        }
    }
}
