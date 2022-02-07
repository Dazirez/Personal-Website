using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HasHealth : MonoBehaviour
{

    BaseMovement movement;
    public float current_hp = 1;
    public float max_health = 3;
    public bool is_player = false;
    public bool is_aquamentus = false;
    public bool is_wallmaster = false;
    public bool invincible = false;
    float low_health_timer;
    float timer;
    public float stun_duration = 0.5f;
    public float time_invincible = 2f;

    bool isStunned;

    void Start()
    {
        if (is_player)
        {
            movement = GetComponent<BaseMovement>();

        }
        isStunned = false;
    }
    void Update()
    {

        timer -= Time.deltaTime;
        low_health_timer += Time.deltaTime;
        if (is_player)
        {
            if (GameController.instance.inGodMode())
            {
                setInvincible(true);
                return;
            }
            if (current_hp <= 1.0f && low_health_timer > 0.33f)
            {
                low_health_timer = 0;
                AudioController.instance.play_low_health();
            }
        }
    }

    public bool isFullHealth()
    {
        return current_hp == max_health;
    }
    public void setInvincible(bool b)
    {
        //change so Player does not collide with stalofs
        if (is_player && b)
        {
            gameObject.layer = 7;
        }
        if (is_player && !b)
        {
            gameObject.layer = 6;
        }
        invincible = b;
    }

    public bool isInvincible()
    {
        return invincible;
    }

    public void AlterHP(float delta)
    {
        //Debug.Log("Current health");
        //Debug.Log(current_hp);
        //Debug.Log("damage dealt");
        //Debug.Log(delta);
        if (delta < 0 && isInvincible()) return;

        current_hp += delta;
        if (current_hp > max_health)
        {
            current_hp = max_health;
        }
        if (current_hp <= 0)
        {
            death();
        }
        else if (delta < 0 && is_player)
        {
            AudioController.instance.play_damage_taken();
            StartCoroutine(Damaged());
            StartCoroutine(Blink());
        }
        else if (delta < 0 && is_aquamentus)
        {
            StartCoroutine(Blink());
            AudioController.instance.play_aquamentus_damaged();
        }
        else if (delta < 0)
        {
            StartCoroutine(Blink());
            AudioController.instance.play_enemy_damage_clip();
        }

    }
    private void death()
    {
        if (is_player)
        {
            AudioController.instance.play_player_death();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // Drop loot and play death sound
            DropsCollectable loot = GetComponent<DropsCollectable>();
            if (loot != null)
            {
                loot.drop_collectable();
            }
            AudioController.instance.play_enemy_death();

            // Figure out which room we're in and call corresponding level controller
            // and reduce num_enemies by 1
            float remainderX = gameObject.transform.position.x % 16f;
            float remainderY = gameObject.transform.position.y % 11f;
            float startOfRoomX = gameObject.transform.position.x - remainderX;
            float startOfRoomY = gameObject.transform.position.y - remainderY;
            Vector2 roomCoordinate = new Vector2(startOfRoomX / 16f, startOfRoomY / 11f);

            // Calling level controller and reducing remainingEnemies by 1
            LevelController currentLevel = GameController.instance.coordinateToLevelController[roomCoordinate];
            currentLevel.reduceEnemies(1, roomCoordinate);


            Destroy(gameObject);
        }
    }

    private IEnumerator Damaged()
    {
        //turn on invincibility for 2 seconds and stun for 0.5 seconds; 
        setInvincible(true);
        movement.enabled = false;
        isStunned = true;
        yield return new WaitForSeconds(1f);
        movement.enabled = true;
        isStunned = false;
        yield return new WaitForSeconds(1.5f);
        setInvincible(false);

    }
    private IEnumerator Blink()
    {
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        timer = 2.0f;

        while (timer > 0.0f)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.05f);
            playerSprite.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }


    }

    public float GetHealth()
    {
        if (GameController.instance.inGodMode() && is_player) return max_health;
        return current_hp;
    }

    public bool GetIsStunned()
    {
        return isStunned;
    }
}
