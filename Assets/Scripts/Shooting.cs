using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;

    public float bulletForce = 20f;


    public static Shooting instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Shooting");
            return;
        }
        instance = this;
    }


    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("Line") != null)
        {
            if (GameObject.FindGameObjectWithTag("Line").GetComponent<AILineShapeDetection>().shape == ShapeType.Line && GameObject.FindGameObjectWithTag("Line").GetComponent<AILineShapeDetection>().shot)
            {
                Vector3 difference = GameObject.FindGameObjectWithTag("Line").GetComponent<AILineShapeDetection>().positions[0] - GameObject.FindGameObjectWithTag("Line").GetComponent<AILineShapeDetection>().positions[GameObject.FindGameObjectWithTag("Line").GetComponent<AILineShapeDetection>().positions.Count - 1];
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                GameObject.FindGameObjectWithTag("Line").GetComponent<AILineShapeDetection>().shot = false;
                ShootWithLine(rotZ);
            }
        }


    }



    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);      

    }
    void ShootWithLine(float rotationDegree)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, (-90 + rotationDegree)));


    }


}
