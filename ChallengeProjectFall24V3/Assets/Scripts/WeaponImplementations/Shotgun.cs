using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shotgun : MonoBehaviour, IWeapon
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Animator gunAnim;
    [SerializeField] private float fireRate;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] AudioClip slideSFX;
    public AudioSource gunSource;
    public Transform camTransform;
    public ShotgunPellet pelletPrefab;
    public ShotgunPellet fakePelletPrefab;

    [SerializeField] private Vector3 _muzzleOffset = new Vector3(0, 1.0f, 0);
    [SerializeField] private uint _numPellets = 5;
    [SerializeField] private float _pelletSpeed = 50f;
    [SerializeField] private float _spreadAngle = 15f;
    [SerializeField] private GameObject gunGFX;

    [SerializeField] public Sprite crosshairLoc;
    public AudioClip gunEquipSFX;

    public Sprite crosshair
    {
        get => crosshairLoc;
        set => crosshair = value;
    }

    private uint numPelletsSqrt;
    private float shootTimer;

    private void Start()
    {
        gunAnim = GetComponent<Animator>();
        camTransform = Camera.main.transform;
        numPelletsSqrt = (uint)Mathf.Sqrt(_numPellets);
        gunSource = transform.parent.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        gunSource.PlayOneShot(gunEquipSFX);
    }

    public void Shoot()
    {
        
        if (shootTimer > 0) return;
        
        gunAnim.Play("ShotgunShoot");

        gunSource.PlayOneShot(shootSFX);
        // gunAnim.Play("Shoot");
        
        //gunAnim.SetTrigger("Shoot");

        Vector3 muzzlePosition = transform.TransformPoint(_muzzleOffset);
        Vector3 fakePosition = camTransform.position;
        fakePosition.y -= 1;
        
        for (uint i = 0; i < _numPellets; i++)
        {
            ShotgunPellet pellet = Instantiate(pelletPrefab, muzzlePosition, Quaternion.identity);
            ShotgunPellet fakePellet = Instantiate(fakePelletPrefab, fakePosition, Quaternion.identity);
        
            Vector3 spreadDirection = Random.insideUnitSphere * Mathf.Tan(_spreadAngle * Mathf.Deg2Rad);
            Vector3 shootDirection = camTransform.forward + spreadDirection;
            shootDirection.Normalize();

            pellet.GetComponent<Rigidbody>().velocity = shootDirection * _pelletSpeed;
            fakePellet.GetComponent<Rigidbody>().velocity = shootDirection * _pelletSpeed;
        }

        shootTimer = fireRate;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;
    }

    public void ResetShootTimer()
    {
        shootTimer = 0;
    }

    public void ResetAnimState()
    {
        gunGFX.SetActive(false);
        gunAnim.SetTrigger("Reset");
    }

    public void ReEnableGFX()
    {
        gunGFX.SetActive(true);
    }

    //called by an animation event on the animator
    public void PlaySlideSFX()
    {
        gunSource.PlayOneShot(slideSFX);
    }
}