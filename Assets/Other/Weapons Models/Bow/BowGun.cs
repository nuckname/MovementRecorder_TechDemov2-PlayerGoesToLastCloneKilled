using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BowGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;  // You can rename this to BowData if needed.
    [SerializeField] private Transform cam;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform muzzle;

    public Camera fpsCam;
    public float minShootForce = 10f;  // Minimum force when tapping the shoot button.
    public float maxShootForce = 50f;  // Maximum force when fully charged.
    public float maxChargeTime = 2f;   // Time to reach max force.
    
    private float chargeTime;

    private Rigidbody playerRb;
    private bool isCharging;

    public Text AmmoCounter;

    private void Start()
    {
        PlayerShoot.shootInput += StartCharging;
        //PlayerShoot.releaseInput += Shoot;

        playerRb = GetComponentInParent<Rigidbody>();
        if (playerRb == null) {
            Debug.LogError("Player Rigidbody not found.");
        }

        AmmoCounter.text = $"{gunData.currentAmmo} / {gunData.magSize}";
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (!gunData.reloading && gameObject.activeSelf)
        {
            StartCoroutine(Reload());
        }
    }
    

    private IEnumerator Reload()
    {
        print("RELOADING");
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;

        AmmoCounter.text = $"{gunData.currentAmmo} / {gunData.magSize}";
        gunData.reloading = false;
    }

    private void StartCharging()
    {
        if (gunData.currentAmmo > 0 && !gunData.reloading)
        {
            isCharging = true;
            chargeTime = 0f;
        }
    }

    private void Shoot()
    {
        if (gunData.currentAmmo > 0 && isCharging)
        {
            isCharging = false;

            float appliedForce = Mathf.Lerp(minShootForce, maxShootForce, chargeTime / maxChargeTime);

            // Instantiate and shoot the arrow
            GameObject currentArrow = Instantiate(arrowPrefab, muzzle.position, Quaternion.identity);
            currentArrow.transform.forward = fpsCam.transform.forward;

            Rigidbody arrowRb = currentArrow.GetComponent<Rigidbody>();
            arrowRb.AddForce(fpsCam.transform.forward * appliedForce, ForceMode.Impulse);

            gunData.currentAmmo--;
            AmmoCounter.text = $"{gunData.currentAmmo} / {gunData.magSize}";

            chargeTime = 0f;
        }
    }

    private void Update()
    {
        if (isCharging)
        {
            chargeTime += Time.deltaTime;
            if (chargeTime > maxChargeTime)
            {
                chargeTime = maxChargeTime;  // Clamp the charge time to maxChargeTime.
            }
        }
        
    }
}
