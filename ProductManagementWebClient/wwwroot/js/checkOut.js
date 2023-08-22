$(document).ready(function () {
    var customerId = JSON.parse($("#customerId").html());
    var token = JSON.parse($("#token").html());

    fetchData();
    function fetchData() {
        $.ajax({
            url: 'https://localhost:7012/api/Orders/GetAllCartItems/' + customerId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                populateTable(data);
                SentRequestOrder();
            },
            error: function (error) {
                console.log('Error fetching data:', error);
            }
        });
    }
    fetchDataCustomer();
    function fetchDataCustomer() {
        $.ajax({
            url: 'https://localhost:7012/api/Customers/GetMember/' + customerId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#cus-phone").text("");
                $("#cus-address").text("");
                $("#cus-address").text(data.address);
                $("#cus-phone").text(data.phone);
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
        $("#bill").text(data[0].totalBill.toLocaleString() + " VNĐ");

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
                    <td data-th="Price" name="price">${od.unitPrice.toLocaleString()}</td>
                    <td data-th="Quantity" name="quantity">
                        <div class="input-group">
                            <input type="hidden" name="proid" value="${od.productId}">
                            
                            <input type="number" readonly class="form-control form-control-lg text-center quantyti" value="${od.quantity}">
                            
                        </div>
                    </td>
                    <td class="text-right" data-th="Price" name="price">${(od.unitPrice * od.quantity).toLocaleString()}</td>
                    
                </tr>
            `;
            tableBody.append(row);
        });
    }

    function SentRequestOrder() {
        $.ajax({
            url: 'https://localhost:7012/api/Orders/SentRequestOrder/' + customerId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            success: function () {
                handleGetSizeCart();
            },
            error: function (error) {
                console.log('Error fetching data:', error);
            }
        });
    }

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
