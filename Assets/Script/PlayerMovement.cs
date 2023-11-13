using System.Collections;
using System.Collections.Generic;
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
        //movimento del player
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        if (isSprinting)
        {
            if (stamina > staminaDrain)
            {
                sprint();
                staminaBar.GetComponent<Slider>().value = stamina;
            }
            else
            {
                speed = walkingSpeed;
            }
            
        }
        else
        {
            if (stamina < maxStamina)
            {
                speed = walkingSpeed;
                regenStamina();
                staminaBar.GetComponent<Slider>().value = stamina;
            }
            
        }

        movement(horizontal, vertical,speed);

        
    }

    private void sprint()
    {
        speed = sprintSpeed;
        stamina -= staminaDrain * Time.deltaTime * 100;
    }

    private void regenStamina()
    {
        stamina += staminaRegen * Time.deltaTime * 100;
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
    }
    private void movement(float horizontal,float vertical,float speed)
    {
        if (horizontal < 0 || horizontal > 0)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            if (horizontal < 0)
            {
                lookingDirection.localPosition = new Vector2(-1.25f, -0.1f);
            }
            else
            {
                lookingDirection.localPosition = new Vector2(1.25f, -0.1f);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (vertical < 0 || vertical > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
            if (vertical < 0)
            {
                lookingDirection.localPosition = new Vector2(0f, -0.6f);
            }
            else
            {
                lookingDirection.localPosition = new Vector2(0f, 0.6f);
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }


    
}
