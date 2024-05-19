using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    Transform parent;

    Dictionary<string, Queue<ReturnToPool_Particle>> bulletAroundPool = new Dictionary<string, Queue<ReturnToPool_Particle>>();
    Dictionary<string, Queue<Bullet>> bulletPool = new Dictionary<string, Queue<Bullet>>();

    public void Init()
    {
        if(parent == null)
            parent = InfinityStageManager.Instance.ObjectPoolingController.UnitTypeParent[(int)UnitType.bullet];

    }



    #region Bullet Around
    public void CreateBulletAround(string key, Action<ReturnToPool_Particle> endCall = null)
    {
        if (string.IsNullOrEmpty(key))
            return;

        DataManager.Instance.GetGameObject(key, (obj) =>
        {
            SetBulletAround(obj, key, endCall);
        });
    }

    void SetBulletAround(GameObject obj, string key, Action<ReturnToPool_Particle> endCall = null)
    {
        var particle = obj.GetComponent<ReturnToPool_Particle>();
        particle.Init();
        particle.particleStoppedEventHandler += () => BindParticleStoppedEvent(particle, key);

        obj.transform.SetParent(parent);

        BindParticleStoppedEvent(particle, key);

        endCall?.Invoke(particle);

    }

    private void BindParticleStoppedEvent(ReturnToPool_Particle particle, string key)
    {
        if (bulletAroundPool.ContainsKey(key) == false)
        {
            bulletAroundPool.Add(key, new Queue<ReturnToPool_Particle>());
        }

        particle.gameObject.SetActive(false);
        bulletAroundPool[key].Enqueue(particle);
    }


    public void GetBulletAroundPool(string key, Action<ReturnToPool_Particle> endCall = null)
    {
        if (string.IsNullOrEmpty(key))
            return;

        if (bulletAroundPool[key].Count > 0)
        {
            var bulletAround = bulletAroundPool[key].Dequeue();
            bulletAround.gameObject.SetActive(true);
            endCall?.Invoke(bulletAround);
        }
        else
        {
            CreateBulletAround(key, (particle) =>
            {
                GetAroundBullet(key, endCall);
            });
        }

    }

    private void GetAroundBullet(string key, Action<ReturnToPool_Particle> endCall = null)
    {
        var bulletAround = bulletAroundPool[key].Dequeue();
        bulletAround.Init();
        bulletAround.gameObject.SetActive(true);
        endCall?.Invoke(bulletAround);
    }



    #endregion

    #region Bullet

    public void CreateBullet(string key, Action<Bullet> endCall = null)
    {
        if (string.IsNullOrEmpty(key))
            return;

        DataManager.Instance.GetGameObject(key, (obj) =>
        {
            SetBullet(obj, key, endCall);
        });
    }

    void SetBullet(GameObject obj, string key, Action<Bullet> endCall = null)
    {
        var bullet = obj.GetComponent < Bullet>();
        bullet.Init();
        bullet.particleStoppedEventHandler += () => BindParticleStoppedEvent(bullet, key);

        obj.transform.SetParent(parent);

        BindParticleStoppedEvent(bullet, key);

        endCall?.Invoke(bullet);

    }


    private void BindParticleStoppedEvent(Bullet bullet, string key)
    {
        if (bulletPool.ContainsKey(key) == false)
        {
            bulletPool.Add(key, new Queue<Bullet>());
        }

        bullet.gameObject.SetActive(false);
        bulletPool[key].Enqueue(bullet);
    }

    public void GetBulletPool(string key, Action<Bullet> endCall = null)
    {
        if (string.IsNullOrEmpty(key))
            return;

        if (bulletPool[key].Count > 0)
        {
            var bulletAround = bulletPool[key].Dequeue();
            bulletAround.gameObject.SetActive(true);
            endCall?.Invoke(bulletAround);
        }
        else
        {
            CreateBullet(key, (particle) =>
            {
                GetAround(key, endCall);
            });
        }

    }

    private void GetAround(string key, Action<Bullet> endCall = null)
    {
        var bullet = bulletPool[key].Dequeue();
        bullet.Init();
        bullet.gameObject.SetActive(true);
        endCall?.Invoke(bullet);


    }
    #endregion




}
