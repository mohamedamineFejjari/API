namespace SmartOllas.Irrigation.API.Plante.AddPlante
{
    public static class PlanteEndPoint
    {
        public static void MapAddPlanteEndPoint(this WebApplication app)
        {
            app.MapPost("/plante", (AddPlanteViewModel plante) =>
            {
                return Results.Ok();
            });
        }
    }
}