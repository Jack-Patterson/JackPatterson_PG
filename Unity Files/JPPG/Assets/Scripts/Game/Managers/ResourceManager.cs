using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    public enum Resource {Stone, Gold, Wood, Food, MeleeSkill}

    internal Item stone;
    internal Item gold;
    internal Item wood;
    internal Item food;
    internal Item meleeSkill;

    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject rockPrefab;
    private GameObject heightFinder;

    private Vector2 minPos = new Vector2(-380f, -210f);
    private Vector2 maxPos= new Vector2(140f, 75f);

    private Vector2 minTownBounds = new Vector2(-150f, -85f);
    private Vector2 maxTownBounds = new Vector2(20f, 15f);

    private List<GameObject> harvestableObjects = new List<GameObject>();
    private List<GameObject> storageItems = new List<GameObject>();

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of ResourcesManager");
            return;
        }
        instance = this;
    }

    void Start()
    {
        stone = new Item("Stone", 0, 40, Resource.Stone);
        gold = new Item("Gold", 0, 1, Resource.Gold);
        wood = new Item("Wood", 0, 20, Resource.Wood);
        food = new Item("Food", 0, 20, Resource.Food);
        meleeSkill = new Item("Melee Skill", 0, 20, Resource.MeleeSkill);

        heightFinder = GameObject.Find("objectHeightFinder");

        for (int i = 0; i < 30; i++)
        {
            GameObject go = spawnResourceItem(treePrefab);
            harvestableObjects.Add(go);
        }

        for (int i = 0; i < 10; i++)
        {
            GameObject go = spawnResourceItem(rockPrefab);
            harvestableObjects.Add(go);
        }
    }

    void Update()
    {
       
    }

    public Item getResourceItem(Resource resource)
    {
        switch (resource)
        {
            case Resource.Stone: return stone;
            case Resource.Wood: return wood;
            case Resource.Gold: return gold;
            case Resource.Food: return food;
            case Resource.MeleeSkill: return meleeSkill;
            default: return null;
        }
    }

    public float getResourceTime(Resource resource)
    {
        switch (resource)
        {
            case Resource.Food: return 2f;
            case Resource.Stone: return 5f;
            case Resource.MeleeSkill: return 2f;
            case Resource.Wood: return 3f;
            case Resource.Gold: return 1f;
            default: return 3f;
        }
    }

    public float getResourceObjectTime(Resource resource)
    {
        switch (resource)
        {
            case Resource.Food: return 240f; // 4 mins
            case Resource.Stone: return 10f; // 10 Mins 600f
            case Resource.MeleeSkill: return 120f; // 2 Mins
            case Resource.Wood: return 360f; // 6 mins
            default: return 300f; // 5 Mins
        }
    }

    internal GameObject spawnResourceItem(GameObject prefab)
    {
        Vector3 pos = spawnPos();

        GameObject tree = Instantiate(prefab, pos, Quaternion.identity);

        return tree;
    }

    private Vector3 spawnPos()
    {
        float x = Random.Range(minPos.x, maxPos.x);
        float z = Random.Range(minPos.y, maxPos.y);

        Ray ray = new Ray(new Vector3(x, 1000, z), Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Vector3 pos = new Vector3(x, hit.point.y, z);

        IInteractable harvestableObject;
        bool found = false;
        Collider[] cols = Physics.OverlapSphere(pos, 3);

        for (int i = 0; i < cols.Length; i++)
        {
            Collider c = cols[i];
            harvestableObject = c.GetComponent<HarvestableObject>();

            if (harvestableObject != null)
            {
                found = true;
                break;
            }
        }
        if (found)
        {
            return spawnPos();
        }
        else if (x < maxTownBounds.x && x > minTownBounds.x && z < maxTownBounds.y && z > minTownBounds.y)
        {
            return spawnPos();
        }

        return pos;
    }

    public List<GameObject> getHarvestableObjectsList()
    {
        return harvestableObjects;
    }

    internal void addToStorageItems(GameObject storageItem)
    {
        storageItems.Add(storageItem);
    }

    internal List<GameObject> getStorageItemsList()
    {
        return storageItems;
    }
}
