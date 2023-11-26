using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;

    [Header("Player Hand")]
    public Transform lookingDirection;

    [Header("Speed Settings")]
    public float walkingSpeed = 3.5f;
    public float speed = 3.5f;
    public float sprintSpeed = 4f;
    public bool isSprinting = false;
    public bool canMove = true;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float stamina = 100f;
    public float staminaDrain = 5f;
    public float staminaRegen = 10f;
    public GameObject staminaBar;

    [Header("Tilemap")]
    public Tilemap world;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = walkingSpeed;
        staminaBar = GameObject.Find("StaminaBar");
        staminaBar.GetComponent<Slider>().maxValue = maxStamina;
        staminaBar.GetComponent<Slider>().value = stamina;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tilePos = world.WorldToCell(lookingDirection.position);
        tilePos.z = 0;
        tilePos.x += 0.5f;
        tilePos.y += 0.5f;

        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        //movimento del player
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        isSprinting = Input.GetKeyDown(KeyCode.LeftShift);

        if (isSprinting)
        {
            if (stamina > staminaDrain)
            {
                Sprint();
            }
            else
            {
                speed = walkingSpeed;
            }
        }
        else if (stamina < maxStamina)
        {
            speed = walkingSpeed;
            RegenStamina();
            staminaBar.GetComponent<Slider>().value = stamina;
        }
        Movement(horizontal, vertical, speed);
    }

    private void Sprint()
    {
        speed = sprintSpeed;
        stamina -= staminaDrain * Time.deltaTime * 100;
        staminaBar.GetComponent<Slider>().value = stamina;
    }
    private void RegenStamina()
    {
        stamina += staminaRegen * Time.deltaTime * 100;
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }
    /// <summary>
    /// Muove il rigidBody con il rispettivo oggetto
    /// </summary>
    /// <param name="horizontal">Valore per muovere orizzontalmente</param>
    /// <param name="vertical">Valore per muovere verticalmente</param>
    /// <param name="speed">Fattore di velocita</param>
    private void Movement(float horizontal, float vertical, float speed = 1)
    {
        if(horizontal == 0 && vertical == 0)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        speed /= Mathf.Sqrt(Mathf.Pow(vertical, 2) + Mathf.Pow(horizontal, 2)); //Sempre != 0
        horizontal *= speed;
        vertical *= speed;
        rb.velocity = new Vector2(horizontal, vertical);
        lookingDirection.localPosition = new Vector2(
            1.25f * GetSignFromValue(horizontal),
            0.6f * GetSignFromValue(vertical)
        );
    }
    /// <summary>
    /// Ritorna il segno del valore
    /// </summary>
    /// <param name="val"></param>
    /// <returns>0 se e 0, 1 se e positivo e -1 se e negativo</returns>
    private int GetSignFromValue(float val)
    {
        if (val == 0) return 0;
        if (val > 0) return 1;
        return -1;
    }
}
