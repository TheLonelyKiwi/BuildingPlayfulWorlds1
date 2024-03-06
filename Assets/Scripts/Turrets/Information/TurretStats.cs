using UnityEngine;

[CreateAssetMenu]
public class TurretStats : ScriptableObject
{
    //all the turret stats
    public string turretName;

    public int health;

    public int maxAmmo;
    public float fireRate;
    public int bulletDamage;
    public float bulletVelocity;
    public float rotationSpeed;
    public float detectionRange;

    public GameObject bulletInstance;
}
