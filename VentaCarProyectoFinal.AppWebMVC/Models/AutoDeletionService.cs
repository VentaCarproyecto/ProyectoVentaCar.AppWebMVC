using Microsoft.EntityFrameworkCore;

namespace VentaCarProyectoFinal.AppWebMVC.Models
{
    public class AutoDeletionService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public AutoDeletionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<VentacarProyectContext>(); // Reemplaza TuDbContext
                    await DeleteExpiredAutos(context);
                }

                await Task.Delay(TimeSpan.FromHours(00.01), stoppingToken); // Ejecuta cada hora (ajusta según sea necesario)
            }
        }

        private async Task DeleteExpiredAutos(VentacarProyectContext context)
        {
            var expiredAutos = await context.Autos
                .Where(a => a.FechaRp.HasValue && a.FechaRp <= DateTime.Now)
                .ToListAsync();

            if (expiredAutos.Any())
            {
                context.Autos.RemoveRange(expiredAutos);
                await context.SaveChangesAsync();
            }
        }
    }
}
