namespace CameraPicker;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    private void Button_Clicked(object sender, EventArgs e)
    {
        Task task = GetImageEditorImageSourceAsync();
        this.imageEditor.IsVisible = true;
    }
    private async Task GetImageEditorImageSourceAsync()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            // Camera permission is not granted, request it
            status = await Permissions.RequestAsync<Permissions.Camera>();
            if (status != PermissionStatus.Granted)
            {
                // Camera permission denied by the user
                // You may show an alert or take appropriate action
                return;
            }
        }

        var photo = await MediaPicker.Default.CapturePhotoAsync();
        if (photo != null)
        {
            // Get the stream of the captured photo and set it as the source of the Image Editor control.
            imageEditor.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);
        }
    }
}