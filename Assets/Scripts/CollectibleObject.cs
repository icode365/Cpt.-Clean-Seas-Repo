using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    public GameObject gameObjectModel;
    public Vector2 rotateAngleRange = new();
    public Vector2 rotateTimeRange = new();

    public Vector2 floatScaleRange = new(1.1f, 1.25f);
    public Vector2 floatTimeRange = new();

    public float sinkTime = 5f;

    private void Awake()
    {
        if(gameObjectModel==null)
        {
            print("~ERROR!~ Child Object for " + gameObject.name + " object is NOT ASSIGNED!");
            
            //gameObjectModel = transform.GetChild(0).gameObject;
        }
    }
    private void Start()
    {
        StartCoroutine(Floating());
    }

    IEnumerator Floating()
    {
        float randomRotateAngle = Random.Range(rotateAngleRange.x, rotateAngleRange.y);
        float randomRotateTime = Random.Range(rotateTimeRange.x, rotateTimeRange.y);

        float randomScale = Random.Range(floatScaleRange.x, floatScaleRange.y);
        float randomScaleTime = Random.Range(floatTimeRange.x, floatTimeRange.y);

        while(true)
        {
            LeanTween.cancel(gameObject);

            LeanTween.rotateAroundLocal(this.gameObject, transform.up, randomRotateAngle, randomRotateTime);
            LeanTween.rotateAroundLocal(this.gameObject, transform.right, randomRotateAngle, randomRotateTime);

            LeanTween.scale(gameObject, transform.localScale * randomScale, randomScaleTime);
            yield return new WaitForSeconds(randomScaleTime + 0.5f);
            LeanTween.scale(gameObject, transform.localScale / randomScale, randomScaleTime);
            yield return new WaitForSeconds(randomScaleTime + 0.5f);
        }
    }
}
