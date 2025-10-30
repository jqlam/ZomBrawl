using UnityEngine;

public enum WeaponType { Starter, SMG, Shotgun, Rifle, Heavy }

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint; public GameObject bulletPrefab;
    public float bulletSpeed = 14f, fireCooldown = 0.25f;
    public int bulletDamage = 10, shotgunPellets = 5; public float shotgunSpread = 12f;
    public WeaponType currentWeapon = WeaponType.Starter;
    float nextFire;

    void Update(){ AimAtMouse(); if(Input.GetMouseButton(0) && Time.time >= nextFire) Shoot(); }

    void AimAtMouse(){
        Vector3 mw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mw - transform.position); float ang = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(ang, Vector3.forward);
    }
    void Shoot(){
        nextFire = Time.time + fireCooldown;
        if(currentWeapon == WeaponType.Shotgun){
            for(int i=0;i<shotgunPellets;i++){ float off = Random.Range(-shotgunSpread*0.5f, shotgunSpread*0.5f); Fire(off, bulletDamage); }
        } else Fire(0f, bulletDamage);
    }
    void Fire(float angleOffset,int dmg){
        Quaternion rot = transform.rotation * Quaternion.Euler(0,0,angleOffset);
        GameObject b = Instantiate(bulletPrefab, firePoint.position, rot);
        b.GetComponent<Bullet>().Init(dmg, bulletSpeed, gameObject.tag);
    }
    public void SetWeapon(WeaponType t){
        currentWeapon=t;
        switch(t){
            case WeaponType.Starter: bulletDamage=10; bulletSpeed=14; fireCooldown=0.25f; shotgunPellets=1; shotgunSpread=0; break;
            case WeaponType.SMG:     bulletDamage=7;  bulletSpeed=16; fireCooldown=0.10f; shotgunPellets=1; shotgunSpread=0; break;
            case WeaponType.Shotgun: bulletDamage=6;  bulletSpeed=12; fireCooldown=0.65f; shotgunPellets=6; shotgunSpread=18; break;
            case WeaponType.Rifle:   bulletDamage=18; bulletSpeed=20; fireCooldown=0.40f; shotgunPellets=1; shotgunSpread=0; break;
            case WeaponType.Heavy:   bulletDamage=30; bulletSpeed=12; fireCooldown=0.85f; shotgunPellets=1; shotgunSpread=0; break;
        }
    }
}
