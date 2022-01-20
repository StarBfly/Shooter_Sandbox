using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mutant : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject player;
    private Transform _goal;
    private NavMeshAgent _agent;
    private Animator _animator;

    [Header("Params:")]
    [SerializeField] private int _health = 3;
    private bool _shot = false;
    private bool _dead = false;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _goal = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_dead)
        {
            _agent.destination = _goal.position;
        }
    }


    public void Damage()
    {
        if (!_shot)
        {
            Debug.Log("DAMAGE MUTANT");
            _health--;
            _shot = true;
            _animator.SetBool("shot", true);
            StartCoroutine(InvulnarbleWindow());
        }
        if (_health == 0)
        {
            OnDeath();
        }

    }

    IEnumerator InvulnarbleWindow()
    {
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("shot", false);
        _shot = false;
    }

    private void OnDeath()
    {
        _animator.SetBool("dead", true);
        _agent.isStopped = true;
        _dead = true;
    }


}
