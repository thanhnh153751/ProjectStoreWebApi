$(document).ready(function () {
    
    var token = JSON.parse($("#token").html());
    ShowAllCategorys();
    function ShowAllCategorys() {
        $("table tbody").html("");
        $.ajax({
            url: "https://localhost:7012/odata/Categorys",
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                // Populate the table with data
                //console.log(token);
                $.each(result.value, function (index, value) {
                    $("tbody").append($("<tr>"));
                    appendElement = $("tbody tr").last();
                    appendElement.append($("<td>").html(value.categoryId));
                    appendElement.append($("<td>").html(value.categoryName));
                    appendElement.append($("<td>").html(value.description));
                    appendElement.append($("<td>").html(value.status));

                    appendElement.append($("<td>").html("<button class=\"btn btn-warning updateBtn\" data-product-id=\"" + value.categoryId + "\">Edit</ button>"
                        + "<button class=\"btn ml-2 btn-danger deleteBtn\" data-product-id=\"" + value.categoryId + "\">Delete</button>"));
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
            url: "https://localhost:7012/odata/Categorys?$filter=categoryId eq " + productId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                // Populate the modal fields with the fetched data
                var product = result.value[0];

                // Populate the modal fields with the fetched data
                $("#categoryId-de").val(product.categoryId);
                $("#categoryName-de").val(product.categoryName);
                $("#description-de").val(product.description);
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
            url: "https://localhost:7012/odata/Categorys?$filter=categoryId eq " + productId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                // Populate the modal fields with the fetched data
                var product = result.value[0];
                // Assign the values to the input fields
                $("#categoryId-up").val(product.categoryId);
                $("#categoryName-up").val(product.categoryName);
                $("#description-up").val(product.description);

            },
            error: function (xhr, status, error) {
                console.log("loi");
                console.log(xhr);
            }
        });
    }

    // Handle submit button delete click
    $("#submitBtn-de").click(function () {
        var productId = $("#categoryId-de").val();
        //console.log(productId +"-----");

        // Send the form data to the API
        $.ajax({
            url: "https://localhost:7012/api/Categorys/deleteCategory/" + productId,
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

    // Handle submit button edit click
    $("#submitBtn-up").click(function () {
        var productId = $("#categoryId-up").val();
        var formData = $("#contactForm-up").serializeArray();

        // Create the model object from the form data
        var model = createModelFromFormData(formData);
        // Send the form data to the API
        $.ajax({
            url: "https://localhost:7012/api/Categorys/putCategory/" + productId,
            type: "put",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(model),
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                //console.log(result);

                // Close the modal
                $("#updateModal").modal("hide");
                // Destroy the existing DataTable instance
                $('#productTable').DataTable().destroy();
                ShowAllCategorys();
                showSuccessToast("Successfully Update");
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