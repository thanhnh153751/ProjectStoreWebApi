$(document).ready(function () {
    var customerId = JSON.parse($("#customerId").html());
    var token = JSON.parse($("#token").html());

    var categoryId = $('#curentCategoryId').val();


    //---search funtion---
    var keySearch = $('#curentKeySearch').val();
    if (keySearch !== null && keySearch.trim() !== '') {
        $('#searchInput').val(keySearch);
        performSearchAndNavigate();
    } else {
        ShowProduct(categoryId, null);
    }

    function performSearchAndNavigate() {
        var searchValue = $('#searchInput').val();
        if (searchValue === "") {
            return;
        }
        ShowProduct(null, searchValue);
    }
    $("#searchButton").click(function () {
        performSearchAndNavigate();
    });
    document.getElementById("searchInput").addEventListener("keyup", function (event) {
        if (event.keyCode === 13) {
            performSearchAndNavigate();
        }
    });
    //---search funtion end---

    $('.cateid a').each(function () {
        var hrefParts = $(this).attr('href').split('/');
        var categoryIdFromHref = hrefParts[hrefParts.length - 1];
        if (categoryIdFromHref === categoryId) {
            $(this).css('background-color', '#ee3e0f');
        }
    });
    //console.log(categoryId);
    function convertODataToRegularArray(odataResponse) {
        if (odataResponse && odataResponse.value) {
            return odataResponse.value;
        }
        return [];
    }
    function ShowProduct(categoryId, searchKeyword) {
        var apiUrl;
        if (searchKeyword) {           
            apiUrl = 'https://localhost:7012/odata/Products?$filter=contains(tolower(productName), tolower(\'' + searchKeyword + '\')) or contains(tolower(description), tolower(\'' + searchKeyword + '\')) or contains(tolower(categoryName), tolower(\'' + searchKeyword + '\'))';
        } else {
            apiUrl = 'https://localhost:7012/api/Products/GetProductByCategoryId/' + categoryId;
        }
        $.ajax({
            url: apiUrl,
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer" + token
            },
            success: function (data) {
                // Handle the data returned by the API
                //displayProducts(data);
                //console.log(data);
                // Convert "odata" formatted data to a regular array
                var dataArray = Array.isArray(data) ? data : convertODataToRegularArray(data);


                    $('#demo').pagination({
                        dataSource: dataArray, // Assuming your API response is an array of products
                        pageSize: 20,
                        showGoInput: true,
                        showGoButton: true,
                        callback: function (data, pagination) {
                            // Update product display
                            var html = displayProducts(data);
                            $('#demo').html(html);
                        }
                    });               
               
            },
            error: function (xhr, status, error) {
                // Handle any errors that occurred during the AJAX call
                console.error(error);
                console.log("getAll error:", error);
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
                '<h4>Price: <span class="nolmal monney" style="color: red">' + product.unitPrice.toLocaleString() + '</span>đ<br /><span style="font-size: 70%;">kho:' + product.unitsInStock + '</span></h4>' +
                '</div>' +
                '<div style="display: flex;">' +
                '<a class="buynow" tabindex="0" id="buynow" data-product-id="' + product.productId +'"  ">Buy now</a>' +
                '<a class="buynow" tabindex="0" id="addtocard" data-product-id="' + product.productId +'" >Add to cart</a>' +
                '</div>' +
                '</div>';

            // Append the dynamically created HTML to the product container
            productContainer.append(html);
        }
    }

    $(document).on("click", "#buynow", function () {
        var productId = $(this).data("product-id");

        //console.log(productId + customerId);

        if (customerId == null) {
            window.location.href = "/Login/LoginRegister";
        } else {
            $.ajax({
                url: "https://localhost:7012/api/Orders/AddToCart",
                type: "POST",
                data: JSON.stringify({
                    "productId": productId,
                    "customerId": customerId
                }),
                contentType: "application/json; charset=utf-8",
                headers: {
                    "Authorization": "Bearer" + token
                },
                success: function (data) {
                    //console.log("AddToCart success:", data);
                    window.location.href = "/Order/ListShoppingCart";
                },
                error: function (error) {
                    // Xử lý lỗi (nếu cần)
                    console.log("buynow error:", error);
                }
            });
        }

    });

    $(document).on("click", "#addtocard", function () {
        var productId = $(this).data("product-id");

        if (customerId == null) {
            window.location.href = "/Login/LoginRegister";
        } else {
            $.ajax({
                url: "https://localhost:7012/api/Orders/AddToCart",
                type: "POST",
                data: JSON.stringify({
                    "productId": productId,
                    "customerId": customerId
                }),
                contentType: "application/json; charset=utf-8",
                headers: {
                    "Authorization": "Bearer" + token
                },
                success: function (data) {
                    // Xử lý kết quả thành công (nếu cần)
                    //console.log("AddToCart success:", data);
                    handleGetSizeCart();
                },
                error: function (error) {
                    // Xử lý lỗi (nếu cần)
                    console.log("AddToCart error:", error);
                }
            });
        }

    });

    function handleGetSizeCart() {

        $.ajax({
            url: "https://localhost:7012/api/Orders/GetSizeCart/" + customerId,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer" + token
            },
            success: function (cartSizeData) {
                //console.log("GetSizeCart :", cartSizeData);
                updateCartSize(cartSizeData);
            },
            error: function (error) {

                console.log("GetSizeCart error:", error);
            }
        });
    }
    function updateCartSize(cartSize) {
        $("#numberItems").text("");
        $("#numberItems").text(cartSize);
    }

});