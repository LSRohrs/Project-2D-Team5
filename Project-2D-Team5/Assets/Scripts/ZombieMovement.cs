using UnityEngine;

public class ZombieMovement : MonoBehaviour
{

    public Transform leftEdge;
    public Transform rightEdge;

    public Transform enemy;

    public float speed;
    public float idle;
    private float idletimer;

    private Vector3 initScale;
    private bool moveLeft;

    public Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("Moving", false);
    }

    private void Update()
    {
        if (moveLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
                MoveinDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveinDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("Moving", false);
        moveLeft = !moveLeft;

        idletimer += Time.deltaTime;

        if (idletimer > idle)
        {
            moveLeft = !moveLeft;
        }
    }

    private void MoveinDirection(int direction)
    {
        idletimer = 0;

        anim.SetBool("Moving", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
