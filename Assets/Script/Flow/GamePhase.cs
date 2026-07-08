/*
 * GamePhase
 게임 전체에서 현재 플레이어가 위치한 큰 흐름
화면과 입력 규칙이 크게 바뀌는 단위로 구분
 */
public enum GamePhase
{
    None,       // 아직 게임 흐름이 시작되지 않음
    Tutorial,   // 튜토리얼 진행 중
    Workshop    // 메인 공방 플레이 중
}