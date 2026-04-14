using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KoziPardy.Core
{
    public class BoardClueMovement : MonoBehaviour
    {
        [Header("Positions")]
        [SerializeField] Vector3 frontPosition;
        [SerializeField] Vector3 originalPosition;
        [SerializeField] Vector3 upwardFinalPosition;

        [Header("Rotations")]
        [SerializeField] Quaternion frontRotation;
        [SerializeField] Quaternion originalRotation;

        [Header("Speeds")]
        [SerializeField] float positionMoveSpeed = 1;
        [SerializeField] float upwardMoveSpeed = 1;

        [Header("Wait Times")]
        [SerializeField] float waitBeforeRotateIn = 0.05f;
        [SerializeField] float waitBeforeRotateOut = 0;

        [Header("ButtonCanvasControl")]
        [SerializeField] ButtonCanvasControl buttonCanvasControl;

        private Vector3 destinationPosition;
        private bool canMoveClue = false;
        private bool isUpFront = false;
        private bool isFinalClue = false;
        private bool finalMovement = false;
        private float step = 0;

        void Start()
        {
            originalPosition = transform.localPosition;
            destinationPosition = transform.position;

            originalRotation = transform.rotation;
        }

        void Update()
        {
            if (finalMovement) step = Time.deltaTime * upwardMoveSpeed;
            else step = Time.deltaTime * positionMoveSpeed;

            if (canMoveClue)
            {
                if (transform.localPosition != destinationPosition) MoveToDestination(step);
                else ArrivedAtDestination();
            }
        }

        private void MoveToDestination(float step)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destinationPosition, step);
        }

        private IEnumerator RotateToDestination(Quaternion target, float wait)
        {
            yield return new WaitForSeconds(wait);
            Quaternion from = transform.rotation;

            float startDistance = Vector3.Distance(transform.localPosition, destinationPosition);

            while (transform.localPosition != destinationPosition)
            {
                float currentDistance = Vector3.Distance(transform.localPosition, destinationPosition);
                float distanceDifference = (startDistance - currentDistance) / startDistance;

                transform.rotation = Quaternion.Slerp(from, target, distanceDifference);

                yield return null;
            }
            transform.rotation = target;
        }

        private void ArrivedAtDestination()
        {
            canMoveClue = false;

            if (transform.localPosition == frontPosition)
            {
                isUpFront = true;
                GetComponent<BoardClueMediaManager>().HideFrontText();

                buttonCanvasControl.ClueIsUpFront(GetComponent<BoardClueStateControl>().GetNumber(), GetComponent<BoardClueStateControl>().GetHasBeenClicked());

                GetComponent<BoardClueStateControl>().SetHasBeenClicked(); //this must come after the line above, as otherwise it'll get marked as an old question even if it's not

                if (isFinalClue)
                {
                    finalMovement = true;
                    StartCoroutine(SwitchBackToSplashCategories());
                }
            }

            if (transform.localPosition == originalPosition)
            {
                isUpFront = false;
                GetComponent<BoardClueMediaManager>().ClueCleanup();

                buttonCanvasControl.ClueIsBackHome();
            }

            if (transform.localPosition == upwardFinalPosition)
            {
                Destroy(gameObject);
            }
        }

        public void MoveInClue()
        {
            if (!isUpFront)
            {
                destinationPosition = frontPosition;
                canMoveClue = true;

                StartCoroutine(RotateToDestination(frontRotation, waitBeforeRotateIn));

                GetComponent<BoardClueMediaManager>().CluePrep();
                GetComponent<BoardClueStateControl>().GradualBrightenIfOld();
            }
        }

        public void MoveOutClue()
        {
            if (isUpFront)
            {
                destinationPosition = originalPosition;
                canMoveClue = true;

                StartCoroutine(RotateToDestination(originalRotation, waitBeforeRotateOut));

                GetComponent<BoardClueStateControl>().GradualDarkenIfOld();
            }
        }

        public void MoveOutClueFinal()
        {
            if (isUpFront)
            {
                destinationPosition = upwardFinalPosition;
                canMoveClue = true;
            }
        }

        public void SetIsFinalClue(bool state)
        {
            isFinalClue = state;
        }

        private IEnumerator SwitchBackToSplashCategories()
        {
            transform.parent = null;
            yield return null;
            DontDestroyOnLoad(gameObject);

            if (GameSettings.globalGameState == GlobalGameState.Single)
            {
                yield return GameSettings.theOnlyGameManager.GetComponent<SceneLoadController>().LoadSceneCoroutine(0, GlobalGameState.Double, true);
            }
            else if (GameSettings.globalGameState == GlobalGameState.Double)
            {
                yield return GameSettings.theOnlyGameManager.GetComponent<SceneLoadController>().LoadSceneCoroutine(0, GlobalGameState.Final, true);
            }
            else
            {
                yield return GameSettings.theOnlyGameManager.GetComponent<SceneLoadController>().LoadSceneCoroutine(0, GlobalGameState.Single, true);
            }

            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());

            yield return null;
            GameObject.FindWithTag("FinalClueControl").GetComponent<FinalClueControl>().SetBoardClueChild(gameObject);
        }
    }
}