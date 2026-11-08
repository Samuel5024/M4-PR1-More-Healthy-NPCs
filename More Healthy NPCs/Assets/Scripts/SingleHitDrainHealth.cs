using System;
using System.Collections;
using UnityEngine;

public class SingleHitDrainHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private int healthLostInOneHit = 2;
    [SerializeField] private float timeHealthDrainsFor = 5f;

    private float currentHealth;

    public event Action<float> OnHPPctChanged = delegate(float f) { };
    public event Action OnDied = delegate { };

    public float CurrentHpPct
    {
        get { return currentHealth / maxHealth; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        StartCoroutine(DrainHealthOverTime());
    }

    private IEnumerator DrainHealthOverTime()
    {
        float elapsedTime = 0f;
        float damagePerSecond = healthLostInOneHit / timeHealthDrainsFor;
        
        while (elapsedTime < timeHealthDrainsFor && currentHealth > 0)
        {
            float timeStep = Time.deltaTime;
            elapsedTime += timeStep;

            currentHealth -= damagePerSecond * timeStep;

            if (currentHealth < 0) currentHealth = 0;

            OnHPPctChanged(CurrentHpPct);

            if (currentHealth <= 0)
            {
                OnDied();
                GameObject.Destroy(this.gameObject);
                yield break;
            }

            yield return null;
        }
    }
}
