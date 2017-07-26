using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {


    public static Spawner spawner;
    public bool spawn = true;

    public GameObject[] prefabs;

    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();
    private Dictionary<string, List<GameObject>> pools = new Dictionary<string, List<GameObject>>();

    void Awake()
    {
        if (spawner == null) spawner = this;
    }

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        prefabDict = new Dictionary<string, GameObject>();
        pools = new Dictionary<string, List<GameObject>>();

        for (int i = 0; i < prefabs.Length; i++)
        {
            GameObject prefab = prefabs[i];
            prefabDict[prefab.name] = prefab;
            pools[prefab.name] = new List<GameObject>();
        }
    }

    public static GameObject Spawn(string name, bool spawnActive = false)
    {
        GameObject spawn = null;

        List<GameObject> pool = spawner.pools[name];
        spawn = pool.Find((g) => !g.activeSelf);

        if (spawn == null)
        {
            spawn = Instantiate(spawner.prefabDict[name]);
            pool.Add(spawn);
        }

        spawn.SetActive(spawnActive);
        return spawn;
    }
}
