using Syncfusion.Maui.ImageEditor;

namespace CameraPicker;

internal class CameraPickerBehavior : Behavior<ContentPage>
{
    private SfImageEditor imageEditor;
    private Button cameraClicked;
    protected override void OnAttachedTo(ContentPage bindable)
    {
        base.OnAttachedTo(bindable);
        this.imageEditor = bindable.FindByName<SfImageEditor>("imageEditor");
        this.cameraClicked = bindable.FindByName<Button>("cameraClicked");
        if (this.cameraClicked != null)
        {
            this.cameraClicked.Clicked += OnButtonClicked;
        }
    }
    private void OnButtonClicked(object sender, EventArgs e)
    {
        GetImageEditorImageSourceAsync();
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
    protected override void OnDetachingFrom(ContentPage bindable)
    {
        base.OnDetachingFrom(bindable);
        if (this.cameraClicked != null)
        {
            this.cameraClicked.Clicked -= OnButtonClicked;
        }
    }
}