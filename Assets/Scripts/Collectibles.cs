using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public SpawnableTypes type;
    public bool isLog = false;
    public bool isCollectible;
    public float moveSpeed = 1f;
    public float sinkTime = 5f;
    public float sinkSpeed = 1f;

/*    [Header("Floating Attributes")]
    public GameObject gameObjectModel;
    public Vector2 rotateAngleRange = new();
    public Vector2 rotateTimeRange = new();

    public Vector2 floatScaleRange = new(1.1f, 1.25f);
    public Vector2 floatTimeRange = new();*/

    private void Start()
    {
        //Invoke(nameof(SinkSelf), sinkTime);
        //StartCoroutine(Floating());
    }

    float xPos = 5.5f;
    
    private void OnEnable()
    {
        if(isLog)
        {
            xPos = -xPos;
            transform.position = new Vector3(xPos, 0f, 0f);
        }
    }

    private void Update()=>
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.down);   
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DisableSelf"))
            gameObject.SetActive(false);

        if (isCollectible)
            if (other.CompareTag(ReferenceLibrary.Instance.referenceItemList[AllTags.CollectionBag]))
                GetCollected();
    }

    void GetCollected()
    {
        LeanTween.cancelAll(gameObject);
        gameObject.SetActive(false);
    }

/*    IEnumerator Floating()
    {
        float randomRotateAngle = Random.Range(rotateAngleRange.x, rotateAngleRange.y);
        float randomRotateTime = Random.Range(rotateTimeRange.x, rotateTimeRange.y);

        float randomScale = Random.Range(floatScaleRange.x, floatScaleRange.y);
        float randomScaleTime = Random.Range(floatTimeRange.x, floatTimeRange.y);

        while (true)
        {
            LeanTween.cancel(gameObjectModel);

            LeanTween.rotateAroundLocal(gameObjectModel, transform.up, randomRotateAngle, randomRotateTime);
            LeanTween.rotateAroundLocal(gameObjectModel, transform.right, randomRotateAngle, randomRotateTime);

            LeanTween.scale(gameObjectModel, transform.localScale * randomScale, randomScaleTime);
            yield return new WaitForSeconds(randomScaleTime + 0.5f);
            LeanTween.scale(gameObjectModel, transform.localScale / randomScale, randomScaleTime);
            yield return new WaitForSeconds(randomScaleTime + 0.5f);
        }
    }*/
}
