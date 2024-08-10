var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IGlobalStateService, GlobalStateService>();
builder.Services.AddTransient<IRequestProcessingService, UpperCaseProcessingService>();
builder.Services.AddTransient<IRequestProcessingService, LowerCaseProcessingService>();
var app = builder.Build();

app.Map("/", () =>
{
    Console.WriteLine("Application has started.");

    var services = app.Services.GetRequiredService<IEnumerable<IRequestProcessingService>>();

    var data = "Hello World";

    Console.WriteLine($"Processing data: {data}");

    var results = services.Select(service => service.ProcessRequest(data)).ToList();

    Console.WriteLine("Processed results:");
    results.ForEach(result => Console.WriteLine(result));
});

app.Run();


public interface IGlobalStateService
{
    int GetState();
    void SetState(int state);
}
public class GlobalStateService : IGlobalStateService
{
    private int _state;

    public int GetState()
    {
        return _state;
    }

    public void SetState(int state)
    {
        _state = state;
    }
}
public interface IRequestProcessingService
{
    string ProcessRequest(string data);
}
/*
public class RequestProcessingService : IRequestProcessingService
{
    public string ProcessRequest(string data)
    {
        return $"Processed data: {data.ToUpper()}";
    }
}*/
public class UpperCaseProcessingService : IRequestProcessingService
{
    public string ProcessRequest(string data)
    {
        return $"Uppercase: {data.ToUpper()}";
    }
}
public class LowerCaseProcessingService : IRequestProcessingService
{
    public string ProcessRequest(string data)
    {
        return $"Lowercase: {data.ToLower()}";
    }
}
