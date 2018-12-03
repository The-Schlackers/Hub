using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsacUp4EnemyScript : MonoBehaviour
{
    Transform[] PlayerArray = new Transform[3];

    private bool Taunted;
    private Transform TauntPlayer;

    [SerializeField]
    private float Turntime;
    [SerializeField]
    private float Movespeed;

    private float MaxHealth = 100;
    private float CurrentHealth;

    private Slider HealthBar;
    public Slider SetHealthBar
    {
        set
        {
            HealthBar = value;
        }
    }

    private bool Stunned;

    private GameObject GM;
    public GameObject SetGm
    {
        set
        {
            GM = value;
        }
    }

    public float Movespeed1
    {
        get
        {
            return Movespeed;
        }

        set
        {
            Movespeed = value;
        }
    }

    public float MaxHealth1
    {
        get
        {
            return MaxHealth;
        }

        set
        {
            MaxHealth = value;
        }
    }



    private float CurrentDamageIncrese;
    [SerializeField]
    private float DamageIncreseTime;

    private float CurrentSlowIncrese;
    [SerializeField]
    private float SlowTime;

    public int Class = 0;

    void Start()
    {
        CurrentSlowIncrese = 1;
        CurrentDamageIncrese = 1;
        CurrentHealth = MaxHealth;

        Stunned = false;
        Taunted = false;
        MaxHealth += Time.time * 0.4f;

        GameObject[] PlayerArraygo = new GameObject[3];
        PlayerArraygo = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < PlayerArraygo.Length; i++)
        {
            PlayerArray[i] = PlayerArraygo[i].transform;

        }
    }
    private bool Spawnedshield = false;
    [SerializeField]
    private GameObject Shield;

    private bool SpawnedHealer = false;
    [SerializeField]
    private GameObject HealerDb;
    private GameObject DetectionHeal;

    private Transform[] Transformarray = new Transform[10];


    void FixedUpdate()
    {
        if (Class == 1 && !Spawnedshield)
        {
            Spawnedshield = true;
            GameObject go = Instantiate(Shield, transform.position, Quaternion.identity);

            go.GetComponent<IsacUp4HealthbarScript>().SetFollowPlayer = gameObject;
        }
        if (Class == 2 && !SpawnedHealer)
        {
            SpawnedHealer = true;
            StartCoroutine(Heal());
        }


        if (!Stunned)
        {
            try
            {
                #region Rotation

                //rotation
                Vector3 diff = Vector3.zero;
                if (Class == 2)
                {
                    Transformarray = Transform.FindObjectsOfType<Transform>();

                    Vector3 TransReturn = GetClosestAlly(Transformarray).position;

                    diff = TransReturn - transform.position;   
                    diff.Normalize();
                }
                else
                {
                    diff = GetClosestEnemy(PlayerArray).position - transform.position;
                    diff.Normalize();
                }


                //Rotate from .rotation, to target, over turntime, Lerp
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90 + FearInt), Turntime);

                #endregion

                GetComponent<Rigidbody2D>().AddForce(transform.up * Movespeed * Time.deltaTime * CurrentSlowIncrese);
            }
            catch
            {

            }

        }


        if (CurrentHealth <= 0)
        {
            Destroy(HealthBar.gameObject);
            Destroy(gameObject);
        }
    }

   

    private void OnCollisionEnter(Collision collision)
    {

        GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(1, 10), Random.Range(1, 10)));
    }

    Transform GetClosestEnemy(Transform[] enemies)
    {
        if (!Taunted)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach (Transform potentialTarget in enemies)
            {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr && !potentialTarget.GetComponent<IsacUp4PlayerScript>().GetDown)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
            return bestTarget;
        }
        else
        {
            return TauntPlayer;
        }



    }
    Transform GetClosestAlly(Transform[] enemies)
    {
        if (!Taunted)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach (Transform potentialTarget in enemies)
            {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr && potentialTarget.gameObject != gameObject && potentialTarget.tag == "Enemy" && potentialTarget.name != "Healer")
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

            //if (bestTarget = null) bestTarget.position = new Vector3(0, 0, 0) ;
            return bestTarget;
        }
        else
        {
            return TauntPlayer;
        }



    }
    public void SetHp()
    {
        HealthBar.value = CurrentHealth / MaxHealth;
    }

    public void DealDamage(float damage, Quaternion vec)
    {
        float DamageNumber = 1;
        if (Class == 1)
        {
            DamageNumber = Quaternion.Angle(transform.rotation, vec);

            if (DamageNumber >= 0 && DamageNumber <= 85)
                DamageNumber = 1f;
            else DamageNumber = 0.3f;
        }

        CurrentHealth -= damage * CurrentDamageIncrese * DamageNumber;
        SetHp();

        if (CurrentHealth <= 0)
        {
            Destroy(HealthBar.gameObject);
            Destroy(gameObject);
        }
    }

    public void Taunt(Transform trans)
    {
        TauntPlayer = trans;

        Taunted = true;

    }

    public void StartStun(float f)
    {
        StartCoroutine(Stun(f));
    }

    private IEnumerator Stun(float f)
    {
        Stunned = true;

        yield return new WaitForSeconds(f);

        Stunned = false;

        yield return null;
    }


    private int DmgInc = 0;

    public void StartDamageIncrese(float f)
    {
        DmgInc += 1;
        StartCoroutine(DamageIncrese(f));

    }
    private IEnumerator DamageIncrese(float f)
    {
        CurrentDamageIncrese = f;
        yield return new WaitForSeconds(DamageIncreseTime);

        if (DmgInc == 1)
            CurrentDamageIncrese = 1;

        DmgInc -= 1;
        yield return null;
    }


    private int SlowInc = 0;

    public void StartSlowIncrese(float f)
    {
        SlowInc += 1;
        StartCoroutine(SlowIncrese(f));

    }
    private IEnumerator SlowIncrese(float f)
    {
        CurrentSlowIncrese = f;
        yield return new WaitForSeconds(SlowTime);

        if (SlowInc == 1)
            CurrentSlowIncrese = 1;

        SlowInc -= 1;
        yield return null;
    }


    private int FearInt = 0;

    public void Fear(float f)
    {
        StartCoroutine(FearEnu(f));
    }

    private IEnumerator FearEnu(float f)
    {
        FearInt = 180;
        yield return new WaitForSeconds(f);


        FearInt = 0;

        yield return null;
    }


    [SerializeField]
    private GameObject Healgo;
    private IEnumerator Heal()
    {
        while (true)
        {
            GameObject Item = Instantiate(Healgo, transform.position, transform.rotation);

            yield return new WaitForSeconds(0.01f);

            if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
            {
                for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
                {
                    try
                    {
                        if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null && Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject != gameObject)
                        {
                            Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().DealDamage(-15,transform.rotation);
                        }
                    }
                    catch
                    {

                    }
                }
            }
            Destroy(Item, 0.2f);


            yield return new WaitForSeconds(3);
        }
    }
}
