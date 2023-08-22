$(document).ready(function () {
    var customerId = JSON.parse($("#customerId").html());
    var token = JSON.parse($("#token").html());
    // Call the function to fetch and replace data
    

    fetchData();
    function fetchData() {
        $.ajax({
            url: 'https://localhost:7012/api/Orders/GetAllCartItems/' + customerId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                populateTable(data);
            },
            error: function (error) {
                console.log('Error fetching data:', error);
            }
        });
    }
    function populateTable(data) {
        var tableBody = $('#cartTable tbody');
        tableBody.empty(); // Clear existing rows
        
        $("#bill").text("");
        $("#bill").text(data[0].totalBill.toLocaleString() +" VNĐ");

        data.forEach(function (od) {
            var row = `
                <tr>
                    <td data-th="Product">
                        <div class="row">
                            <div class="col-md-3 text-left">
                                <img src="/${od.image}" alt="" class="img-fluid d-none d-md-block rounded mb-2 shadow">
                            </div>
                            <div class="col-md-9 text-left mt-sm-2">
                                <h4>${od.productName}</h4>
                            </div>
                        </div>
                    </td>
                    <td data-th="Price" name="price">${od.unitPrice.toLocaleString() }</td>
                    <td data-th="Quantity" name="quantity">
                        <div class="input-group">
                            <input type="hidden" name="proid" value="${od.productId}">
                            <span class="input-group-prepend">
                                <button name="quan" value="0" type="button" class="btn btn-sm btn-danger btn-number" data-type="minus" data-field="quant[1]">
                                    <span class="fa fa-minus"></span>
                                </button>
                            </span>
                            <input type="number" readonly class="form-control form-control-lg text-center quantyti" value="${od.quantity}">
                            <span class="input-group-append">
                                <button name="quan" value="1" type="button" class="btn btn-sm btn-success btn-number" data-type="plus" data-field="quant[1]">
                                    <span class="fa fa-plus"></span>
                                </button>
                            </span>
                        </div>
                    </td>
                    <td class="text-right" data-th="Price" name="price">${(od.unitPrice * od.quantity).toLocaleString()}</td>
                    <td class="actions" data-th="">
                        <div class="text-right">
                            <button name="remove" class="btn btn-white border-secondary bg-white btn-md mb-2">
                                <i class="fas fa-trash"></i>
                            </button>
                        </div>
                    </td>
                </tr>
            `;
            tableBody.append(row);
        });
    }

    $(document).on('click', 'button[name="quan"]', function () {
        var row = $(this).closest('tr');
        var productId = row.find('input[name="proid"]').val();
        var sign = $(this).val() === '1'; // true for "plus" button, false for "minus" button

        // Create the data object to be sent in the AJAX request
        var dataToSend = {
            customerId: customerId,
            productId: productId,
            sign: sign
        };
        //console.log(dataToSend);
        
        $.ajax({
            url: 'https://localhost:7012/api/Orders/ChangeQuantityInCart',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dataToSend),
            success: function (response) {
                // Handle the response here, if needed
                //console.log('AJAX request successful:', response);
                fetchData();
                handleGetSizeCart();
            },
            error: function (error) {
                // Handle errors here, if needed
                console.log('AJAX request failed:', error);
            }
        });
    });

    $(document).on('click', 'button[name="remove"]', function () {
        var row = $(this).closest('tr');
        var productId = row.find('input[name="proid"]').val();
        var dataToSend = {
            customerId: customerId,
            productId: productId,
        };
        $.ajax({
            url: 'https://localhost:7012/api/Orders/RemoveFromCart',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dataToSend),
            success: function (response) {
                // Handle the response here, if needed
                //console.log('AJAX request successful:', response);
                fetchData();
                handleGetSizeCart();
            },
            error: function (error) {
                // Handle errors here, if needed
                console.log('AJAX request failed:', error);
            }
        });
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
