using Syncfusion.Maui.ImageEditor;
using System.Globalization;
using System.Resources;

namespace CameraPicker;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
        
    }
}
