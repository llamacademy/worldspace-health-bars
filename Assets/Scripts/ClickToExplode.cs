using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class ClickToExplode : MonoBehaviour
{
    private Camera Camera;
    [SerializeField]
    private ParticleSystem ParticleSystemPrefab;
    public int MaxHits = 25;
    public float Radius = 10f;
    public LayerMask HitLayer;
    public LayerMask BlockExplosionLayer;
    public int MaxDamage = 50;
    public int MinDamage = 10;
    public float ExplosiveForce;

    private Collider[] Hits;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
        Hits = new Collider[MaxHits];
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Ray ray = Camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Instantiate(ParticleSystemPrefab, hit.point, Quaternion.identity);
                int hits = Physics.OverlapSphereNonAlloc(hit.point, Radius, Hits, HitLayer);

                for (int i = 0; i < hits; i++)
                {
                    float distance = Vector3.Distance(hit.point, Hits[i].transform.position);
                    int damage = Mathf.FloorToInt(Mathf.Lerp(MaxDamage, MinDamage, distance / Radius));
                    if (Hits[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody) && !Physics.Raycast(hit.point, (Hits[i].transform.position - hit.point).normalized, distance, BlockExplosionLayer.value))
                    {
                        rigidbody.AddExplosionForce(ExplosiveForce, hit.point, Radius);
                    }
                    if (Hits[i].TryGetComponent<IDamageable>(out IDamageable damageable))
                    {
                        damageable.OnTakeDamage(damage);
                    }
                }
            }
        }
    }
}