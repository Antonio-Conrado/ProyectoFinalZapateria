using Blazored.SessionStorage;
using BlazorLogin.Client.Extensiones;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using TiendaZapateria.Client;
using TiendaZapateria.Client.Services.Contrato;
using TiendaZapateria.Client.Services.Implementacion;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5155/") });



builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
builder.Services.AddAuthorizationCore();



builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IVistaService, VistaService>();
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ITallaService, TallaService>();
builder.Services.AddScoped<IMarcaService, MarcaService>();




builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
