using System;
using System.Collections;
using UnityEngine;

public class SingleHitDrainHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int healthLostInOneHit = 2;
    [SerializeField] private float timeHealthDrainsFor = 5f;
    
    private int hitsRemaining;
    private bool canHealthDrain = true;

    public event Action<float> OnHPPctChanged = delegate(float f) { };
    public event Action OnDied = delegate { };

    public float CurrentHpPct
    {
        get { return (float) hitsRemaining / (float) healthLostInOneHit; }
    }

    private void Start()
    {
        hitsRemaining = healthLostInOneHit;
    }

    public void TakeDamage(int amount)
    {
        if (canHealthDrain)
        {
            StartCoroutine(DrainHealthOverTime());
            OnHPPctChanged(CurrentHpPct);
        }

        if (hitsRemaining <= 0)
        {
            OnDied();
        }
    }

    private IEnumerator DrainHealthOverTime()
    {
        // put health drain logic here?
    }
}
