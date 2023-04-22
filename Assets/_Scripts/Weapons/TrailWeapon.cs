using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailWeapon : MonoBehaviour
{

    [SerializeField] private float trailBetweenDelay = 1;
    [SerializeField] private float trailAutoDestructTime = 3;
    [SerializeField] private ParticleSystem defaultTrail;
    [SerializeField] private GameObject trailObject;

    #region Singleton
    
    static public TrailWeapon Instance = null;
    void Awake() {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }
    
    #endregion

    public void ActiateWeapon() {
        defaultTrail.gameObject.SetActive(false);
        StartCoroutine(TrailRoutine());
    }

    private IEnumerator TrailRoutine() {
        while (true)
        {
            yield return new WaitForSeconds(trailBetweenDelay);
            GameObject newTrailObject = Instantiate(trailObject, transform.position, Quaternion.identity);
            Destroy(newTrailObject, trailAutoDestructTime);
        }
    }

    public void LongerTrail(float seconds) {
        trailAutoDestructTime += seconds;
    }

}
