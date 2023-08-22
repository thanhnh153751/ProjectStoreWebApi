$(document).ready(function () {
    var customerId = JSON.parse($("#customerId").html());
    var token = JSON.parse($("#token").html());
    function fetchData() {
        $.ajax({
            url: 'https://localhost:7012/api/Customers/GetMember/' + customerId,
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                populateData(data);
            },
            error: function (error) {
                console.log('Error fetching data:', error);
            }
        });
    }

    // Function to populate the HTML elements with the data
    function populateData(data) {
        // Update the respective elements with the returned data
        $('#contactName').text(data.contactName || ''); // If data.contactName is null, display an empty string
        $('#username').text(data.username || '');
        $('#phone').text(data.phone || '');
        $('#address').text(data.address || '');
    }

    // Call the fetchData function to populate the HTML elements on page load
    fetchData();

    $(document).on("click", ".updateBtn", function () {
        var productId = customerId;

        // Fetch product details and populate the modal
        fetchProductDetailsUpdate(productId);

        // Show the modal
        $("#updateModal").modal("show");
    });

    function fetchProductDetailsUpdate(productId) {
        $.ajax({
            url: "https://localhost:7012/api/Customers/GetMember/" + productId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                
                // Assign the values to the input fields
                $("#contactName-up").val(result.contactName);
                $("#username-up").val(result.username);
                $("#phone-up").val(result.phone);
                $("#address-up").val(result.address);

            },
            error: function (xhr, status, error) {
                console.log("loi");
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
    // Handle submit button edit click
    $("#submitBtn-up").click(function () {
        
        var formData = $("#contactForm-up").serializeArray();

        // Create the model object from the form data
        var model = createModelFromFormData(formData);
        model.customerId = customerId;
        model.password = "";
        model.image = "";
        model.roleId = null;
        model.status = null;
        console.log(model);
        // Send the form data to the API
        $.ajax({
            url: "https://localhost:7012/api/Customers/UpdateMemberInfor/" + customerId,
            type: "put",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(model),
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                //console.log(result);
                fetchData();
                // Close the modal
                $("#updateModal").modal("hide");
                // Destroy the existing DataTable instance
                
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