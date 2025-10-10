export function renderChartJs(data, modalId, canvasId) {
	if (!data) return;

	const modal = new window.bootstrap.Modal(document.getElementById(modalId));
	modal.show();

	setTimeout(() => {
		const ctx = document.getElementById(canvasId).getContext('2d');

		const labels = data.map((d) => d.nombreJugador);
		const config = {
			type: 'bar',
			data: {
				labels: labels,
				datasets: [
					{
						label: 'Madera',
						data: data.map((d) => d.madera),
						backgroundColor: 'rgba(153, 102, 255, 0.6)',
					},
					{
						label: 'Piedra',
						data: data.map((d) => d.piedra),
						backgroundColor: 'rgba(255, 159, 64, 0.6)',
					},
					{
						label: 'Comida',
						data: data.map((d) => d.comida),
						backgroundColor: 'rgba(75, 192, 192, 0.6)',
					},
				],
			},
			options: {
				responsive: true,
				scales: {
					x: {
						ticks: { color: '#ccc' },
						grid: { color: 'rgba(255,255,255,0.1)' },
					},
					y: {
						beginAtZero: true,
						ticks: { color: '#ccc' },
						grid: { color: 'rgba(255,255,255,0.1)' },
					},
				},
				plugins: {
					title: {
						display: true,
						text: 'Recursos recolectados por jugador',
						color: '#ccc',
					},
					legend: {
						labels: { color: '#ccc' },
					},
				},
			},
		};

		window.recursosChart = new window.Chart(ctx, config);
	}, 300);
}
