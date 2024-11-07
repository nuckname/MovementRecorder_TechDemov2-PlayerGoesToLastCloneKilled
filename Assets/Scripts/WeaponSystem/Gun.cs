using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject bulletPrefab;
    public static Action shootInput;
    [SerializeField] private Transform muzzle;

    public Camera fpsCam;
    public float baseSpread = 0.6f; 
    public float speedSpreadMultiplier = 2.0f; 
    public float mouseSpreadMultiplier = 0.5f; 

    private float timeSinceLastShot;
    public float baseShootForce = 35f; // Base force applied to bullets
    public float upwardForce;

    private Rigidbody playerRb;
    private float currentSpread;

    private Vector3 previousPosition;
    private Vector3 lastMousePosition;
    private float spreadXValue;
    private float spreadYValue;

    public Text AmmoCounter;

    [SerializeField] private Gun gun;

    private void Start()
    {
        if (fpsCam == null) {
            fpsCam = Camera.main;
        }

        if (muzzle == null)
        {
            muzzle = this.gameObject.transform.Find("Muzzle(dont rename)");
        }

        if (gun == null)
        {
            gun = FindObjectOfType<Gun>();
            if (gun == null)
            {
                Debug.LogError("Gun object could not be found in the scene.");
            }
        }

        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        playerRb = GetComponentInParent<Rigidbody>();
        
        spreadXValue = gunData.spreadMultiplierX;
        spreadYValue = gunData.spreadMultiplierY;

        previousPosition = playerRb.position;
        lastMousePosition = Input.mousePosition;

        AmmoCounter.text = $"{gunData.currentAmmo.ToString()} / {gunData.magSize.ToString()}";
        Debug.Log("Gun initialized with " + gunData.currentAmmo + " bullets.");
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload() {
        if (!gunData.reloading && this.gameObject.activeSelf) {
            Debug.Log("Reloading started.");
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload() {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        AmmoCounter.text = $"{gunData.currentAmmo.ToString()} / {gunData.magSize.ToString()}";
        
        Debug.Log("Reloading complete. Ammo refilled to " + gunData.magSize + " bullets.");
        gunData.reloading = false;
    }

    private bool CanShoot() {
        bool canShoot = !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
        if (!canShoot && gunData.reloading) {
            Debug.Log("Cannot shoot: currently reloading.");
        } else if (!canShoot) {
            Debug.Log("Cannot shoot: waiting for fire rate cooldown.");
        }
        return canShoot;
    }

    private void Shoot() {
        if (gunData.currentAmmo > 0 && CanShoot()) {
            Debug.Log("Shooting. Ammo left: " + gunData.currentAmmo);
            AmmoCounter.text = $"{gunData.currentAmmo.ToString()} / {gunData.magSize.ToString()}";
            ProjectileShoot();
        } else if (gunData.currentAmmo <= 0) {
            Debug.Log("Out of ammo. Need to reload.");
        }
    }

    private Vector3 lastPos;
    private void FixedUpdate()
    {
        timeSinceLastShot += Time.fixedDeltaTime;

        // Player movement-based spread
        if (playerRb != null) {
            float playerSpeed = playerRb.velocity.magnitude;
            currentSpread = baseSpread + Mathf.Pow(playerSpeed, 2) * speedSpreadMultiplier;
            Debug.Log("Player speed-based spread calculated: " + currentSpread);
        }

        // Mouse acceleration-based spread
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
        float mouseSpeed = mouseDelta.magnitude / Time.deltaTime;
        float mouseAcceleration = Mathf.Clamp(mouseSpeed * mouseSpreadMultiplier, 0, 1.0f);

        currentSpread += mouseAcceleration;
        Debug.Log("Mouse acceleration-based spread added. Total spread: " + currentSpread);

        lastPos = gameObject.transform.position;
        lastMousePosition = Input.mousePosition;
    }

    private void ProjectileShoot()
    {
        if (fpsCam == null)
        {
            fpsCam = Camera.main;
        }

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint = Physics.Raycast(ray, out RaycastHit hit) ? hit.point : ray.GetPoint(75);

        Vector3 directionWithoutSpread = targetPoint - muzzle.position;

        float spread = currentSpread;
        float x = UnityEngine.Random.Range(-spread, spread) * spreadXValue;
        float y = UnityEngine.Random.Range(-spread, spread) * spreadYValue;
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;

        Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            // Adjust shoot force based on mouse movement speed
            float adjustedShootForce = baseShootForce * MouseControlledPlayback.playbackSpeed;
            bulletRb.AddForce(directionWithSpread.normalized * adjustedShootForce, ForceMode.Impulse);
            Debug.Log("Bullet fired with adjusted force: " + adjustedShootForce + " in direction: " + directionWithSpread.normalized);
        }
        else
        {
            Debug.LogWarning("Bullet does not have a Rigidbody component.");
        }

        gunData.currentAmmo--;
        timeSinceLastShot = 0;
        OnGunShot();
    }

    private void OnGunShot() {
        Debug.Log("Gun shot fired.");
    }
}
