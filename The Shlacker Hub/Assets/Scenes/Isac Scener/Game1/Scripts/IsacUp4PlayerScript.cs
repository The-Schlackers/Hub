using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsacUp4PlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject GameOverCanvas;

    [SerializeField]
    private GameObject ScreenShake;

    private CanvasGroup GoC;

    [SerializeField]
    private float DamageFromTouch;

    private int Player;
    public int Setplayer
    {
        set
        {
            Player = value;
        }
        get
        {
            return Player;
        }
    }


    private GameObject GM;

    [SerializeField]
    private float Speed;

    [SerializeField]
    private float SpeedBuff;

    public float CurrentSpeedBuff;

    [SerializeField]
    private float SpeedBuffDuration;

    private Slider HealthBar;
    public Slider SetHealthBar
    {
        set
        {
            HealthBar = value;
        }
    }

    public float Health;
    [SerializeField]
    private float MaxHealth;

    [SerializeField]
    private List<GameObject> Abilitys = new List<GameObject>();
    [SerializeField]
    private List<float> AbilityCooldowns = new List<float>();
    private float[] CurrentCooldown;

    [SerializeField]
    private float Dps1DashPushForce;
    [SerializeField]
    private float Dps2DashPushForce;
    [SerializeField]
    private float Dps1Damage;
    [SerializeField]
    private float Dps2Damage;

    [SerializeField]
    private float TauntDamage;
    [SerializeField]
    private float DamageReductionDuration;
    [SerializeField]
    private float DamageReductionProcent;
    private bool DamageReductionOn;


    [SerializeField]
    private float Heal1;
    [SerializeField]
    private float HealingPushForce;
    [SerializeField]
    private float Heal1Damage;

    [SerializeField]
    private float HealingTicks;
    [SerializeField]
    private float HealingPerTick;
    [SerializeField]
    private float WaitPerTick;


    [SerializeField]
    private float StunDamage;

    [SerializeField]
    private float StunnedTime;

    private bool Invincible;
    private bool Down;
    public bool GetDown
    {
        get
        {
            return Down;
        }
    }

    [SerializeField]
    private float SecondsDown;


    [SerializeField]
    private float dps1xheal2Dmg;
    [SerializeField]
    private float dps1xheal2Push;
    [SerializeField]
    private float DmgTicks;
    [SerializeField]
    private float DmgWaitPerTick;


    [SerializeField]
    private float dps1xtank2Dmg;
    [SerializeField]
    private float dps1xtank2DmgReduce;

    [SerializeField]
    private float dps1xsupp2Dmg;
    [SerializeField]
    private float dps1xsupp2Slow;

    [SerializeField]
    private float TxHFearDuration;

    [SerializeField]
    private float TankxSuppPushForce;
    [SerializeField]
    private float TankxSuppdmg;

    [SerializeField]
    private float TankxSuppStunTime;









    void Start()
    {
        ScreenShake = GameObject.FindGameObjectWithTag("MainCamera");
        CurrentCooldown = new float[AbilityCooldowns.Count];

        Down = false;
        Invincible = false;
        CurrentSpeedBuff = 1;
        Health = MaxHealth;
        SetHp();

        for (int i = 0; i < CurrentCooldown.Length; i++)
        {
            CurrentCooldown[i] = 0;
        }
        DamageReductionOn = false;
    }

    void FixedUpdate()
    {
        if (!Down)
        {
            #region Movement

            //Movement Mechanics

            //new vec 2 from horizontal and vertical axis per player, Set ut in inputmanager
            Vector2 dir = Vector3.zero;
            dir.x = Input.GetAxis("Player" + Player + " Horizontal");
            dir.y = Input.GetAxis("Player" + Player + " Vertical");

            //Limits movement to be "1" if both right an up are pressed, or one by themselves // Can be optimised with SqrDist instead of distance
            if (dir != Vector2.zero)
                dir = dir / Vector2.Distance(dir, Vector2.zero);

            //Change pos dep on previous steps
            //transform.position += ((Vector3.up * dir.y) + (Vector3.right * dir.x)) * Speed * Time.deltaTime;
            GetComponent<Rigidbody2D>().AddForce(((Vector3.up * dir.y) + (Vector3.right * dir.x)) * Speed * CurrentSpeedBuff * Time.deltaTime);

            //Rotation set to direction of movement (2d graphics)
            if (dir.x != 0 || dir.y != 0)
                transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);

            #endregion
        }

    }

    void Update()
    {


        if (Health > MaxHealth)
        {
            Health = MaxHealth;
            SetHp();
        }
        if (Health <= 0 && !Down)
        {
            Down = true;
            StartCoroutine(Downstate());
        }

        for (int i = 0; i < CurrentCooldown.Length; i++)
        {
            CurrentCooldown[i] -= Time.deltaTime;
        }

        if (!Down)
        {

            //Abilitys

            //new bools from inputs from inputmanager depending on curent player
            bool fire1 = Input.GetButtonDown("Player" + Player + " Ability1");
            bool fire2 = Input.GetButtonDown("Player" + Player + " Ability2");

            switch (PlayerPrefs.GetInt("Player" + Player))
            {

                case 1: //Red Class / Dps
                    if (fire1)
                    {
                        if (CurrentCooldown[0] <= 0)
                        {
                            StartCoroutine(DpsAbility1());
                            CurrentCooldown[0] = AbilityCooldowns[0];
                        }
                    }
                    else if (fire2)
                    {
                        if (CurrentCooldown[1] <= 0)
                        {
                            GetComponent<Rigidbody2D>().AddForce(transform.up * 10000);
                            StartCoroutine(DpsAbility2());
                            CurrentCooldown[1] = AbilityCooldowns[1];
                        }
                    }
                    break;

                case 2: //Blue Class / Tank
                    if (fire1)
                    {
                        if (CurrentCooldown[2] <= 0)
                        {
                            StartCoroutine(TankAbility1());
                            CurrentCooldown[2] = AbilityCooldowns[2];
                        }

                    }
                    else if (fire2)
                    {
                        if (CurrentCooldown[3] <= 0)
                        {
                            StartCoroutine(TankAbility2());
                            CurrentCooldown[3] = AbilityCooldowns[3];
                        }
                    }
                    break;

                case 3: //Green Class / Heal
                    if (fire1)
                    {
                        if (CurrentCooldown[4] <= 0)
                        {
                            StartCoroutine(HealingAbility1());
                            CurrentCooldown[4] = AbilityCooldowns[4];
                        }
                    }
                    else if (fire2)
                    {
                        if (CurrentCooldown[5] <= 0)
                        {
                            StartCoroutine(HealingAbility2());
                            CurrentCooldown[5] = AbilityCooldowns[5];
                        }
                    }
                    break;

                case 4: //Yellow Class / Support
                    if (fire1)
                    {
                        if (CurrentCooldown[6] <= 0)
                        {
                            StartCoroutine(SupportAbility1());
                            CurrentCooldown[6] = AbilityCooldowns[6];
                        }
                    }
                    else if (fire2)
                    {
                        if (CurrentCooldown[7] <= 0)
                        {
                            StartCoroutine(SupportAbility2());
                            CurrentCooldown[7] = AbilityCooldowns[7];
                        }
                    }
                    break;
            }
        }
    }

    public void SetHp()
    {
        HealthBar.value = Health / MaxHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!Invincible && !Down)
        {
            if (collision.collider.tag == "Enemy")
            {
                if (CurrentCooldown[8] <= 0)
                {

                    CurrentCooldown[8] = AbilityCooldowns[8];
                    if (!DamageReductionOn)
                    {
                        Health -= DamageFromTouch;
                    }
                    else
                    {
                        Health -= DamageFromTouch * DamageReductionProcent;
                    }
                    SetHp();
                }

            }
        }

    }

    private void DeactivateInvincible()
    {
        Invincible = false;
    }

    private IEnumerator DpsAbility1()
    {

        ScreenShake.GetComponent<IsacUp4ScreenShake>().TriggerShake(.4f, .2f, 10);
        GameObject Item = Instantiate<GameObject>(Abilitys[0], transform.position + transform.up * 2, transform.rotation);
        yield return new WaitForSeconds(0.08f);


        for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
        {

            try
            {

                if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null)
                {

                    Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().DealDamage(Dps1Damage, transform.rotation);

                    Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<Rigidbody2D>().AddForce(-(transform.position - Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<Transform>().position) * Dps1DashPushForce);

                }
                else if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject.tag == "Heal2")
                {
                    if (CurrentCooldown[9] <= 0)
                    {
                        StartCoroutine(Dps1xHeal2(Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject.transform));
                        CurrentCooldown[9] = AbilityCooldowns[9];
                    }
                }
                else if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject.tag == "Tank2")
                {

                    StartCoroutine(Dps1xTank2(Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject.transform));
                }
                else if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject.tag == "Tank2")
                {

                    StartCoroutine(Dps1xsupp2(Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject.transform));
                }

            }
            catch
            {

            }
        }
        Destroy(Item, 0.2f);
        yield return null;
    }

    private IEnumerator DpsAbility2()
    {
        Invincible = true;
        GameObject Item = Instantiate<GameObject>(Abilitys[7], transform.position + transform.up * 1.5f, transform.rotation);
        yield return new WaitForSeconds(0.08f);

        if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
        {
            for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
            {
                try
                {
                    if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null)
                    {
                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().DealDamage(Dps2Damage, Item.transform.rotation);

                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<Rigidbody2D>().AddForce(-(transform.position - Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<Transform>().position) * Dps2DashPushForce);

                    }

                }
                catch
                {

                }
            }
        }
        Invoke("DeactivateInvincible", 0.5f);
        Destroy(Item, 0.2f);

        yield return null;
    }


    private IEnumerator Dps1xHeal2(Transform trans)
    {

        ScreenShake.GetComponent<IsacUp4ScreenShake>().TriggerShake(4f, .06f, 6);
        GameObject Item = Instantiate<GameObject>(Abilitys[9], trans.position, trans.rotation);
        yield return new WaitForSeconds(0.1f);

        if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
        {
            for (int u = 0; u < DmgTicks; u++)
            {
                for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
                {
                    try
                    {
                        if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null)
                        {
                            Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().DealDamage(dps1xheal2Dmg, Item.transform.rotation);

                            Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<Rigidbody2D>().AddForce(-(transform.position - Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<Transform>().position) * dps1xheal2Push);

                        }


                    }
                    catch
                    {

                    }

                }
                yield return new WaitForSeconds(DmgWaitPerTick);
            }

            Destroy(Item);

            yield return null;
        }
        Destroy(Item, 0.2f);
        yield return null;
    }

    private IEnumerator Dps1xTank2(Transform trans)
    {

        ScreenShake.GetComponent<IsacUp4ScreenShake>().TriggerShake(0.2f, .1f, 6);
        GameObject Item = Instantiate<GameObject>(Abilitys[10], trans.position, trans.rotation);
        yield return new WaitForSeconds(0.1f);


        for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
        {
            try
            {
                if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null)
                {
                    Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().DealDamage(dps1xtank2Dmg, Item.transform.rotation);

                    Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().StartDamageIncrese(dps1xtank2DmgReduce);

                }


            }
            catch
            {

            }
        }
        Destroy(Item, 0.2f);
        yield return null;
    }

    private IEnumerator Dps1xsupp2(Transform trans)
    {

        ScreenShake.GetComponent<IsacUp4ScreenShake>().TriggerShake(0.2f, .1f, 6);
        GameObject Item = Instantiate<GameObject>(Abilitys[9], trans.position, trans.rotation);
        yield return new WaitForSeconds(0.1f);


        for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
        {
            try
            {
                if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null)
                {
                    Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().DealDamage(dps1xsupp2Dmg, Item.transform.rotation);
                    Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().StartSlowIncrese(dps1xsupp2Slow);

                }


            }
            catch
            {

            }
        }
        Destroy(Item, 0.2f);
        yield return null;
    }



    private IEnumerator TankAbility1()
    {
        GameObject Item = Instantiate<GameObject>(Abilitys[1], transform.position + transform.up * 3, transform.rotation);

        yield return new WaitForSeconds(0.1f);

        if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
        {
            for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
            {
                try
                {
                    if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null)
                    {
                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().Taunt(transform);
                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().DealDamage(TauntDamage, transform.rotation);
                    }
                    else if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject.tag == "Heal2")
                    {
                        if (CurrentCooldown[10] <= 0)
                        {
                            StartCoroutine(TankxHeal2(Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject.transform));
                            CurrentCooldown[10] = AbilityCooldowns[10];
                        }
                    }
                    else if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject.tag == "Supp2")
                    {

                        StartCoroutine(TankxSupp2(Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject.transform));

                    }
                }
                catch
                {

                }
            }
        }
        Destroy(Item, 0.2f);
        yield return null;
    }

    private IEnumerator TankAbility2()
    {
        GameObject Item = Instantiate<GameObject>(Abilitys[2], transform.position, transform.rotation);

        yield return new WaitForSeconds(0.1f);

        if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
        {
            for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
            {
                try
                {
                    if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>() != null)
                    {
                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().StartDamageReduction();
                    }
                }
                catch
                {

                }
            }
        }
        Destroy(Item, 1.2f);
        yield return null;
    }


    private IEnumerator TankxHeal2(Transform trans)
    {
        GameObject Item = Instantiate<GameObject>(Abilitys[11], trans.position, trans.rotation);

        yield return new WaitForSeconds(0.1f);

        if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
        {
            for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
            {
                try
                {
                    if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null)
                    {
                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().Fear(TxHFearDuration);
                    }
                }
                catch
                {

                }
            }
        }
        Destroy(Item, 0.2f);
        yield return null;
    }

    private IEnumerator TankxSupp2(Transform trans)
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 10000);

        yield return new WaitForSeconds(0.5f);
        GameObject Item = Instantiate<GameObject>(Abilitys[12], transform.position + transform.up * 2, transform.rotation);

        ScreenShake.GetComponent<IsacUp4ScreenShake>().TriggerShake(.4f, .6f, 10);
        yield return new WaitForSeconds(0.1f);

        if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
        {
            for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
            {
                try
                {
                    if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null)
                    {
                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<Rigidbody2D>().AddForce(transform.up * TankxSuppPushForce);
                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().DealDamage(TankxSuppdmg, transform.rotation);
                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().StartStun(StunnedTime);
                    }
                }
                catch
                {

                }
            }
        }
        Destroy(Item, 0.2f);
        yield return null;
    }


    public void StartDamageReduction()
    {
        StartCoroutine(DamageReductionAbility());
    }
    private IEnumerator DamageReductionAbility()
    {
        DamageReductionOn = true;

        yield return new WaitForSeconds(DamageReductionDuration);

        DamageReductionOn = false;

        yield return null;
    }



    private IEnumerator HealingAbility1()
    {
        GameObject Item = Instantiate<GameObject>(Abilitys[3], transform.position + transform.up * 2.4f, transform.rotation);

        yield return new WaitForSeconds(0.01f);


        for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
        {
            try
            {
                if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>() != null && Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().Setplayer != Player)
                {
                    Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().Health += Heal1;
                    Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().SetHp();

                }
                else if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null)
                {
                    Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().DealDamage(Heal1Damage, transform.rotation);
                }
                else if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].gameObject.tag == "Supp2")
                {
                    if (CurrentCooldown[11] <= 0)
                    {
                        StartCoroutine(Healxsupp());
                        CurrentCooldown[11] = AbilityCooldowns[11];
                    }

                }
            }
            catch
            {
                Destroy(Item, 0.2f);
            }
        }
        Destroy(Item, 0.2f);
        yield return null;
    }

    private IEnumerator HealingAbility2()
    {
        GameObject Item = Instantiate<GameObject>(Abilitys[4], transform.position, transform.rotation);

        yield return new WaitForSeconds(0.1f);
        if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
        {
            for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
            {
                try
                {
                    if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null)
                    {

                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<Rigidbody2D>().AddForce(-(transform.position - Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<Transform>().position) * HealingPushForce);
                    }
                }
                catch
                {

                }
            }
        }

        if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
        {
            for (int u = 0; u < HealingTicks; u++)
            {
                if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
                {
                    for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
                    {
                        try
                        {
                            if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>() != null)
                            {
                                Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().Health += HealingPerTick;
                                Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().SetHp();
                            }
                        }
                        catch
                        {

                        }

                    }
                }
                yield return new WaitForSeconds(WaitPerTick);
            }

            Destroy(Item, 0.2f);
        }

        yield return null;
    }

    [SerializeField]
    private float HealxSuppMovespeed;
    [SerializeField]
    private float HealxSuppTime;
    [SerializeField]
    private float HealxSuppHealing;

    private IEnumerator Healxsupp()
    {

        ScreenShake.GetComponent<IsacUp4ScreenShake>().TriggerShake(0.2f, .04f, 6);
        Vector3 pos = transform.position;
        float localtimeStart = Time.time;
        float localtime = 0;

        while (localtime < HealxSuppTime)
        {
            localtime = Time.time - localtimeStart;

            GameObject[] Item = new GameObject[4];

            for (int u = 0; u < 4; u++)
            {
                Item[u] = Instantiate<GameObject>(Abilitys[13], pos, Quaternion.Euler(0, 0, 90 * u));
                Item[u].transform.position += Item[u].transform.up * localtime * HealxSuppMovespeed;
            }
                yield return new WaitForSeconds(0.1f);
            for(int u = 0; u < 4; u++)
            {
                for (int i = 0; i < Item[u].GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
                {
                    try
                    {
                        if (Item[u].GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>() != null)
                        {
                            Item[u].GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().Health += HealxSuppHealing;
                            Item[u].GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().SetHp();
                        }
                    }
                    catch
                    {

                    }
                }
            }
                
            foreach(GameObject g in Item)
                Destroy(g, 0.1f);
            
          
        }


        yield return null;
    }


    private IEnumerator SupportAbility1()
    {
        GameObject Item = Instantiate<GameObject>(Abilitys[5], transform.position, transform.rotation);

        yield return new WaitForSeconds(0.01f);

        if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
        {
            for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
            {
                try
                {
                    if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>() != null)
                    {
                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().StartStun(StunnedTime);

                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4EnemyScript>().DealDamage(StunDamage, transform.rotation);
                    }
                }
                catch
                {

                }
            }
        }
        Destroy(Item, 0.2f);
        yield return null;
    }

    private IEnumerator SupportAbility2()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject Item = Instantiate<GameObject>(Abilitys[6], transform.position + transform.up * 3, transform.rotation);

        yield return new WaitForSeconds(0.05f);

        if (Item.GetComponent<IsacUp4GetTargets>().Getlist != null)
        {
            for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
            {
                try
                {
                    if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>() != null)
                    {
                        Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().CurrentSpeedBuff = SpeedBuff;
                    }
                }
                catch
                {

                }
            }
        }

        yield return new WaitForSeconds(SpeedBuffDuration);

        for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
        {
            try
            {
                if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>() != null)
                {
                    Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().CurrentSpeedBuff = 1;
                }
            }
            catch
            {

            }
        }

        Destroy(Item, 0.2f);
        yield return null;
    }



    [SerializeField]
    private GameObject ReviveSliderGo;
    [SerializeField]
    private float ReviveTime;


    private IEnumerator Downstate()
    {
        float CurrentReviveProcent = 0;

        GameObject ReviveBarGo = Instantiate<GameObject>(ReviveSliderGo, transform.position, Quaternion.Euler(0, 0, 0));
        ReviveBarGo.GetComponent<IsacUp4HealthbarScript>().SetFollowPlayer = gameObject;

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;


        ReviveBarGo.GetComponentInChildren<Slider>().value = CurrentReviveProcent;

        Health = MaxHealth;
        SetHp();


        bool Gameover = false;
        while (Down || !Gameover)
        {

            GameObject Item = Instantiate<GameObject>(Abilitys[8], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);

            bool FoundFriendly = false;

            for (int i = 0; i < Item.GetComponent<IsacUp4GetTargets>().Getlist.Count; i++)
            {
                try
                {
                    if (Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>() != null && Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().Setplayer != Player && !Item.GetComponent<IsacUp4GetTargets>().Getlist[i].GetComponent<IsacUp4PlayerScript>().GetDown)
                    {
                        FoundFriendly = true;
                        CurrentReviveProcent += 1 / ReviveTime;

                        ReviveBarGo.GetComponentInChildren<Slider>().value = CurrentReviveProcent;

                    }
                }
                catch
                {

                }
            }
            if (CurrentReviveProcent >= 1)
            {
                Destroy(ReviveBarGo);
                Health = MaxHealth / 2;
                SetHp();

                Down = false;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                Destroy(Item);


                break;
            }
            else if (Health > 0 && FoundFriendly == false)
            {
                Health -= MaxHealth / SecondsDown;

                SetHp();
            }
            else if (Health <= 0)
            {
                Gameover = true;
                GM.GetComponent<IsacUp4GameScript>().GameOver();
                break;
            }

            yield return new WaitForSeconds(0.9f);

            Destroy(Item, 0.1f);
        }

        yield return null;
    }


    public GameObject SetGm
    {
        set
        {
            GM = value;
        }
    }


}
