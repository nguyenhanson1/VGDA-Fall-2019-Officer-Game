using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteract : MonoBehaviour, IDamagable
{
    public Factions.Faction myFaction => Factions.Faction.Good;
    public Health health => totalHealth;

    private Health totalHealth = new Health();

    [Header("Health")]
    public int displayHealth = 0;
    public int maxHealth = 5;

    [Header("Attacking")]
    [SerializeField] private PauseMenuManager pause;
    [Tooltip("Script where object will get their bullets from.")]
    [SerializeField] private ObjectPooler bulletPool = null;
    [Tooltip("Main Camera of the Game")]
    [SerializeField] private Camera persCam = null;
    [Tooltip("How fast the Player shoots (Higher for faster).")]
    [SerializeField] private float dps = 1f;

    //Seconds between each shot
    private float shotDelay = 0;
    //Player can only shoo when it's true
    private bool shotDelayed = false;
    private Attack attack = new Attack();

    private void OnEnable()
    {
        shotDelay = 1f / dps;
        GameManager.StartOccurred += Initialize;
        GameManager.UpdateOccurred += checkforShot;
        Health.OnDeath += Begoned;
        GameManager.UpdateOccurred += DisplayHealth;
    }
    private void OnDisable()
    {
        GameManager.StartOccurred -= Initialize;
        GameManager.UpdateOccurred -= checkforShot;
        Health.OnDeath -= Begoned;
        GameManager.UpdateOccurred -= DisplayHealth;
    }


    private void Initialize()
    {
        totalHealth.HealthTotal = maxHealth;
    }

    private void DisplayHealth()
    {
        displayHealth = totalHealth.HealthTotal;
    }

    //Destroys Player when its health goes to 0
    private void Begoned(Health h)
    {
        if (totalHealth.HealthTotal <= 0)
        { // totalHealth == h
            // Play Death Animation
            SceneManager.LoadScene("EndMenu");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    //Check if Fire button was pressed.
    private void checkforShot()
    {
        if (Input.GetButton("Fire1") && !pause.gamePaused)
            if (!shotDelayed)
            {
                if(FindObjectOfType<AudioManager>() != null)
                    FindObjectOfType<AudioManager>().PlaySound("PlayerShoot");

                attack.Shoot(gameObject, bulletPool);


                /*
                //Shoot Raycast to find if target in line of sight
                GameObject target = aimBot.getHitPosition();
                
                if (target != null)
                {
                    //Get direction to get rotation to look at enemy target (if any)
                    Vector3 direction = target.transform.position - transform.position;
                    attack.Shoot(gameObject, bulletPool, Quaternion.LookRotation(direction, transform.up));
                    Debug.Log("Gottem!");
                }
                else
                {
                    attack.Shoot(gameObject, bulletPool);
                }
                */
                StartCoroutine(DelayShots());
            }
    }
    private IEnumerator DelayShots()
    {
        shotDelayed = true;
        yield return new WaitForSeconds(shotDelay);
        shotDelayed = false;
    }
}
