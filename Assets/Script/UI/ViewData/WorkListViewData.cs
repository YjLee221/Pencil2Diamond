public class WorkListViewData
{
    public WorkingStep WorkingStep { get;  private set; }
    public int AvailableAmount { get; private set; }
    public int MaxSelectableAmount { get; private set; }
    public int PressMachineLevel { get; private set; }

    public WorkListViewData(WorkingStep workingStep, int availableAmount, int maxSelectableAmount, int pressMachineLevel)
    {
        WorkingStep = workingStep;
        AvailableAmount = availableAmount;
        MaxSelectableAmount = maxSelectableAmount;
        PressMachineLevel = pressMachineLevel;
    }
}