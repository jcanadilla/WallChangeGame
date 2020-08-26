using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    // Indica si el player está en la pared derecha
    internal bool right = false;

    // Indica si el player está en la pared izquierda
    internal bool left = false;

    // Indica que el player ha saltado hacia la derecha
    internal bool jumpToRight = false;

    // Indica que el player ha saltado hacia la izquierda
    internal bool jumpToLeft = false;

    internal bool initialized = false;

    private float wallDistance = 2f;

    public float MoveSpeed { get => moveSpeed; }

    // Start is called before the first frame update
    void Start()
    {
        int randomNumber = Random.Range(0, 1);
        if (randomNumber == 0) jumpToLeft = true;
        if (randomNumber == 1) jumpToRight = true;
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
            transform.Translate(Vector3.right * Time.deltaTime * jumpSpeed);
        }
        else if (!right && !jumpToLeft && transform.position.x >= wallDistance)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -wallDistance, wallDistance), transform.position.y, transform.position.z);
            jumpToRight = false;
            right = true;
        }

        if (!right && jumpToLeft && transform.position.x > -wallDistance)
        {
            transform.Translate(Vector3.left * Time.deltaTime * jumpSpeed);
        }
        else if (!left && !jumpToRight && transform.position.x <= -wallDistance)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -wallDistance, wallDistance), transform.position.y, transform.position.z);
            jumpToLeft = false;
            left = true;
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
