using DogGo.Repositories;
using DogGo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DogGo.Interfaces;
using DogWalker.Repositories;
using DogGo.Models.ViewModels;

namespace DogGo.Controllers
{
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo; 

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public OwnerController(IOwnerRepository ownerRepository, IDogRepository dogRepository,
        IWalkerRepository walkerRepository, INeighborhoodRepository neighborhoodRepository)

        {
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
            _walkerRepo = walkerRepository;
            _neighborhoodRepo = neighborhoodRepository;
        }
        // GET: OwnerController
        public ActionResult Index()
        {
            List<Owner> owners = _ownerRepo.GetAllOwners();

            return View(owners);

        }

        /// GET: Owners/Details/5
        public ActionResult Details(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);
            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(owner.Id);
            List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(owner.NeighborhoodId);

            ProfileViewModel vm = new ProfileViewModel()
            {
                Owner = owner,
                Dogs = dogs,
                Walkers = walkers
            };

            return View(vm);
        }

        // GET: Owners/Create
        public ActionResult Create()
        {
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                Owner = new Owner(),
                Neighborhoods = neighborhoods
            };

            return View(vm);
        }

        // POST: OwnerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Owner owner)
        {
            try
            {
                _ownerRepo.AddOwner(owner);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(owner);
            }
        }

        //// GET: Owners/Create
        //public ActionResult Create()
        //{
        //    List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

        //    OwnerFormViewModel vm = new OwnerFormViewModel()
        //    {
        //        Owner = new Owner(),
        //        Neighborhoods = neighborhoods
        //    };

        //    return View(vm);
        //}
        // GET: Owners/Edit/5
        public ActionResult Edit(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);
            List<Neighborhood> neighborhoods = _neighborhoodRepo.GetAll();

            OwnerFormViewModel vm = new OwnerFormViewModel()
            {
                Owner = new Owner(),
                Neighborhoods = neighborhoods
            };

            if (owner == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // POST: Owners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Owner owner)
        {
            try
            {
                _ownerRepo.UpdateOwner(owner);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(owner);
            }
        }

        // GET: Owners/Delete/5
        public ActionResult Delete(int id)
        {
            Owner owner = _ownerRepo.GetOwnerById(id);

            return View(owner);
        }

        // POST: OwnerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Owner owner)
        {
            try
            {
                _ownerRepo.DeleteOwner(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(owner);
            }
        }

    }
}
