public class WorkListViewData
{
    public WorkingStep WorkingStep { get; }
    public int AvailableAmount { get; }
    public int MaxSelectableAmount { get; }
    public int PressMachineLevel { get; }

    public WorkListViewData(WorkingStep workingStep, int availableAmount, int maxSelectableAmount, int pressMachineLevel)
    {
        WorkingStep = workingStep;
        AvailableAmount = availableAmount;
        MaxSelectableAmount = maxSelectableAmount;
        PressMachineLevel = pressMachineLevel;
    }
}