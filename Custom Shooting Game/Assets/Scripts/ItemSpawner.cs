using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] Item_Exp expPrefab;

    private EnemyHealth enemy;

    private void Awake()
    {
        enemy = GetComponent<EnemyHealth>();
    }


    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        CreateExp();

        GameObject item = items[Random.Range(0, items.Length)];
        int percent = Mathf.RoundToInt(enemy.intensity * 100f);
        if(Random.Range(0,100) <= percent)
        {
            Vector2 itemPos = new Vector2(transform.position.x + 1, transform.position.y);
            Instantiate(item, itemPos, Quaternion.identity);
        }
    }
    private void CreateExp()
    {
        float intensity = enemy.intensity;
        Item_Exp exp = Instantiate<Item_Exp>(expPrefab, transform.position, transform.rotation);

        int point = Mathf.RoundToInt(100 * intensity);

        exp.SetUp(point);
    }
}
