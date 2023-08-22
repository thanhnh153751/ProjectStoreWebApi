// Sample data for revenue per day (replace with your actual data)
//const revenueData = [1000, 1200, 800, 1500, 1300, 1100, 1400, 1600, 1800, 1700, 1900, 2000, 2200, 2100, 2300, 2400, 2600, 2500, 2700, 2800, 2900, 3000, 3200, 3100, 3300, 3400, 3600, 3500, 3700, 3800, 4000];
let revenueData =[];

// Dates for the x-axis (replace with your actual dates)
let dates = Array.from({ length: 31 }, (_, i) => `2023-07-${i + 1}`);

// Line Chart
const lineChartCanvas = document.getElementById("lineChart").getContext("2d");
const lineChart = new Chart(lineChartCanvas, {
    type: "line",
    data: {
        labels: dates,
        datasets: [{
            label: "Total Revenue",
            data: revenueData,
            borderColor: "rgba(75, 192, 192, 1)",
            borderWidth: 2,
            fill: false,
        }],
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
            x: {
                display: true,
                title: {
                    display: true,
                    text: "Date",
                },
            },
            y: {
                display: true,
                title: {
                    display: true,
                    text: "Total Revenue",
                },
            },
        },
    },
});
//------------------------------------------------------------------------------
function populateYearSelect() {
    $.ajax({
        url: "https://localhost:7012/api/Orders/GetAllYearInOrderDate",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var yearSelect = $("#yearSelect");
            yearSelect.empty();
            $.each(data, function (index, year) {
                yearSelect.append($("<option></option>").attr("value", year).text(year));
            });
        },
        error: function (error) {
            console.log("Error while fetching years:", error);
        }
    });
}

// Function to handle the chart generation based on selected Year and Month
function generateChart(year, month) {
    console.log(year + " " + month);
    $.ajax({
        url: "https://localhost:7012/api/Orders/GetRevenueForMonth/" + month + "/" + year,
        type: "get",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            // Update revenueData with the fetched data
            revenueData = data;
           
            // Get the number of days in the selected month and year
            const numberOfDays = new Date(year, month, 0).getDate();

            // Generate new dates for the x-axis based on the selected month and year
            dates = Array.from({ length: numberOfDays }, (_, i) => `${year}-${month.toString().padStart(2, '0')}-${(i + 1).toString().padStart(2, '0')}`);

            // Update the chart data and labels
            lineChart.data.labels = dates;
            lineChart.data.datasets[0].data = revenueData;
            lineChart.update();
        },
        error: function (error) {
            console.log("Error while fetching revenue data:", error);
        }
    });
}



// Call the populateYearSelect function to fill the Year select options
populateYearSelect();

// Event listener to handle changes in the Year and Month selects
$("#yearSelect, #monthSelect").on("change", function () {
    var selectedYear = parseInt($("#yearSelect").val());
    var selectedMonth = parseInt($("#monthSelect").val());
    generateChart(selectedYear, selectedMonth);
});