using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceLibrary : MonoBehaviour
{
    #region ----Singleton----
    private static ReferenceLibrary instance;
    public static ReferenceLibrary Instance
    {
        get
        {
            if (instance != null)
                return instance;
            print("ERROR! ~Instance of ReferenceLibrary is NULL~");
            return null;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public Dictionary<AllTags, string> referenceItemList = new()
    {
        {AllTags.Collectible , "Collectible" },
        {AllTags.CollectionBag, "CollectionBag"},
        {AllTags.Obstacle, "Obstacle"},
        {AllTags.DisableSelf, "DisableSelf"}
    };
}
