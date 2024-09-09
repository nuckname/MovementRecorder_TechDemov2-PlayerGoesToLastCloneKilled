using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject SMG;
    [SerializeField] private GameObject tronGun;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject grenadeLauncher;
    [SerializeField] private GameObject katana;
    [SerializeField] private GameObject spiralGun;
    [SerializeField] private GameObject knife;
    
    private List<GameObject> allWeapons = new List<GameObject>();
    
    [SerializeField] private Transform weaponHolder; // Change to Transform for easier management

    private void Start()
    {
        //Starting weapon
        //allWeapons.Add(grenadeLauncher);
        //allWeapons.Add(knife);        
        
        allWeapons.Add(pistol);
        allWeapons.Add(tronGun);
        allWeapons.Add(SMG);
        allWeapons.Add(spiralGun);        
        //allWeapons.Add(katana);

        DeactivateAllWeapons();

        allWeapons[0].SetActive(true);
    }

    public void GenerateRandomWeapon()
    {
        DeactivateAllWeapons();
        
        int randomIndex = UnityEngine.Random.Range(0, allWeapons.Count);
        print(randomIndex);
        GameObject selectedWeapon = allWeapons[randomIndex];
        
        selectedWeapon.SetActive(true);
    }

    private void DeactivateAllWeapons()
    {
        foreach (GameObject weapon in allWeapons)
        {
            weapon.SetActive(false);
        }
    }
}