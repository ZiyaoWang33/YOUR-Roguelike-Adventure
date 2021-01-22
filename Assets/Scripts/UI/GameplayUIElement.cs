<<<<<<< HEAD
﻿using UnityEngine;

public abstract class GameplayUIElement : MonoBehaviour
{
    [SerializeField] protected GameObject element = null;

    protected Player player = null;

    protected virtual void Awake()
    {
        Player.OnPlayerEnter += OnPlayerEnterEventHandler;
    }

    protected virtual void OnPlayerEnterEventHandler(Player player)
    {
        element.SetActive(true);

        this.player = player;
        player.OnPlayerExit += OnPlayerExitEventHandler;
    }

    protected virtual void OnPlayerExitEventHandler()
    {
        element.SetActive(false);
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIElement : MonoBehaviour
{
    protected virtual void OnPlayerEnterEventHandler(Player player)
    {
       
>>>>>>> 087a702e945d5ddf0d47656acf8dfbefec300f77
    }

    protected virtual void OnDestroy()
    {
<<<<<<< HEAD
        Player.OnPlayerEnter -= OnPlayerEnterEventHandler;
        player.OnPlayerExit -= OnPlayerExitEventHandler;
=======

>>>>>>> 087a702e945d5ddf0d47656acf8dfbefec300f77
    }
}
