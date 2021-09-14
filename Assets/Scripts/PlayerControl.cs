using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed;
    public Vector3 target;
    public float velocity;
    public Vector3 previous;
    public Animator animator;
    public GameObject selectedGameObject;

    private void Awake()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        SetSelectedVisible(false);
    }
    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }
    void Start()
    {
        speed = 8f;
        target = transform.position;
    }

    void Update()
    {
        movePlayer();
        calculateVelocity();
        animator.SetFloat("Velocity", velocity);
    }

    void calculateVelocity()
    {
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
    }

    void setTargetPos()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
    }

    public void movePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
