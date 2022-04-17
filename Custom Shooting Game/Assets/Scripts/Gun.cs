using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reload
    }
    public State state
    {
        get; private set;
    }

    public int ammoRemain
    {
        get; private set;
    }
    public int magCapacity
    {
        get; private set;
    }
    public int magAmmo
    {
        get; private set;
    }
    [SerializeField] Transform fireTransform;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] AudioClip shotClip;
    [SerializeField] AudioClip reloadClip;

    public float reloadTime = 1.5f;

    private LineRenderer bulletLineRenderer;
    private PlayerInput playerInput;
    private float lastAttackTime;
    private float delay;
    private float gunDamage;
    private AudioSource gunAudio;


    private void Awake()
    {
        bulletLineRenderer = GetComponent<LineRenderer>();
        playerInput = GetComponentInParent<PlayerInput>();
        gunAudio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = true;
        state = State.Ready;
        ammoRemain = 100;
        magCapacity = 30;
        magAmmo = magCapacity;
        lastAttackTime = 0;
        UIManager.instance.UpdateAmmoText(magAmmo, magCapacity, ammoRemain);
    }
    private void OnEnable()
    {
        UIManager.instance.UpdateAmmoText(magAmmo, magCapacity, ammoRemain);
        lastAttackTime = 0;
    }
    private void GunRotate()
    {
        //총이 발사 방향을 바라보게함
        float mPosX = playerInput.mousePos.x - transform.position.x;
        float mPosY = playerInput.mousePos.y - transform.position.y;
        float angle = Mathf.Atan2(mPosY, mPosX) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void Fire()
    {
        if(state == State.Ready && Time.time >= lastAttackTime + GameManager.instance.player_RPM)
        {
            lastAttackTime = Time.time;
            gunAudio.PlayOneShot(shotClip);
            Shot();
            GunRotate();
        }
    }
    private void Shot()
    {
        RaycastHit2D hit;
        Vector2 hitPoint = Vector2.zero;
        Vector2 firePoint = playerInput.mousePos - (Vector2)fireTransform.position;
        hit = Physics2D.Raycast(fireTransform.position, firePoint, maxDistance,LayerMask.GetMask("Enemy"));
        if (hit)
        {
            hitPoint = hit.point;
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                Vector2 knockDir = (hitPoint - (Vector2)transform.position).normalized;
                enemy.OnDamaged(GameManager.instance.player_Damage,knockDir,0.05f,10);
            }
        }
        else
        {
            Vector2 firePos = fireTransform.position;

            hitPoint = firePos + firePoint.normalized * maxDistance;

        }
        magAmmo--;
        if (magAmmo <= 0) state = State.Empty;

        UIManager.instance.UpdateAmmoText(magAmmo, magCapacity, ammoRemain);
        StartCoroutine(ShotEffect(hitPoint));
    }
    IEnumerator ShotEffect(Vector2 hitPoint)
    {
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPoint);

        bulletLineRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);
        bulletLineRenderer.enabled = false;
    }
    public void Reload()
    {
        if(state != State.Reload && magAmmo < magCapacity && ammoRemain > 0)
        {
            UIManager.instance.ActiveReloadSlider(reloadTime);
            StartCoroutine(FillAmmo());
            gunAudio.PlayOneShot(reloadClip);
        }
    }
    IEnumerator FillAmmo()
    {
        state = State.Reload;
        yield return new WaitForSeconds(reloadTime);

        int ammoFill = magCapacity - magAmmo;

        if (ammoRemain < ammoFill) ammoFill = ammoRemain;
        ammoRemain -= ammoFill;

        magAmmo += ammoFill;
        state = State.Ready;
        UIManager.instance.UpdateAmmoText(magAmmo, magCapacity, ammoRemain);
    }
    public void AddAmmoRemain(int newAmmo) 
    {
        ammoRemain += newAmmo;
        UIManager.instance.UpdateAmmoText(magAmmo, magCapacity, ammoRemain);
    }
}
