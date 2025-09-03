window._charts = window._charts || {};

window.createLineChart = (elementId, labels, data, title) => {
    const ctx = document.getElementById(elementId)?.getContext('2d');
    if (!ctx) return;
    if (window._charts[elementId]) {
        window._charts[elementId].destroy();
    }
    window._charts[elementId] = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: title ?? 'Series',
                data: data,
                fill: false,
                tension: 0.25,
                pointRadius: 3,
                borderWidth: 2
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: true, position: 'top' },
                tooltip: { mode: 'index', intersect: false }
            },
            interaction: { mode: 'nearest', intersect: false },
            scales: {
                x: { display: true },
                y: { display: true, beginAtZero: true }
            }
        }
    });
};

window.createDonutChart = (elementId, labels, data, title) => {
    const ctx = document.getElementById(elementId)?.getContext('2d');
    if (!ctx) return;
    if (window._charts[elementId]) {
        window._charts[elementId].destroy();
    }
    window._charts[elementId] = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                label: title ?? 'Breakdown',
                data: data,
                backgroundColor: [
                    '#4CAF50', '#2196F3', '#FFC107', '#E91E63', '#9C27B0', '#FF5722', '#607D8B'
                ],
                hoverOffset: 6
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { position: 'bottom' }
            }
        }
    });
};
