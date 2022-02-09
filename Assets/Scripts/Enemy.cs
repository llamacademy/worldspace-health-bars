using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int Health = 100;
    [SerializeField]
    private ProgressBar HealthBar;

    private NavMeshAgent Agent;
    private float MaxHealth;

    private void Awake()
    {
        MaxHealth = Health;
        Agent = GetComponent<NavMeshAgent>();
    }

    public void OnTakeDamage(int Damage)
    {
        Health -= Damage;
        HealthBar.SetProgress(Health / MaxHealth, 3); 

        if (Health < 0)
        {
            OnDied();
            Agent.enabled = false;
        }
    }

    private void OnDied()
    {
        Destroy(gameObject, 1f);
        Destroy(HealthBar.gameObject, 1f);
    }

    public void SetupHealthBar(Canvas Canvas, Camera Camera)
    {
        HealthBar.transform.SetParent(Canvas.transform);
        if (HealthBar.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = Camera;
        }
    }
}
