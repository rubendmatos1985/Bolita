using System.ComponentModel;
using Xamarin.Forms;
using Bolita.ViewModels;

namespace Bolita.Views
{
	public partial class ItemDetailPage : ContentPage
	{
		public ItemDetailPage()
		{
			InitializeComponent();
			BindingContext = new ItemDetailViewModel();
		}
	}
}