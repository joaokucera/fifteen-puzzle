using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour
{
    #region Fields
    public float speed = 10.0f;
    private RaycastHit hit;
    private float distance = 10.0f;
    private bool isMoving = false;
    private Transform blockRef;
    private Vector3 origin;
    private Vector3 target;
    private bool isShuffling = false;
    public int moveTimes = 50;
    private float startTimeMovement;
    #endregion

    #region Update
    void Update()
    {

        if (isShuffling)
        {
            Shuffle();
        }
        else
        {
#if UNITY_EDITOR
            DebugInputs();
#endif
        }

        if (isMoving)
        {
            transform.position = Vector3.Lerp(origin, target, (Time.time - startTimeMovement) * (isShuffling ? 1000.0f : speed));
            blockRef.position = Vector3.Lerp(target, origin, (Time.time - startTimeMovement) * (isShuffling ? 1000.0f : speed));
            if (Vector3.Distance(transform.position, target) <= 0.0f)
                isMoving = false;
        }
    }
    #endregion

    #region Shuffle board
    void Shuffle()
    {
        if (!isShuffling)
        {
            isShuffling = true;
            StartCoroutine("Shuffling");
        }

    }

    IEnumerator Shuffling()
    {
        int countMovement = 0;
        while (countMovement < moveTimes)
        {
            if (isMoving)
                yield return null;

            bool move = false;
            switch (Random.Range(1, 5))
            {
                case 1:
                    if (MoveBlockToUp())
                        move = true;
                    break;
                case 2:
                    if (MoveBlockToRight())
                        move = true;
                    break;
                case 3:
                    if (MoveBlockToDown())
                        move = true;
                    break;
                case 4:
                    if (MoveBlockToLeft())
                        move = true;
                    break;
            }

            if (move)
            {
                countMovement++;
                yield return null;
            }
        }

        isShuffling = false;
    }
    #endregion

    #region Properties to check movement

    bool CheckMovement(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            origin = transform.position;
            blockRef = hit.transform;
            target = blockRef.position;
            return true;
        }

        return false;
    }

    bool HasAboveBlock
    {
        get
        {
            return CheckMovement(Vector3.up);
        }
    }

    bool HasBelowBlock
    {
        get
        {
            return CheckMovement(Vector3.down);
        }
    }

    bool HasLeftBlock
    {
        get
        {
            return CheckMovement(Vector3.left);
        }
    }

    bool HasRightBlock
    {
        get
        {
            return CheckMovement(Vector3.right);
        }
        
    }
    #endregion

    #region Movement
    bool MoveBlockToUp()
    {
        if (isMoving)
            return false;

        if (HasBelowBlock)
        {
            startTimeMovement = Time.time;
            isMoving = true;
        }

        return true;
    }

    bool MoveBlockToDown()
    {
        if (isMoving)
            return false;

        if (HasAboveBlock)
        {
            startTimeMovement = Time.time;
            isMoving = true;
        }

        return true;
    }

    bool MoveBlockToLeft()
    {
        if (isMoving)
            return false;

        if (HasRightBlock)
        {
            startTimeMovement = Time.time;
            isMoving = true;
        }

        return true;
    }

    bool MoveBlockToRight()
    {
        if (isMoving)
            return false;

        if (HasLeftBlock)
        {
            startTimeMovement = Time.time;
            isMoving = true;
        }

        return true;
    }
    #endregion

    #region Debug inputs
    void DebugInputs()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveBlockToLeft();

        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveBlockToRight();

        if (Input.GetKeyDown(KeyCode.UpArrow))
            MoveBlockToUp();

        if (Input.GetKeyDown(KeyCode.DownArrow))
            MoveBlockToDown();
    }
    #endregion
}

