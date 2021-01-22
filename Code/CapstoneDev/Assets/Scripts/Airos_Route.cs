using UnityEngine;

public class Airos_Route : MonoBehaviour
{
    [SerializedField]
    private Transform[] controlSpots;

    private Vector2 TracPosition;

    private void OnDraw()
    {
        for (float t = 0; t <= 1; t += .05)
        {
            TracPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position +
                       3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position +
                       3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position +
                        Mathf.Pow(t, 3) * controlPoints[3].position;

            Gizmos.DrawSphere(TracPosition.Position, 0.25);
        }

        Gizmos.DrawLine(new Vector2(controlSpots[0].position.x, controlSpots[0].position.y),
            new Vector2(controlSpots[1].position.x, controlSpots[1].position.y));

        Gizmos.DrawLine(new Vector2(controlSpots[2].position.x, controlSpots[2].position.y),
            new Vector2(controlSpots[3].position.x, controlSpots[3].position.y));
    }


