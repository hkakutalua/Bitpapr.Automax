namespace Bitpapr.Automax.Navigation
{
    /// <summary>
    /// Interface for implementers that want to show dialogs
    /// </summary>
    public interface IDialogService
    {
        void ShowDetailedDialog(string titleMessage, string contentMessage,
            string detailMessage, DialogType dialogType);
    }
}