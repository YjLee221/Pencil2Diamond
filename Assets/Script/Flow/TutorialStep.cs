/*
 * TutorialStep
 튜토리얼 진행 단계
 */
public enum TutorialStep
{
    None,

    OpeningDialog,      // 튜토리얼 시작 대화
    SharpeningPencil,     // 연필 깎기

    GraphiteDialog,       // 흑연 추출 대화
    ExtractingGraphite,   // 흑연 추출

    TemperatureDialog,    // 온도 조절 대화
    AdjustingTemperature, // 온도 조절

    SellingDialog,        // 보석 판매 대화
    SellingDiamond,       // 보석 판매

    EndingDialog,         // 튜토리얼 종료 대화
    Completed
}