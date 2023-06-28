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
            int userId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(userId);

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
        //I want to edit the get because I only want to received the data if the user is authorized/signed in
        public ActionResult Edit(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);//I need the dog by id for the ownerId
            int userId = GetCurrentUserId(); //get the current user id 

            if (dog == null) //if there is no dog
            {
                return NotFound();//404 error
            }

            else if (dog.OwnerId != userId) //if my owner id does not match the current userID return not found.
            {
                return NotFound();
            }
            else 
            {
                
                return View(dog); //else return my dog index
            }
            
        }

        // POST: dogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            try
            {    
            
                _dogRepo.UpdateDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: dogs/Delete/5

        public ActionResult Delete(int id) //again I am looking at the get to see if I can access the data. Similar if/else statement to edit. 
        {
            Dog dog = _dogRepo.GetDogById(id);
            int userId = GetCurrentUserId(); 

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
