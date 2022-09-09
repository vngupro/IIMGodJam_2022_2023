using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;

    public GameObject bulletPrefab;
    public GameObject bulletUlt;

    public float bulletForce = 20f;


    public static Shooting instance;

    [SerializeField] private int score;
    [SerializeField] private int scoreMinUlt;
    [SerializeField] private int scoreLost;
    [SerializeField] private float croissanceDifficulty;
    private bool ultimateMustache;

    [SerializeField] private ParticleSystem ultParticles;
    [SerializeField] private float buffTime;
    public bool underUlt;
    private float timeUltLeft;
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
            if (GameObject.FindGameObjectWithTag("Line").GetComponent<AILineShapeDetection>().shape == ShapeType.Circle && ultimateMustache)
            {
                ultimateMustache = false;
                scoreLost += score;
                score = 0;
                scoreMinUlt += (int)(scoreMinUlt * croissanceDifficulty / 100);
                Instantiate(ultParticles, transform.position, Quaternion.identity);
                underUlt = true;
                timeUltLeft = buffTime;
            }
        }
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>().score;

        if (score >= scoreMinUlt + scoreLost)
        {
            ultimateMustache = true;
        }

        if (timeUltLeft > 0)
        {
            timeUltLeft -= Time.deltaTime;
        }
        else
        {
            underUlt = false;

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
        
        if (underUlt)
        {
            GameObject bullet = Instantiate(bulletUlt, firePoint.position, Quaternion.Euler(0, 0, (-90 + rotationDegree)));

        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, (-90 + rotationDegree)));

        }


    }


}
