using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : Singleton<ProjectileController>
{

    Dictionary<int, Queue<Projectile>> projectiles = new Dictionary<int, Queue<Projectile>>();
    Transform projectileParent;
    Dictionary<int, Queue<Projectile>> Usedprojectiles = new Dictionary<int, Queue<Projectile>>();
    Transform usedprojectileParent;
    public Player Target;

    public Projectile FirstPooling(Projectile pro)
    {
        if (!Usedprojectiles.ContainsKey(pro.SerialNum))
        {
            Usedprojectiles.Add(pro.SerialNum, new Queue<Projectile>());
        }
        if (!projectiles.ContainsKey(pro.SerialNum))
        {
            projectiles.Add(pro.SerialNum, new Queue<Projectile>());
        }
        Projectile obj = GameObject.Instantiate(pro.gameObject, pro.transform.position + Vector3.up * 0.5f, Quaternion.Euler(90, 0, 0)).transform.GetComponent<Projectile>();
        obj.name = pro.name;
        obj.transform.SetParent(projectileParent);
        obj.DirectionControl(Target.transform);
        projectiles[pro.SerialNum].Enqueue(obj);
        return obj;
    }

    public Projectile ProjectilePooling(Transform Pos, int _Projectile_SerialNum)
    {
        if (Usedprojectiles[_Projectile_SerialNum].Count > 0)
        {
            Projectile temp = Usedprojectiles[_Projectile_SerialNum].Dequeue();
            temp.transform.SetParent(projectileParent);
            temp.ReUse(Pos);
            temp.DirectionControl(Target.transform);
            projectiles[_Projectile_SerialNum].Enqueue(temp);
            return temp;
        }
        else
        {
            Projectile obj = GameObject.Instantiate(ProjectileInputer.ProjectileList[_Projectile_SerialNum], Pos.position + Vector3.up * 0.5f, Quaternion.Euler(90, 0, 0)).transform.GetComponent<Projectile>();
            obj.name = ProjectileInputer.ProjectileList[_Projectile_SerialNum].name;
            obj.transform.SetParent(projectileParent);
            obj.DirectionControl(Target.transform);
            projectiles[_Projectile_SerialNum].Enqueue(obj);
            return obj;
        }
    }

    public void UsedProjectilePooling(Projectile projectile)
    {
        projectile = projectiles[projectile.SerialNum].Dequeue();
        Usedprojectiles[projectile.SerialNum].Enqueue(projectile);
        projectile.transform.SetParent(usedprojectileParent);
        projectile.gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Awake()
    {
        projectileParent = this.transform.GetChild(0).transform;
        usedprojectileParent = this.transform.GetChild(1).transform;
    }

    public void Barrior(List<Projectile> projectiles)
    {
        foreach (Projectile p in projectiles)
        {
            UsedProjectilePooling(p);
        }
    }
}
