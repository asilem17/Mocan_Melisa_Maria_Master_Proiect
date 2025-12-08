using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Mocan_Melisa_Grpc;

public class BreedsGrpcController : Controller
{
    private readonly GrpcChannel _channel;

    public BreedsGrpcController()
    {
        _channel = GrpcChannel.ForAddress("https://localhost:7201");
    }

    public IActionResult Index()
    {
        var client = new BreedService.BreedServiceClient(_channel);
        var breeds = client.GetAll(new Empty());
        return View(breeds);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Breed breed)
    {
        if (!ModelState.IsValid) return View(breed);

        var client = new BreedService.BreedServiceClient(_channel);
        client.Insert(breed);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var client = new BreedService.BreedServiceClient(_channel);
        var breed = client.Get(new BreedId { Id = id });
        return View(breed);
    }

    [HttpPost]
    public IActionResult Edit(Breed breed)
    {
        var client = new BreedService.BreedServiceClient(_channel);
        client.Update(breed);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var client = new BreedService.BreedServiceClient(_channel);
        var breed = client.Get(new BreedId { Id = id });
        return View(breed);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var client = new BreedService.BreedServiceClient(_channel);
        client.Delete(new BreedId { Id = id });
        return RedirectToAction(nameof(Index));
    }
}
