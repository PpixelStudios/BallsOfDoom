using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public Transform[] waypoints; // Array de waypoints em ordem.

    private void OnDrawGizmos()
    {
        // Desenha linhas entre os waypoints para facilitar o design da rota.
        if (waypoints != null && waypoints.Length > 1)
        {
            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }
}
