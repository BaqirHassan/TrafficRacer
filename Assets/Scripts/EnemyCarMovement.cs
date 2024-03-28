using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarMovement : MonoBehaviour
{
    [SerializeField] float MaxMovementSpeed;


    bool IsMoving = true;
    // Start is called before the first frame update
    void Start()
    {
        MaxMovementSpeed *= Random.Range(.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsMoving)
            transform.position += transform.forward * MaxMovementSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            print("Player Collided");
            IsMoving = false;
        }
    }
}
