using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject lazer;
    [SerializeField] private Transform lazerSpawnPoint;

    private Animator myAnim;
    private Transform weaponCollider;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");
    
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        MouseFollowWithOffset();
    }
    
    public void Attack()
    {
        myAnim.SetTrigger(FIRE_HASH);
    }

    public void SpawnStaffProjectileAnimEvent()
    {
        GameObject newLazer = Instantiate(lazer, lazerSpawnPoint.position, Quaternion.identity);
        newLazer.GetComponent<MagicLazer>().UpdateLazerRange(weaponInfo.weaponRange);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition; // get the position of the mouse
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position); // get position of player object in relation to camera (e.g. bottom left 0,0)

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg; // returns angle in rad whose tan is y/x
        
        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
