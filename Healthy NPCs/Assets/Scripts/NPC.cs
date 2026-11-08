using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float turnSpeed = 90f;

    private IHealth healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<IHealth>();
    }
    
    internal void TakeDamage(int amount)
    {
        healthSystem.TakeDamage(amount);
    }
    
    private void Update()
     {
         transform.position += transform.forward * moveSpeed * Time.deltaTime;
         transform.Rotate(0f, turnSpeed * Time.deltaTime, 0f);

         if (Input.GetKeyDown(KeyCode.Space))
         {
             TakeDamage(10);
         }
     }
}
