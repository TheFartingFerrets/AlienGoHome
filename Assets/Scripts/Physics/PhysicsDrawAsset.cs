using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class PhysicsDrawAsset : MonoBehaviour 
{
    public GameObject AssetPrefab;

    public int PrefabAmount = 0;
    public Text AssetAmount;

    public void Awake()
    {
        PrefabAmount = GameObject.Find("PhysicsControl").GetComponent<PhysicsControl>().AllowedAssets[transform.GetSiblingIndex()];
        AssetAmount = transform.GetChild(0).GetComponent<Text>();
        AssetAmount.text = PrefabAmount.ToString();
    }
    public void CreatePhysicsAsset()
    {
        if( PrefabAmount > 0)
        {
            GameObject G = (GameObject)Instantiate(AssetPrefab, new Vector3(0, -3f,0f), Quaternion.identity);
            G.GetComponent<PhysicsAsset>().SetAssetDrawParent(this.gameObject);
            PrefabAmount--;
            PhysicsControl.control.AssetsInScene++;
        }
    }

    public void Update()
    {
        AssetAmount.text = PrefabAmount.ToString();
    }

    public void AddPhysicsAsset()
    {
        PrefabAmount++;
    }
}
