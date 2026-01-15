using BudaPaulLab7.Models;

namespace BudaPaulLab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		var shopl = (ShopList)BindingContext;

		listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		var slist = (ShopList)BindingContext;
		slist.Date = DateTime.UtcNow;
		await App.Database.SaveShopListAsync(slist);
		await Navigation.PopAsync();
	}

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		var slist = (ShopList)BindingContext;
		await App.Database.DeleteShopListAsync(slist);
		await Navigation.PopAsync();
	}

	async void OnChooseButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
		{
			BindingContext = new Product()
		});
	}

	async void OnDeleteItemButtonClicked(object sender, EventArgs e)
	{
		if (listView.SelectedItem != null)
		{
			var selectedProduct = (Product)listView.SelectedItem;
			var shopList = (ShopList)BindingContext;

			bool answer = await DisplayAlert("Confirm Delete", 
				$"Are you sure you want to delete '{selectedProduct.Description}' from this shopping list?", 
				"Yes", "No");

			if (answer)
			{
				await App.Database.DeleteListProductAsync(shopList.ID, selectedProduct.ID);
				listView.ItemsSource = await App.Database.GetListProductsAsync(shopList.ID);
			}
		}
		else
		{
			await DisplayAlert("No Selection", "Please select an item to delete.", "OK");
		}
	}
}