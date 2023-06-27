using DogGo.Interfaces;
using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DogGo.Controllers
{
    public class DogController : Controller
    {
        private readonly IDogRepository _dogRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public DogController(IDogRepository dogRepository)
        {
            _dogRepo = dogRepository;
        }

        // controller to get the signed in user id. We will use this a lot.
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        // GET: Dogs
        [Authorize]
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);

            return View(dogs);
        }

        // GET: dogs/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }
        // GET: dogs/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: dogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: dogs/Edit/5

        public ActionResult Edit(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            int userId = GetCurrentUserId(); //get the current user id name it userId

            if (dog == null)
            {
                return NotFound();
            }

            else if (dog.OwnerId != userId) //if my owner id does not match the current userID return not found.
            {
                return NotFound();
            }
            else 
            {
                
                return View(dog); 
            }
            
        }

        // POST: dogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            try
            {    //match dogs OwnerId to the current user's Id
            
                _dogRepo.UpdateDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: dogs/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            int userId = GetCurrentUserId(); //get the current user id, name it userId

            if (dog == null)
            {
                return NotFound();
            }

            else if (dog.OwnerId != userId) //if my owner id does not match the current userID return not found.
            {
                return NotFound();
            }
            else
            {

                return View(dog);
            }

        
    }

        // POST: dogs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            { 
                _dogRepo.DeleteDog(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }
    }
}
