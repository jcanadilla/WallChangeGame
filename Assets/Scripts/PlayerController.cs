using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Vector3 leftSmokePosition;
    [SerializeField] private Vector3 rightSmokePosition;

    // Indica si el player está en la pared derecha
    internal bool right = false;

    // Indica si el player está en la pared izquierda
    internal bool left = false;

    // Indica que el player ha saltado hacia la derecha
    internal bool jumpToRight = false;

    // Indica que el player ha saltado hacia la izquierda
    internal bool jumpToLeft = false;

    private float wallDistance = 2f;

    private GameObject smoke;

    public float MoveSpeed { get => moveSpeed; }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        int randomNumber = Random.Range(0, 1);
        right = false;
        left = false;
        if (randomNumber == 0)
        {
            jumpToLeft = true;
        } else
        {
            jumpToLeft = false;
        }

        if (randomNumber == 1)
        {
            jumpToRight = true;
        } else
        {
            jumpToRight = false;
        }
        smoke = this.transform.Find("smoke").gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Elegir lado inicial pasado 1 segundo
/*        if (!initialized && Time.fixedTime > 0.5f)
        { */
            
         /*   initialized = true;
        }
*/
        // Mueve el player hacia arriba
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

        if (!left && jumpToRight && transform.position.x < wallDistance)
        {
            // Está en un salto
            transform.Translate(Vector3.right * Time.deltaTime * jumpSpeed);
        }
        else if (!right && !jumpToLeft && transform.position.x >= wallDistance)
        {
            // Está en la pared derecha
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -wallDistance, wallDistance), transform.position.y, transform.position.z);
            jumpToRight = false;
            right = true;

            // Activamos el humo en la posición derecha
            smoke.transform.localPosition = rightSmokePosition;
            smoke.GetComponent<ParticleSystem>().Play();
        }

        if (!right && jumpToLeft && transform.position.x > -wallDistance)
        {
            // Está en un salto
            transform.Translate(Vector3.left * Time.deltaTime * jumpSpeed);
        }
        else if (!left && !jumpToRight && transform.position.x <= -wallDistance)
        {
            // Está en la pared izquierda
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -wallDistance, wallDistance), transform.position.y, transform.position.z);
            jumpToLeft = false;
            left = true;

            // Activamos el humo en la posición izquierda
            smoke.transform.localPosition = leftSmokePosition;
            smoke.GetComponent<ParticleSystem>().Play();
        }
        
    }

    public void Jump()
    {
        // Cambiar los valores de los booleans
        if (this.left)
        {
            jumpToRight = true;
            left = false;
        }

        if (this.right)
        {
            jumpToLeft = true;
            right = false;
        }

        // Desactivamos el humo
        smoke.GetComponent<ParticleSystem>().Stop();
    }

    public void inRightWall()
    {
        jumpToRight = false;
        right = true;
    }

    public void inLeftWall()
    {
        jumpToLeft = false;
        left = true;
    }
}
