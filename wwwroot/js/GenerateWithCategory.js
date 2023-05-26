    <script>
        var catCost = @catCost;
        var allCost = @allCost;

        var ctx = document.getElementById('pieChart').getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ['Category Cost', 'General Cost'],
                datasets: [{
                    data: [catCost, allCost - catCost],
                    backgroundColor: [
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(192, 75, 75, 0.2)'
                    ],
                    borderColor: [
                        'rgba(75, 192, 192, 1)',
                        'rgba(192, 75, 75, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false
            }
        });
    </script>