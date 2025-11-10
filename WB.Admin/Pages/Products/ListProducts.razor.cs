using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using WB.Admin.Extensions;
using WB.Admin.Pages.Products.Dialogs;
using WB.Shared.Configs;
using WB.Shared.Dtos;
using WB.Shared.Dtos.Product.ResponseDtos;

namespace WB.Admin.Pages.Products
{
    public partial class ListProducts : ComponentBase
    {
        #region Variables Declaration
        private IList<ProductListResponseDto> ProductList { get; set; } = new List<ProductListResponseDto>();
        private IList<ProductListResponseDto> FilteredProductList { get; set; } = new List<ProductListResponseDto>();
        protected RadzenDataGrid<ProductListResponseDto>? productsGrid = new RadzenDataGrid<ProductListResponseDto>();
        protected string search = string.Empty;
        #endregion

        #region On Component Load 
        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            StateHasChanged();
        }
        #endregion

        #region On Load Grid Data
        protected async Task LoadData()
        {
            spinnerService.Show();
            try
            {
                var response = await apiHelper.SendGetAsync<List<ProductListResponseDto>>($"Product/GetProducts");
                if (response.IsSuccessStatusCode)
                {
                    ProductList = (List<ProductListResponseDto>)response.ResultData;

                    FilteredProductList = ProductList;
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    await invalidRequestHandler.ReturnBadRequestNotification(response);
                }
            }
            catch (Exception ex)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
            spinnerService.Hide();
        }
        #endregion

        #region Grid Search
        protected async Task OnSearchInput()
        {
            try
            {
                if (ProductList == null || ProductList.Count == 0)
                {
                    FilteredProductList = new List<ProductListResponseDto>();
                    return;
                }

                search = string.IsNullOrEmpty(search) ? "" : search.TrimStart().TrimEnd().ToLower();

                FilteredProductList = await gridSearchExtension.Filter(ProductList, new Query()
                {
                    Filter = $@"i => (i.NameEn != null && i.NameEn.ToLower().Contains(@0)) || (i.NameAr != null && i.NameAr.ToLower().Contains(@0))",

                    FilterParameters = new object[] { search }
                });

                await InvokeAsync(StateHasChanged);

            }
            catch (Exception ex)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }

        #endregion

        private async Task UpdateProduct(ProductListResponseDto product)
        {
            try
            {
                var result = await dialogService.OpenAsync<EditProductDialog>(translationState.Translate("Edit_Product"),
                    new Dictionary<string, object> {
                    { "ProductId", product.Id }
                    },
                    new DialogOptions() { Width = "30%" });
                if (result != null)
                {
                    var index = FilteredProductList.IndexOf(product);
                    if (index != -1)
                    {
                        FilteredProductList[index] = result;
                    }
                }
                await productsGrid.RefreshDataAsync();
                await InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }

        private async Task ViewProduct(ProductListResponseDto product)
        {
            try
            {
                var result = await dialogService.OpenAsync<ViewProductDialog>(translationState.Translate("View_Product"),
                    new Dictionary<string, object> {
                    { "ProductId", product.Id }
                    },
                    new DialogOptions() { Width = "50%" , Height = "80%" });
                if (result != null)
                {
                    var index = FilteredProductList.IndexOf(product);
                    if (index != -1)
                    {
                        FilteredProductList[index] = result;
                    }
                }
                await productsGrid.RefreshDataAsync();
                await InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                notificationService.Notify(new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Detail = translationState.Translate("Something_went_wrong_Please_try_again"),
                });
            }
        }
    }
}
