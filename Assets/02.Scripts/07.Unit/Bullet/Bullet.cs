using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ReturnToPool_Particle
{

    [SerializeField] float dissableAfterTime = 5f;
    [SerializeField] float hitOffset = 0f;
    [SerializeField] bool UseFirePointRotation;
    float speed = 15f;
    float oroginalSpeed;
    Rigidbody rb;
    Collider sc;
    Light li;
    [SerializeField] GameObject[] Detached;

    public ReturnToPool_Particle hit; // datamanager gethit
    [SerializeField] Vector3 rotationOffset = new Vector3(0, 0, 0);
    [Space(10)]
    [SerializeField] BulletData bulletData;

    private RigidbodyConstraints originalConstraints;

    public Action<Vector3, Quaternion, ContactPoint, bool, Vector3> hitEventHandler;



    void FixedUpdate()
    {
        if (speed != 0)
        {
            rb.velocity = transform.forward * speed;
            //transform.position += transform.forward * (speed * Time.deltaTime);         
        }
    }

    public override void Init()
    {
        base.Init();
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<Collider>();
        li = GetComponent<Light>();

        if (li != null)
            li.enabled = false;
        sc.enabled = false;
        StopParticle();
        oroginalSpeed = speed;
        originalConstraints = rb.constraints;
        speed = 0;
    }

    public void SetData(BaseTowerData towerData, bool isSkill = false, Skill skill = null)
    {
        bulletData.data = towerData;
        if(isSkill)
        {
            bulletData.skill = skill;
        }

    }


    private IEnumerator LateCall()
    {
        yield return new WaitForSeconds(dissableAfterTime);
        StopParticle(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        //본인것만 처리
        sc.enabled = false;
        if (li != null)
            li.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0;
        yield break;
    }
    private IEnumerator TouchCall(GameObject detachedPrefab)
    {
        yield return new WaitForSeconds(1);
        detachedPrefab.transform.SetParent(gameObject.transform);
        detachedPrefab.transform.position = gameObject.transform.position;
        detachedPrefab.transform.rotation = gameObject.transform.rotation;
        yield break;
    }
    void OnTransformParentChanged()
    {
        if (li != null)
            li.enabled = true;
        sc.enabled = true;
        rb.constraints = originalConstraints;
        speed = oroginalSpeed;
        StartParticle();
        /*
        if (flash != null && useFlash)
        {
            //Instantiate flash effect on projectile position
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;

            //Destroy flash effect depending on particle Duration time
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
         */

        StartCoroutine(nameof(LateCall));
    }

    void OnCollisionEnter(Collision collision)
    {
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;
        speed = 0;
        sc.enabled = false;
        if (li != null)
            li.enabled = false;
        StopCoroutine(nameof(LateCall));
        StopParticle();
        if (Detached.Length > 0)
        {
            foreach (var detachedPrefab in Detached)
            {
                if (detachedPrefab != null)
                {
                    detachedPrefab.transform.parent = null;
                    StartCoroutine(TouchCall(detachedPrefab));
                }
            }
        }
       // systems.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        StopParticle(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * hitOffset;

        //Spawn hit effect on collision
        /*
         */
        hitEventHandler?.Invoke(pos, rot, contact, UseFirePointRotation, rotationOffset);

        if (hit != null)
        {
            var hitInstance = Instantiate(hit, pos, rot);
            if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
            else { hitInstance.transform.LookAt(contact.point + contact.normal); }

            //Destroy hit effects depending on particle Duration time
            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }

    }


}
