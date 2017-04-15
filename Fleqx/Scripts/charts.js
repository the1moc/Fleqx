$(function ()
{
    // On click of a section button, load the task chart.
    $(document).on("click", ".graphButton", function(event)
    {
        $.ajax({
            url: $(this).data("url"),
            success: function (data)
            {
                createGraph(data);
            }
        });
    });
});

function createGraph(d)
{
    if (this.myChart != null) {
        this.myChart.destroy();
    }

    var parsedData = JSON.parse(d);
    $(".graph-title").text(parsedData.title);

    switch(parsedData.type)
    {
        case "bar":
            createBarChart(parsedData);
            break;
        case "doughnut":
            createDoughnutChart(parsedData);
            break;
    }
}

function createBarChart(parsedData)
{
    if (parsedData.data.datasets[0].backgroundColor.length == 1)
    {
        parsedData.data.datasets[0].backgroundColor = parsedData.data.datasets[0].backgroundColor[0];
    }
    if (parsedData.data.datasets[0].borderColor.length == 1) {
        parsedData.data.datasets[0].borderColor = parsedData.data.datasets[0].borderColor[0];
    }

    $("#chart").css("max-height", "800px");
    $("#chart").css("max-width", "");
    var ctx = document.getElementById('chart').getContext('2d');
    this.myChart = new Chart(ctx, {
        type: parsedData.type,
        data: parsedData.data,
        options: {
            legend: {
                display: false
            },
            scales: {
                xAxes: [{
                    ticks: {
                        fontSize: 20
                    }
                }],
                yAxes: [{
                    ticks: {
                        fontSize: 20,
                        beginAtZero: true,
                        userCallback: function (label, index, labels)
                        {
                            if (Math.floor(label) === label) {
                                return label;
                            }
                        }
                    }
                }]
            }
        }
    });
}

function createDoughnutChart(parsedData)
{
    $("#chart").css("max-height", "800px");
    $("#chart").css("max-width", "800px");
    var ctx = document.getElementById('chart').getContext('2d');
    this.myChart = new Chart(ctx, {
        type: parsedData.type,
        data: parsedData.data,
        options: {
            animation: {
                animateScale: true
            }
        }
    });
}
