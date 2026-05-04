using UnityEngine;

public class UIUtills
{
    public static Rect GetScreenRect(RectTransform rt)
    {
        // 네 모서리의 월드 좌표를 담을 배열 (0:좌하, 1:좌상, 2:우상, 3:우하)
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        // 회전되거나 크기가 변한 상태를 모두 포괄할 수 있도록 최소/최대 X, Y값을 찾습니다.
        float minX = corners[0].x;
        float minY = corners[0].y;
        float maxX = corners[0].x;
        float maxY = corners[0].y;

        for (int i = 1; i < 4; i++)
        {
            if (corners[i].x < minX) minX = corners[i].x;
            if (corners[i].x > maxX) maxX = corners[i].x;
            if (corners[i].y < minY) minY = corners[i].y;
            if (corners[i].y > maxY) maxY = corners[i].y;
        }

        // 찾아낸 최소/최대값을 바탕으로 사각형(Rect)을 생성해 반환합니다.
        return new Rect(minX, minY, maxX - minX, maxY - minY);
    }
}
