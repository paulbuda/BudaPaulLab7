using BudaPaulLab7.Models;
using Microsoft.Maui.Controls; // Ensure this is present for ContentPage and InitializeComponent

namespace BudaPaulLab7;

public partial class ShopEntryPage : ContentPage
{
	public ShopEntryPage()
	{
		InitializeComponent(); // This requires a corresponding ShopEntryPage.xaml file
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		listView.ItemsSource = await App.Database.GetShopsAsync();
	}

	async void OnShopAddedClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ShopPage
		{
			BindingContext = new Shop()
		});
	}

	async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem != null)
		{
			await Navigation.PushAsync(new ShopPage
			{
				BindingContext = e.SelectedItem as Shop
			});
		}
	}
}