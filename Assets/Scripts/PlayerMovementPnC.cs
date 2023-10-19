using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementPnC : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject AS;
    private Rigidbody2D rb;
    private AudioSource aus;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool stop;
    public bool showPath;
    public bool showAhead;
    

    void Start()
    {
        aus = AS.GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //agent.speed += 0.1f;
            agent.SetDestination(Input.mousePosition);
        }
        /*if ((transform.position - target.transform.position).magnitude < 1 && !stop)
        {
            stop = true;
            target.GetComponent<Navigate>().enabled = false;

            var win = FindObjectOfType<Continue>();
            win.WinCondition();
        }*/
    }

    private void OnDrawGizmos()
    {
       // Navigate.DrawGizmos(agent, showPath, showAhead);
    }
}
