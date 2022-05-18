﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DamageableActor : Actor
{
    protected SpriteRenderer spriteRenderer;
    protected Health health;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = gameObject.GetComponentInHierarchy<Health>();

        health.onHealthUpdated -= OnHealthUpdated;
        health.onHealthUpdated += OnHealthUpdated;

        health.onDie -= OnDie;
        health.onDie += OnDie;
    }

    private void OnDie()
    {
        animator.SetTrigger("die");
    }

    private void OnHealthUpdated(float curHealth, float deltaChange)
    {
        if (deltaChange < 0)
        {
            // 受到伤害
            animator.SetTrigger("hit");
        }
    }
}
