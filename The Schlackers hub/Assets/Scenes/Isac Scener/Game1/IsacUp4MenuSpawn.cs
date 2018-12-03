using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsacUp4MenuSpawn : MonoBehaviour
{
    private float currentTime = 0;
    private float spawnTime = 4.5f;
    [SerializeField]
    private GameObject prefab;
    private Vector3 center;


    // Use this for initialization
    void Start ()
    {
        center = transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= spawnTime)
        {
            currentTime = 0;
            Vector3 pos = RandomCircle(center, 10f);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, center - pos);
            Instantiate(prefab, pos, rot );
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
