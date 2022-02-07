using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMasterController : MonoBehaviour
{
    public float spawn_timer = 2;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        timer -= Time.deltaTime;
    }

    public void spawn_wallmaster(Vector3 pos, int dir)
    {
        if (GetComponent<LevelController>().remainingEnemies <= 0)
        {
            return;
        }
        if (timer < 0)
        {
            timer = spawn_timer;
            StartCoroutine(spawn_and_destroy(pos, dir));
        }

    }
    IEnumerator spawn_and_destroy(Vector3 pos, int dir)
    {
        GameObject enemy = Spawner.instance.Spawn(6, pos);
        Debug.Log(enemy);
        enemy.GetComponent<WallmasterMovement>().set_direction(dir);
        yield return new WaitForSeconds(4.1f);
        Destroy(enemy);
    }
}
