using System;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

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

    private float timeSinceLastShot;
    public float shootForce, upwardForce;

    private Rigidbody playerRb;
    private float currentSpread;

    //spread
    private Vector3 previousPosition;
    
    private float spreadXValue;
    private float spreadYValue;

    public Text AmmoCounter;
    
    //fix bug
    [SerializeField] private Gun gun;

    private void Start()
    {
        
        //Fixes bug that cannot reload on restarting scene 
        if (fpsCam == null) {
            fpsCam = Camera.main;
        }

        if (muzzle == null)
        {
            muzzle = this.gameObject.transform.Find("Muzzle(dont rename)");
        }
        
        if (gun == null)
        {
            gun = FindObjectOfType<Gun>();  // This assumes there's only one gun in the scene
            if (gun == null)
            {
                Debug.LogError("Gun object could not be found in the scene.");
            }
        }
        
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

        // Assuming the player's Rigidbody is on the same GameObject or a parent GameObject
        playerRb = GetComponentInParent<Rigidbody>();
        if (playerRb == null) {
        }

        spreadXValue = gunData.spreadMultiplierX;
        print(spreadXValue);
        spreadYValue = gunData.spreadMultiplierY;
        
        previousPosition = playerRb.position;
        
        //Ammo
        AmmoCounter.text = $"{gunData.currentAmmo.ToString()} / {gunData.magSize.ToString()}";
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload() {
        if (!gunData.reloading && this.gameObject.activeSelf) {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload() {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        //Ammo Reload
        AmmoCounter.text = $"{gunData.currentAmmo.ToString()} / {gunData.magSize.ToString()}";
        
        gunData.reloading = false;
    }

    private bool CanShoot() {
        bool canShoot = !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
        return canShoot;
    }

    private void Shoot() {
        if (gunData.currentAmmo > 0) {
            if (CanShoot()) {
                //Ammo UI text
                AmmoCounter.text = $"{gunData.currentAmmo.ToString()} / {gunData.magSize.ToString()}";
                ProjectileShoot();
            } else {
            }
        } else {
        }
    }


    private Vector3 lastPos;
    private void FixedUpdate()
    {
        timeSinceLastShot += Time.fixedDeltaTime;

        if(gameObject.transform.position != lastPos)
        {
            if (playerRb != null) {
                float playerSpeed = playerRb.velocity.magnitude;
                currentSpread = baseSpread + Mathf.Pow(playerSpeed, 2) * speedSpreadMultiplier; 
            }
        }
        else
        {
            float playerSpeed = playerRb.velocity.magnitude;
            currentSpread = 0;
        }

        lastPos = gameObject.transform.position;
    }

    private void ProjectileShoot()
    {
        if (fpsCam == null)
        {
            fpsCam = Camera.main;
        }
        
        // Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Just a ray through the middle of your current view
        RaycastHit hit;

        // Check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit)) {
            targetPoint = hit.point;
        } else {
            targetPoint = ray.GetPoint(75); // Just a point far away from the player
        }

        // Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - muzzle.position;

        // Use the current spread calculated in the Update method
        float spread = currentSpread;

        // Calculate spread
        float x = UnityEngine.Random.Range(-spread, spread) * spreadXValue;
        float y = UnityEngine.Random.Range(-spread, spread) * spreadYValue;

        // Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); // Just add spread to last direction

        // Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity); // Store instantiated bullet in currentBullet
        // Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        // Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);


        
        gunData.currentAmmo--;
        timeSinceLastShot = 0;
        OnGunShot();
    }

    private void OnGunShot() {
    }
}
