using Blood_Donation_Management_System.EF;
using Blood_Donation_Management_System.EF.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Blood_Donation_Management_System.Controllers
{
    public class DonorController : Controller
    {
        BloodDonationManagementSystemContext db;

        public DonorController(BloodDonationManagementSystemContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View(db.Donors.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Donor donor)
        {
            if (ModelState.IsValid)
            {
                db.Donors.Add(donor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donor);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var donor = db.Donors.Find(id);

            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        [HttpPost]
        public IActionResult Edit(Donor formObj)
        {
            var exObj = db.Donors.Find(formObj.DonorId);

            exObj.FullName = formObj.FullName;
            exObj.BloodGroup = formObj.BloodGroup;
            exObj.ContactNo = formObj.ContactNo;
            exObj.City = formObj.City;
            exObj.LastDonationDate = formObj.LastDonationDate;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var donor = db.Donors.Find(id);

            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        [HttpPost]
        public IActionResult Delete(Donor formObj, string Dcsn)
        {
            if (Dcsn == "Yes")
            {
                var data = db.Donors.Find(formObj.DonorId);

                db.Donors.Remove(data);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var data = db.Donors.Find(id);

            return View(data);
        }

        [HttpGet]
        public IActionResult BloodGroup(string bg)
        {
            var data = from d in db.Donors
                       where string.IsNullOrEmpty(bg) || d.BloodGroup == bg
                       select d;

            ViewBag.BloodGroup = bg;

            return View(data.ToList());
        }

        [HttpGet]
        public IActionResult RecentDonors()
        {
            var data = (from d in db.Donors
                        orderby d.LastDonationDate descending
                        select d).ToList();

            return View(data);
        }





    }
}
