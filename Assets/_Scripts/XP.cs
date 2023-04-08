using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP : MonoBehaviour
{

    [SerializeField] private int xpAmount = 1;

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.CompareTag("Player"))
        {
            Destroy(gameObject);
            XPManager.Instance.GainXP(xpAmount);
        }
    }

}
