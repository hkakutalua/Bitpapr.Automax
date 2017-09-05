namespace Bitpapr.Automax.Navigation
{

    public interface IDialogService
    {
        void ShowDetailedDialog(string titleMessage, string contentMessage,
            string detailMessage, DialogType dialogType);
    }
}