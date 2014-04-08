using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour
{
    #region Events
    public delegate void MoveAction();
    public static event MoveAction OnMoveBlock;
    public static event MoveAction OnStartShuffle;
    public static event MoveAction OnEndShuffle;
    #endregion

    #region Fields
    public float speed = 10.0f;
    public float shufflingSpeed = 250.0f;
    private RaycastHit hit;
    private float distance = 10.0f;
    private bool isMoving = false;
    private Transform blockRef;
    private Vector3 origin;
    private Vector3 target;
    private bool isShuffling = false;
    public int moveTimes = 200;
    private float startTimeMovement;
    private bool isGameOver = false;
    private float threshold = 70.0f;
    private Vector2 finalFingerPosition;  
    private Vector2 lastFingerPosition; 
    #endregion

    void OnEnable()
    {
        GameOver.OnGameOver += OnGameOver;
    }

    void OnDisable()
    {
        GameOver.OnGameOver -= OnGameOver;
    }

    void OnGameOver()
    {
        isGameOver = true;
    }

    #region Update
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(origin, target, (Time.time - startTimeMovement) * (isShuffling ? shufflingSpeed : speed));
            blockRef.position = Vector3.Lerp(target, origin, (Time.time - startTimeMovement) * (isShuffling ? shufflingSpeed : speed));
            if (Vector3.Distance(transform.position, target) <= 0.0f)
            {
                isMoving = false;
                transform.position = target;
                blockRef.position = origin; 
            }
        }
        else
        {
            if (!isGameOver)
            {
                if (isShuffling)
                {
                    Shuffle();
                }
                else
                {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
                    KeyboardInputs();
#elif UNITY_ANDROID
                    TouchInputs();
#endif
                }
            }
        }
    }
    #endregion

    #region Shuffle board
    void Shuffle()
    {
        if (!isShuffling)
        {
            if (OnStartShuffle != null)
                OnStartShuffle();
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

        if (OnEndShuffle != null)
            OnEndShuffle();
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

            if (!isShuffling)
            {
                if (OnMoveBlock != null)
                    OnMoveBlock();
            }
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

            if (!isShuffling)
            {
                if (OnMoveBlock != null)
                    OnMoveBlock();
            }
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

            if (!isShuffling)
            {
                if (OnMoveBlock != null)
                    OnMoveBlock();
            }
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

            if (!isShuffling)
            {
                if (OnMoveBlock != null)
                    OnMoveBlock();
            }
        }

        return true;
    }
    #endregion

    #region Keyboard inputs
    void KeyboardInputs()
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

    #region Touch inputs
    void TouchInputs()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                finalFingerPosition = touch.position;
                lastFingerPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                lastFingerPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if ((finalFingerPosition.x - lastFingerPosition.x) > threshold) // left swipe
                    MoveBlockToLeft();
                else if ((finalFingerPosition.x - lastFingerPosition.x) < -threshold) // right swipe
                    MoveBlockToRight();
                else if ((finalFingerPosition.y - lastFingerPosition.y) < -threshold) // up swipe
                    MoveBlockToUp();
                else if ((finalFingerPosition.y - lastFingerPosition.y) > threshold) // down swipe
                    MoveBlockToDown();
            }
        }

        // Back button
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit(); 
    }
    #endregion
}

