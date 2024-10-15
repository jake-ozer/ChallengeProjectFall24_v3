using UnityEngine;
using Random = UnityEngine.Random;

public class Shotgun : MonoBehaviour, IWeapon
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Animator gunAnim;
    [SerializeField] private float fireRate;
    [SerializeField] AudioClip shootSFX;
    private AudioSource gunSource;
    public Transform camTransform;
    public ShotgunPellet pelletPrefab;

    [SerializeField] private Vector3 _muzzleOffset = new Vector3(0, 1.0f, 0);
    [SerializeField] private uint _numPellets = 5;
    [SerializeField] private float _pelletSpeed = 50f;
    [SerializeField] private float _spreadAngle = 15f;
    

    private uint numPelletsSqrt;
    private float shootTimer;

    private void Start()
    {
        gunAnim = GetComponent<Animator>();
        camTransform = Camera.main.transform;
        numPelletsSqrt = (uint)Mathf.Sqrt(_numPellets);
    }

    public void Shoot()
    {
        
        if (shootTimer > 0) return;
        
        gunAnim.Play("ShotgunShoot");

        // gunSource.PlayOneShot(shootSFX);
        // gunAnim.Play("Shoot");
        
        gunAnim.SetTrigger("Shoot");

        Vector3 offset = new Vector3(0, 0, 0);

        Vector3 muzzlePosition = transform.TransformPoint(_muzzleOffset);
        for (uint i = 0; i < numPelletsSqrt; i++)
        {
            for (uint j = 0; j < numPelletsSqrt; j++)
            {
                offset.x = i * 0.02f;
                offset.y = j * 0.02f;
                ShotgunPellet pellet = Instantiate(pelletPrefab, muzzlePosition + offset, Quaternion.identity);
            
                Vector3 spreadDirection = Random.insideUnitSphere * Mathf.Tan(_spreadAngle * Mathf.Deg2Rad);
                Vector3 shootDirection = camTransform.forward + spreadDirection;
                shootDirection.Normalize();

                pellet.GetComponent<Rigidbody>().velocity = shootDirection * _pelletSpeed;
            }
        }

        shootTimer = fireRate;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;
    }
}