using DogGo.Interfaces;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalkRepository _walkRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;
        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository,
            IWalkRepository walkRepository,
            IOwnerRepository ownerRepository,
            IDogRepository dogRepository,
            INeighborhoodRepository neighborhoodRepository)
        {
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
            _neighborhoodRepo = neighborhoodRepository;
        }
        // controller to get the signed in user id. We will use this a lot.
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(id))
            {
                return 0;
            }
            else
            { return int.Parse(id); }
        }

        // GET: Walkers
        public ActionResult Index(int id)
        {
            int userId = GetCurrentUserId();//current user id (owner)
            Owner owner = _ownerRepo.GetOwnerById(userId); // get the owner by Id. will be used to match with walker through neighborhoodID
            List<Walker> walkers = _walkerRepo.GetAllWalkers();//all walkers


            if (userId != 0)//if there is a user id. 0 would mean no id? Right?
            {

                List<Walker> neighborhoodWalkers = walkers.Where(walker => walker.NeighborhoodId == owner.NeighborhoodId).ToList();
                return View(neighborhoodWalkers);//Make a list called neighborhoodWalkers. it equals the walkers where walker neighborhoodId equals the owner neighborhoodID. return the list that meet the parameters. 
            }
            
            else
            {
                return View(walkers);
            }

        }


        // GET: Walkers/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            if (walker == null)
            {
                return NotFound();
            }

            List<Walk> walks = _walkRepo.GetWalkByWalkerId(id);


            WalkerProfileViewModel vm = new WalkerProfileViewModel
            {
                Walker = walker,
                Walks = walks
            };

            return View(vm);
        }


        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
