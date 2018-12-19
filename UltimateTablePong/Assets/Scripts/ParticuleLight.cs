using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticuleLight : MonoBehaviour {

    void Start() {
        StartCoroutine(DestroyLight());
    }

    void Update()
    {
        gameObject.GetComponent<Light>().intensity -= 10.0f;
    }

    IEnumerator DestroyLight()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}
