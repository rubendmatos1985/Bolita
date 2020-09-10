using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Bolita.Services;
using Bolita.Views;

namespace Bolita
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();
			Device.SetFlags(new[] { "Brush_Experimental", "Shapes_Experimental" });
			DependencyService.Register<MockDataStore>();
			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
