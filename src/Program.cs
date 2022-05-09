var builder = WebApplication.CreateBuilder();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hit the /albums endpoint to retrieve a list of albums!");
});

app.MapGet("/albums", () =>
{
    return Album.GetAll();
})
.WithName("GetAlbums");

app.Run();

record Album(int Id, string Title, string Artist, double Price, string Image_URL)
{
     public static List<Album> GetAll(){
         var albums = new List<Album>(){
            new Album(1, "Sgt Peppers Lonely Hearts Club Band", "The Beatles", 10.99, "https://www.listchallenges.com/f/items/f3b05a20-31ae-4fdf-aebd-d1515af54eea.jpg"),
            new Album(2, "Pet Sounds", "The Beach Boys", 13.99, "https://www.listchallenges.com/f/items/fdef1440-e979-455a-90a7-14e77fac79af.jpg"),
            new Album(3, "The Beatles: Revolver", "The Beatles", 13.99, "https://www.listchallenges.com/f/items/19ff786d-d7a4-4fdc-bee2-59db8b33370d.jpg"),
            new Album(4, "Highway 61 Revisited", "Bob Dylan", 12.99,"https://www.listchallenges.com/f/items/428cf087-6c24-45ad-bf8c-bd3c57ddf444.jpg"),
            new Album(5, "Rubber Soul", "The Beatles", 12.99, "https://www.listchallenges.com/f/items/ebc794ef-1491-4672-a007-0081edc1a8ae.jpg"),
            new Album(6, "What's Going On", "Marvin Gaye", 14.99, "https://www.listchallenges.com/f/items/e5250d6c-1c15-4617-a943-b27e87e21704.jpg")
         };

        return albums; 
     }
}