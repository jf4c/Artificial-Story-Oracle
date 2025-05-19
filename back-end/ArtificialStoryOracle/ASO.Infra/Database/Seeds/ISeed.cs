namespace ASO.Infra.Database.Seeds;

public interface ISeed
{ 
    static abstract void Seed(AppDbContext context);
}