$(document).ready(function () {
    var customerId = JSON.parse($("#customerId").html());
    var token = JSON.parse($("#token").html());
    ShowAllCategorys();
    function ShowAllCategorys() {
        $("table tbody").html("");
        $.ajax({
            url: "https://localhost:7012/api/Orders/GetAllHistoryOrderByCumtomerId/" + customerId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer" + token
            },
            success: function (result, status, xhr) {
                // Populate the table with data
                $.each(result, function (index, value) {
                    var appendElement = $("<tr>");
                    appendElement.append($("<td>").html(value.orderId));
                    appendElement.append($("<td>").html(value.requiredDate));
                    appendElement.append($("<td>").html(value.totalmoney.toLocaleString()));
                    appendElement.append($("<td>").html(value.phone));
                    appendElement.append($("<td>").html(value.address));
                    appendElement.append($("<td>").html(value.status));

                    // Check if the status is not "approval" before adding the delete button
                    if (value.status !== "approval") {
                        appendElement.append($("<td>").html("<button class=\"btn btn-warning updateBtn\" data-product-id=\"" + value.orderId + "\">View Detail</button>"
                            + "<button class=\"btn ml-2 btn-danger deleteBtn\" data-product-id=\"" + value.orderId + "\">Cancel Order</button>"));
                    } else {
                        appendElement.append($("<td>").html("<button class=\"btn btn-warning updateBtn\" data-product-id=\"" + value.orderId + "\">View Detail</button>"));
                    }

                    $("tbody").append(appendElement);
                });

                // Initialize the DataTable
                $('#productTable').DataTable({
                    // Example options
                    paging: true, // Enable pagination
                    searching: true, // Enable search functionality
                    ordering: true, // Enable column sorting
                    lengthMenu: [4, 10, 25, 50],
                    pageLength: 4
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr);

            }
        });
    }


    function createModelFromFormData(formData) {
        var model = {};
        $.each(formData, function (index, field) {
            model[field.name] = field.value;
        });
        return model;
    }

    // Handle submit create button click
    $("#submitBtn").click(function () {
        // Get the form data
        var formData = $("#addCategoryForm").serializeArray();
        // Create the model object from the form data
        var model = createModelFromFormData(formData);
        // Send the form data to the API
        $.ajax({
            url: "https://localhost:7012/api/Categorys/postCategory",
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(model),
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                //console.log(result);

                // Close the modal
                $("#CategoryAddModal").modal("hide");

                // Destroy the existing DataTable instance
                $('#productTable').DataTable().destroy();
                ShowAllCategorys();
                // Clear the form fields
                //$("#contactForm")[0].reset();

                // Save the image file
                //saveImageFile();
                showSuccessToast("Successfully Created");
            },
            error: function (xhr, status, error) {
                // Handle the error response
                console.log("loi");
                console.log(xhr);
            }
        });
    });


    // Click event listener for the delete buttons
    $(document).on("click", ".deleteBtn", function () {
        var productId = $(this).data("product-id");
        // Fetch product details and populate the modal
        fetchProductDetails(productId);

        // Show the modal
        $("#deleteModal").modal("show");
    });

    // Click event listener for the update buttons
    $(document).on("click", ".updateBtn", function () {
        var productId = $(this).data("product-id");

        // Fetch product details and populate the modal
        fetchProductDetailsUpdate(productId);
        
        // Show the modal
        $("#updateModal").modal("show");
    });

    // Function to fetch product details and populate the modal
    function fetchProductDetails(productId) {
        $.ajax({
            url: "https://localhost:7012/api/Orders/GetOrderDetailByOrderId/" + productId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                // Populate the modal fields with the fetched data
                var product = result[0];

                // Populate the modal fields with the fetched data
                $("#orderid_de").text("");
                $("#orderId-de").val("");
                $("#orderid_de").text("OrderID: " + product.orderId);
                $("#orderId-de").val(product.orderId);
            },
            error: function (xhr, status, error) {
                console.log("loi");
                console.log(xhr);
            }
        });
    }

    // Function to fetch product details and populate the modal
    function fetchProductDetailsUpdate(productId) {
        $.ajax({
            url: "https://localhost:7012/api/Orders/GetOrderDetailByOrderId/" + productId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                // Populate the modal fields with the fetched data
                var products = result; // Since the result is an array of products
                var tableBody = $("#table-body");

                // Clear the table body before populating it
                tableBody.empty();

                // Loop through the products and add them to the table
                for (var i = 0; i < products.length; i++) {
                    var product = products[i];
                    var row = $("<tr></tr>");

                    // Create the columns for each property
                    var orderIdCell = $("<td></td>").text(product.orderId);
                    var productNameCell = $("<td></td>").text(product.productName);
                    var imageCell = $("<td></td>").html('<img src="/' + product.image + '" alt="' + product.productName + '" style="max-width: 100px; max-height: 100px;">');
                    var unitPriceCell = $("<td></td>").text(product.unitPrice.toLocaleString());
                    var quantityCell = $("<td></td>").text(product.quantity);

                    // Append the columns to the row
                    row.append(orderIdCell);
                    row.append(productNameCell);
                    row.append(imageCell);
                    row.append(unitPriceCell);
                    row.append(quantityCell);

                    // Append the row to the table body
                    tableBody.append(row);
                }

            },
            error: function (xhr, status, error) {
                console.log("loi");
                console.log(xhr);
            }
        });
    }

    // Handle submit button delete click
    $("#submitBtn-de").click(function () {
        var productId = $("#orderId-de").val();
        //console.log(productId +"-----");

        // Send the form data to the API
        $.ajax({
            url: "https://localhost:7012/api/Orders/DeleteOrderByOrderId/" + productId,
            type: "delete",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                //console.log(result);

                // Close the modal
                $("#deleteModal").modal("hide");
                // Destroy the existing DataTable instance
                $('#productTable').DataTable().destroy();
                ShowAllCategorys();
                showSuccessToast("Successfully Deleted");
            },
            error: function (xhr, status, error) {
                // Handle the error response
                console.log("loi");
                console.log(xhr);
            }
        });
    });




    function showSuccessToast(message) {
        var toastContainer = $(".toast-container");

        var toast = $('<div class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-autohide="true" data-delay="5000"></div>');
        var toastHeader = $('<div class="toast-header bg-primary text-white"></div>');
        var toastTitle = $('<strong class="me-auto"></strong>').text("Success");
        var closeButton = $('<button type="button" class="ml-2 mb-1 mt-1 close" data-dismiss="toast" aria-label="Close"></button>');
        closeButton.append('<span aria-hidden="true">&times;</span>');

        var toastBody = $('<div class="toast-body"></div>').text(message);

        toastHeader.append(toastTitle);
        toastHeader.append(closeButton);
        toast.append(toastHeader);
        toast.append(toastBody);

        // Position the toast container in the upper right corner
        toastContainer.css("position", "fixed");
        toastContainer.css("top", "1rem");
        toastContainer.css("right", "1rem");

        // Set flexbox properties to align the toasts vertically
        toastContainer.css("display", "flex");
        toastContainer.css("flex-direction", "column-reverse");
        toastContainer.css("align-items", "flex-end");

        // Add a margin between stacked toasts
        toastContainer.children().each(function () {
            $(this).css("margin-bottom", "10px");
        });

        toastContainer.append(toast);

        // Show the toast
        toast.toast("show");
    }

});