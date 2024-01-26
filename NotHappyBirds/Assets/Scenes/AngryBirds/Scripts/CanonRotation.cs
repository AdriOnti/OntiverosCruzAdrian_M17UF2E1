using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation;
    public Vector3 _minRotation;
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }

    void Update()
    {

        var mousePos = Input.mousePosition;
        var dist = mousePos - transform.position;
        var ang = (Mathf.Atan2(dist.y, dist.x) * 360 / Mathf.PI + offset);
        transform.rotation = Quaternion.Euler(0f, 0f, ang);

        if (Input.GetMouseButton(0))
        {
            ProjectileSpeed += ProjectileSpeed * 4 * Time.deltaTime;
            CalculateBarScale(Mathf.Lerp(initialScaleX, 1, ProjectileSpeed / MaxSpeed));
        }

        if (Input.GetMouseButtonUp(0))
        {
            var projectile = Instantiate(Bullet, ShootPoint.transform.position, transform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = ShootPoint.transform.up * ProjectileSpeed;
            ProjectileSpeed = 1f;
            CalculateBarScale(0f);
        }

    }

    public void CalculateBarScale(float x)
    {
        PotencyBar.transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}