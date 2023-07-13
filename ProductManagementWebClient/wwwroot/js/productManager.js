$(document).ready(function () {
    
    var token = JSON.parse($("#token").html());
    ShowAllProducts();
    function ShowAllProducts() {
        $("table tbody").html("");
        $.ajax({
            url: "https://localhost:7012/odata/Products",
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer" + token
            },
            success: function (result, status, xhr) {
                // Populate the table with data
                $.each(result.value, function (index, value) {
                    $("tbody").append($("<tr>"));
                    appendElement = $("tbody tr").last();
                    appendElement.append($("<td>").html(value.productId));
                    appendElement.append($("<td>").html(value.categoryName));
                    appendElement.append($("<td>").html(value.productName));
                    appendElement.append($("<td>").html(value.unitPrice));
                    appendElement.append($("<td>").html(value.unitsInStock));
                    appendElement.append($("<td>").html(value.unitsOnOrder));
                    appendElement.append($("<td>").html("<img style=\"width: 80px;height: 60px;\" src=\"/" + value.image + "\"/>"));
                    appendElement.append($("<td>").html(value.description));
                    appendElement.append($("<td>").html(value.status));

                    appendElement.append($("<td>").html("<button class=\"btn btn-warning updateBtn\" data-product-id=\"" + value.productId + "\">Edit</ button>"
                        + "<button class=\"btn ml-2 btn-danger deleteBtn\" data-product-id=\"" + value.productId + "\">Delete</button>"));
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



    


    ShowAllCategorys();
    function ShowAllCategorys() {
        $.ajax({
            url: "https://localhost:7012/odata/Categorys?$select=categoryId,categoryName",
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (response, status, xhr) {

                // Process the response and update the datalist options
                var datalist = $('#datalistOptions');
                datalist.empty(); // Clear existing options
                //for modal-up
                var datalistUp = $('#datalistOptions-up');
                datalistUp.empty();

                //var defau = $('<option>').attr('value', '').text('');
                //datalist.append(defau);
                // Iterate through the response and create new options
                response.value.forEach(function (category) {
                    var option = $('<option>').attr('value', category.categoryId).text(category.categoryName);
                    datalist.append(option);

                    var optionUp = $('<option>').attr('value', category.categoryId).text(category.categoryName);
                    datalistUp.append(optionUp);
                });
                // Set the first option as the default selected option
                var firstOption = $('#datalistOptions option:first');
                firstOption.prop('selected', true);
                firstOption.attr('selected', true);

                //var firstOptionUp = $('#datalistOptions-up option:first');
                //firstOptionUp.prop('selected', true);
                //firstOptionUp.attr('selected', true); 

                // Set the first option name as the default selection
                var firstOptionName = response.value[0].categoryName;
                datalist.selectpicker('val', firstOptionName);

                var firstOptionNameUp = response.value[0].categoryName;
                datalistUp.selectpicker('val', firstOptionNameUp);

                // Initialize the selectpicker after adding options
                //$('.selectpicker').selectpicker('refresh');


                
                response.value.forEach(function (categoryUp) {
                    
                });
                $('.selectpicker').selectpicker('refresh');
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
        var formData = $("#contactForm").serializeArray();

        var imageFile = $("#file-input")[0].files[0];
        var imageName = imageFile ? ('images/'+imageFile.name) : '';
        formData.push({ name: "image", value: imageName });
        
        // Create the model object from the form data
        var model = createModelFromFormData(formData);
        // Send the form data to the API
        $.ajax({
            url: "https://localhost:7012/api/Products/postProduct",
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(model),
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                //console.log(result);

                // Close the modal
                $("#exampleModal").modal("hide");

                // Destroy the existing DataTable instance
                $('#productTable').DataTable().destroy();
                ShowAllProducts();
                // Clear the form fields
                //$("#contactForm")[0].reset();

                // Save the image file
                saveImageFile();
                showSuccessToast("Successfully Created");
            },
            error: function (xhr, status, error) {
                // Handle the error response
                console.log("loi");
                console.log(xhr);
            }
        });
    });

    function saveImageFile() {
        var imageFile = $("#file-input")[0].files[0];

        var formData = new FormData();
        formData.append("imageFile", imageFile);

        $.ajax({
            url: "/Product/UploadImage",
            type: "post",
            contentType: false,
            processData: false,
            data: formData,
            success: function (result, status, xhr) {
                console.log("Image saved successfully:", result);
            },
            error: function (xhr, status, error) {
                console.log("Error saving image:", error);
            }
        });
    }

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
            url: "https://localhost:7012/odata/Products?$filter=productId eq " + productId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                // Populate the modal fields with the fetched data
                var product = result.value[0];

                // Populate the modal fields with the fetched data
                $("#productId-de").val(product.productId);
                $("#productName-de").val(product.productName);
                $("#categoryName-de").val(product.categoryName);
                $("#unitsInStock-de").val(product.unitsInStock);
                $("#unitPrice-de").val(product.unitPrice);
                // Assign the image source and description
                $("#imagede").attr("src",'/'+ product.image);
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
            url: "https://localhost:7012/api/Products/GetProductById/" + productId,
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                // Populate the modal fields with the fetched data
                var product = result;
                // Assign the values to the input fields
                $("#productId-up").val(product.productId);
                $("#productName-up").val(product.productName);
                $("#unitPrice-up").val(product.unitPrice);
                $("#unitsInStock-up").val(product.unitsInStock);
                $("#unitsOnOrder-up").val(product.unitsOnOrder);
                $("#description-up").val(product.description);
                $("#datalistOptions-up").selectpicker('val', product.categoryId);

                // Display the uploaded image if product.image exists
                if (product.image) {
                    var imgCont = document.getElementById("cont-up");
                    imgCont.innerHTML = "";
                    var label = document.getElementById('thumbnail-image-label-up');
                    label.style = "display: none;";

                    for (let i = 0; i < 1; i++) {
                        var divElm = document.createElement('div');
                        divElm.id = "rowdiv-up" + i;
                        var spanElm = document.createElement('span');
                        var image = document.createElement('img');
                        image.src = '/' + product.image;
                        image.id = "output-up" + i;
                        image.width = "200";

                        spanElm.appendChild(image);
                        var deleteImgUp = document.createElement('button');
                        deleteImgUp.innerHTML = "x";
                        deleteImgUp.style = "cursor: pointer";

                        deleteImgUp.onclick = function () {
                            this.parentNode.remove();
                            label.style = "";
                            document.getElementById('file-input-up').value = null;
                        };

                        divElm.appendChild(spanElm);
                        divElm.appendChild(deleteImgUp);
                        imgCont.appendChild(divElm);
                    }
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
        var productId = $("#productId-de").val();
        //console.log(productId +"-----");

        // Send the form data to the API
        $.ajax({
            url: "https://localhost:7012/api/Products/deleteProduct/" + productId,
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
                ShowAllProducts();
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
        var productId = $("#productId-up").val();
        var formData = $("#contactForm-up").serializeArray();

        var imageFile = $("#file-input-up")[0].files[0];
        var imageName = imageFile ? ('images/' + imageFile.name) : '';
        formData.push({ name: "image", value: imageName });
        // Create the model object from the form data
        var model = createModelFromFormData(formData);
        console.log(formData);
        // Send the form data to the API
        $.ajax({
            url: "https://localhost:7012/api/Products/putProduct/" + productId,
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
                ShowAllProducts();
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



    // Add an event handler for the search name button
    $("#searchBookNameButton").on("click", function () {
        var searchQuery = $("#searchBookName").val().trim();
        //console.log(searchQuery);

        SearchBookName(searchQuery);
    });

    // Add an event handler for the search price button
    $("#searchBookPriceButton").on("click", function () {
        var searchQuery = $("#searchBookPrice").val().trim();
        SearchBookPrice(searchQuery);
    });


    function SearchBookName(query) {
        $("table tbody").html("");
        $.ajax({
            url: "https://localhost:7012/odata/Books?$filter=contains(tolower(title),'" + query + "')",
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                // Process the search results
                if (result.value.length > 0) {
                    $.each(result.value, function (index, value) {
                        $("tbody").append($("<tr>"));
                        appendElement = $("tbody tr").last();
                        appendElement.append($("<td>").html(value.book_id));
                        appendElement.append($("<td>").html(value.title));
                        appendElement.append($("<td>").html(value.type));
                        appendElement.append($("<td>").html(value.pub_id));
                        appendElement.append($("<td>").html(value.price));
                        appendElement.append($("<td>").html(value.advance));
                        appendElement.append($("<td>").html(value.royalty));
                        appendElement.append($("<td>").html(value.ytd_sales));
                        appendElement.append($("<td>").html(value.notes));
                        appendElement.append($("<td>").html(new Date(value.published_date).toLocaleDateString('en-GB')));
                        appendElement.append($("<td>").html("<a href=\"/Book/Edit/" + value.book_id + "\">Edit</a>|"
                            + "<a href=\"/Book/Delete/" + value.book_id + "\">Delete</a>"));
                    });
                } else {
                    // Handle case when no matching books are found
                    $("table tbody").html("<tr><td colspan='11'>No matching books found.</td></tr>");
                }
            },
            error: function (xhr, status, error) {
                console.log(xhr);
            }
        });
    }

    function SearchBookPrice(query) {
        $("table tbody").html("");
        $.ajax({
            url: "https://localhost:7012/odata/Books?$filter=price" + " eq " + query,
            type: "get",
            contentType: "application/json; charset=utf-8",
            headers: {
                "Authorization": "Bearer " + token
            },
            success: function (result, status, xhr) {
                // Process the search results
                if (result.value.length > 0) {
                    $.each(result.value, function (index, value) {
                        $("tbody").append($("<tr>"));
                        appendElement = $("tbody tr").last();
                        appendElement.append($("<td>").html(value.book_id));
                        appendElement.append($("<td>").html(value.title));
                        appendElement.append($("<td>").html(value.type));
                        appendElement.append($("<td>").html(value.pub_id));
                        appendElement.append($("<td>").html(value.price));
                        appendElement.append($("<td>").html(value.advance));
                        appendElement.append($("<td>").html(value.royalty));
                        appendElement.append($("<td>").html(value.ytd_sales));
                        appendElement.append($("<td>").html(value.notes));
                        appendElement.append($("<td>").html(new Date(value.published_date).toLocaleDateString('en-GB')));
                        appendElement.append($("<td>").html("<a href=\"/Book/Edit/" + value.book_id + "\">Edit</a>|"
                            + "<a href=\"/Book/Delete/" + value.book_id + "\">Delete</a>"));
                    });
                } else {
                    // Handle case when no matching books are found
                    $("table tbody").html("<tr><td colspan='11'>No matching books found.</td></tr>");
                }
            },
            error: function (xhr, status, error) {
                $("table tbody").html("<tr><td colspan='11'>No matching books found.</td></tr>");
                console.log(xhr);
            }
        });
    }

});