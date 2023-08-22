$(document).ready(function () {
    var customerId = JSON.parse($("#customerId").html());
    var token = JSON.parse($("#token").html());
    // Call the function to fetch and replace data
    getNewTopProduct();
    function getNewTopProduct() {
        $.ajax({
            url: "https://localhost:7012/odata/Products?$orderby=productId desc&$top=10",
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: function (result, status, xhr) {
                // Clear the existing content in the data_newshoes container
                $("#data_newshoes").html("");

                // Loop through the products and append them to the data_newshoes container
                $.each(result.value, function (index, product) {
                    var item = $('<div class="col-xl-3 col-lg-3 col-md-6 col-sm-12">');
                    item.append($('<div class="shoes-box">')
                        .append('<i><img src="/' + product.image + '"></i>')
                        .append('<p class="nolmal">' + product.productName + '</p>')
                        .append('<h4>Price: <span class="nolmal monney" style="color: red">' + product.unitPrice.toLocaleString() + '</span>đ <br><span style="font-size: 70%;">kho:' + product.unitsInStock + '</span></h4>')
                    );
                    item.append($('<div style="display: flex;">')
                        .append('<a class="buynow" id="buynow" data-product-id="' + product.productId +'" >Buy now</a>')                       
                        .append('<a class="buynow" id="addtocard" data-product-id="' + product.productId +'" >Add to cart</a>')
                    );

                    $("#data_newshoes").append(item);
                });

                // Reinitialize the Slick slider 
                $(".new-shoes").slick({
                    slidesToShow: 4,
                    slidesToScroll: 4,
                    prevArrow: $('.prev'),
                    nextArrow: $('.next')
                });
            },
            error: function (xhr, status, error) {
                console.log("Error fetching data: " + error);
            }
        });
    }
    getTopProductByView();
    function getTopProductByView() {
        $.ajax({
            url: "https://localhost:7012/api/Products/GetProductsByView",
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: function (result, status, xhr) {
                // Clear the existing content in the data_viewTopShoes container
                $("#data_viewTopShoes").html("");

                // Loop through the products and append them to the data_viewTopShoes container
                $.each(result, function (index, product) {
                    var item = $('<div class="col-xl-3 col-lg-3 col-md-6 col-sm-12 margintop">');
                    item.append($('<div class="shoes-box">')
                        .append('<i><img src="/' + product.image + '"></i>')
                        .append('<p><span class="nolmal">' + product.productName + '</span></p>')
                        .append('<h4>Price: <span class="nolmal monney" style="color: red">' + product.unitPrice.toLocaleString() + '</span>đ <br><span style="font-size: 70%;">kho:' + product.unitsInStock + '</span></h4>')
                    );
                    item.append($('<div style="display: flex;">')
                        .append('<a class="buynow" id="buynow" data-product-id="' + product.productId +'"  ">Buy now</a>')
                        .append('<a class="buynow" id="addtocard" data-product-id="' + product.productId +'" >Add to cart</a>')
                    );

                    $("#data_viewTopShoes").append(item);
                });
                // Reinitialize the Slick slider 
                $(".view-shoes").slick({
                    slidesToShow: 4,
                    slidesToScroll: 4,
                    prevArrow: $('.prev-view'),
                    nextArrow: $('.next-view')
                });
            },
            error: function (xhr, status, error) {
                console.log("Error fetching data: " + error);
            }
        });
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
