using BudaPaulLab7.Models; // Add this line if ShopList is in the Models namespace

namespace BudaPaulLab7;

public partial class ListEntryPage : ContentPage
{
	public ListEntryPage()
	{
		this.InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		listView.ItemsSource = await App.Database.GetShopListsAsync();
	}

	async void OnShopListAddedClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ListPage
		{
			BindingContext = new ShopList()
		});
	}

	async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem != null)
		{
			await Navigation.PushAsync(new ListPage
			{
				BindingContext = e.SelectedItem as ShopList
			});
		}
	}
}