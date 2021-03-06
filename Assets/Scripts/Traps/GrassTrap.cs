﻿using UnityEngine;

public class GrassTrap : Trap
{
    [SerializeField] private float debuffTime = 1;
    [SerializeField] private bool generateRoots = true;
    [SerializeField] private GameObject slowVFX = null;
    [SerializeField] private GameObject root = null;

    private const float slowRatio = 1; // Set to 1 for 100% slow 

    protected override void EnterEffect()
    {
        if (generateRoots)
        {
            Vector3 trapPos = transform.position;
            Vector3 rootPos = new Vector3(trapPos.x, trapPos.y, trapPos.z - 1);
            Root newRoot = Instantiate(root, rootPos, transform.rotation).GetComponent<Root>();
            newRoot.SetUp(player.GetComponent<Player>(), false, debuffTime, slowRatio);
        }
        else
        {
            SlowingEffect newDebuff = player.AddComponent<SlowingEffect>();
            newDebuff.SetInitial(slowVFX, debuffTime);
            newDebuff.SetStats(slowRatio, slowRatio);
        }
    }

    protected override void ExitEffect()
    {
        return;
    }
}