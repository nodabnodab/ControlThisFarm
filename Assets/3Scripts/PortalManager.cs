using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public Portal[] portals; // 모든 포탈을 배열로 관리

    private void Start()
    {
        // 포탈 페어링을 자동으로 설정
        for (int i = 0; i < portals.Length; i += 2)
        {
            if (i + 1 < portals.Length)
            {
                // 인접한 두 포탈을 페어로 설정
                portals[i].linkedPortal = portals[i + 1];
                portals[i + 1].linkedPortal = portals[i];
            }
        }
    }
}
