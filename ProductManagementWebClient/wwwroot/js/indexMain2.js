$(document).ready(function () {
    var token = JSON.parse($("#token").html());

    var categoryId = $('#curentCategoryId').val();
    console.log(categoryId);
    ShowProduct();
    function ShowProduct() {
        $.ajax({
            url: 'https://localhost:7012/api/Products/GetProductByCategoryId/' + categoryId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer" + token
            },
            success: function (data) {
                // Handle the data returned by the API
                //displayProducts(data);


                $('#demo').pagination({
                    dataSource: data, // Assuming your API response is an array of products
                    pageSize: 20,
                    showGoInput: true,
                    showGoButton: true,
                    callback: function (data, pagination) {
                        // Update product display
                        var html = displayProducts(data);
                        $('#demo').html(html);

                        // Generate pagination HTML
                        //var paginationHtml = generatePaginationHtml(pagination);
                        //$('#pagination-container').html(paginationHtml);
                    }
                });
            },
            error: function (xhr, status, error) {
                // Handle any errors that occurred during the AJAX call
                console.error(error);
            }
        });
    }

    function displayProducts(products) {
        var productContainer = $('#productHome2');

        // Clear the existing content in the product container
        productContainer.empty();

        // Iterate over the products and create the HTML dynamically
        for (var i = 0; i < products.length; i++) {
            var product = products[i];

            var html = '<div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 margintop">' +
                '<div class="shoes-box">' +
                '<i><img src="/' + product.image + '"></i>' +
                '<p><span class="nolmal">' + product.productName + '</span></p>' +
                '<h4>Price: <span class="nolmal monney" style="color: red">' + product.unitPrice + '</span>đ<br /><span style="font-size: 70%;">kho:' + product.unitsInStock + '</span></h4>' +
                '</div>' +
                '<div style="display: flex;">' +
                '<a class="buynow" name="buynow" onclick="buynow()">Buy now</a>' +
                '<input class="getPid" type="hidden" value="' + product.productId + '" />' +
                '<a class="buynow" name="addtocard" onclick="buy()">Add to cart</a>' +
                '</div>' +
                '</div>';

            // Append the dynamically created HTML to the product container
            productContainer.append(html);
        }
    }

});