using UnityEngine;
using System.Collections.Generic;

public class SimpleHoseConnector : MonoBehaviour
{
    public Transform startPoint;    // 시작점
    public Transform endPoint;      // 끝점
    public GameObject segmentPrefab;// 세그먼트 프리팹
    public int segmentCount = 10;   // 세그먼트 개수

    private List<GameObject> segments = new List<GameObject>();

    void Start()
    {
        CreateSegments();
    }

    void Update()
    {
        UpdateSegments();
    }

    void CreateSegments()
    {
        // 기존 세그먼트 삭제
        foreach (var seg in segments)
        {
            if (seg != null) Destroy(seg);
        }
        segments.Clear();

        // 새 세그먼트 생성
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject segment = Instantiate(segmentPrefab, transform);
            segments.Add(segment);
        }
    }

    void UpdateSegments()
    {
        if (startPoint == null || endPoint == null || segments.Count == 0) return;

        for (int i = 0; i < segments.Count; i++)
        {
            float t = (float)i / (segments.Count - 1);
            Vector3 pos = Vector3.Lerp(startPoint.position, endPoint.position, t);
            segments[i].transform.position = pos;
            
            // 방향도 맞추고 싶다면 (선택사항)
            if (i < segments.Count - 1)
            {
                Vector3 nextPos = Vector3.Lerp(startPoint.position, endPoint.position, (float)(i + 1) / (segments.Count - 1));
                Vector3 dir = (nextPos - pos).normalized;
                segments[i].transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(90, 0, 0); // Capsule용 회전
            }
        }
    }
}