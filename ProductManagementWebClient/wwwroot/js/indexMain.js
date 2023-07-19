$(document).ready(function () {
  //  $('.new-shoes').slick({
  //      prevArrow: $('.prev'),
  //      nextArrow: $('.next')
  //});
   //$('.view-shoes').slick({
   //    prevArrow: $('.prev-view'),
   //    nextArrow: $('.next-view')
   //});

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
                        .append('<h4>Price: <span class="nolmal monney" style="color: red">' + product.unitPrice + '</span>đ <br><span style="font-size: 70%;">kho:' + product.unitsInStock + '</span></h4>')
                    );
                    item.append($('<div style="display: flex;">')
                        .append('<a class="buynow" name="buynow" onclick="buynow()">Buy now</a>')
                        .append('<input class="getPid" type="hidden" value="' + product.productId + '">')
                        .append('<a class="buynow" name="addtocard" onclick="buy()">Add to cart</a>')
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
                        .append('<h4>Price: <span class="nolmal monney" style="color: red">' + product.unitPrice + '</span>đ <br><span style="font-size: 70%;">kho:' + product.unitsInStock + '</span></h4>')
                    );
                    item.append($('<div style="display: flex;">')
                        .append('<a class="buynow" onclick="buynow()">Buy now</a>')
                        .append('<input class="getPid" type="hidden" value="' + product.productId + '">')
                        .append('<a class="buynow" onclick="buypn()">Add to cart</a>')
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

    

});
