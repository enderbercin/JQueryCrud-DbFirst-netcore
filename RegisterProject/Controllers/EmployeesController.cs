using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RegisterProject.Data;
using RegisterProject.Data.Domains;
using RegisterProject.Models;

namespace RegisterProject.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EmployeesController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Employees
                .Include(e => e.MediaLibrary)
                .Include(p => p.GeographicLibrary);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.MediaLibrary)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["MediaId"] = new SelectList(_context.MediaLibraries, "MediaId", "MediaId");
            IList<SelectListItem> countries = (from x in _context.GeographicLibraries.Where(x => x.UstId == 0).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Tanim,
                                                   Value = x.Id.ToString(),
                                               }).ToList();


            IList<SelectListItem> cities = (from x in _context.GeographicLibraries.Where(x => x.UstId != 0).ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.Tanim,
                                                Value = x.Id + "-" + x.UstId,
                                            }).ToList();


            ViewBag.Countries = countries;
            ViewBag.Cities = cities;
            return View();
        }


        // GET: Employees/AddOrEdit

        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            var employee = new Employee();

            IList<SelectListItem> countries = (from x in _context.GeographicLibraries.Where(x => x.UstId == 0).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Tanim,
                                                   Value = x.Id.ToString(),
                                               }).ToList();

            //gl.Where(x => x.Value == "0").ToList();

            IList<SelectListItem> cities = (from x in _context.GeographicLibraries.Where(x => x.UstId != 0).ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.Tanim,
                                                Value = x.Id + "-" + x.UstId,
                                            }).ToList();
            List<SelectListItem> schools = (from x in _context.Schools.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.SchoolName,
                                            }).ToList();

            List<School> schoolList = _context.Schools.ToList();

            ViewBag.Schools = schools;
            ViewBag.Countries = countries;
            ViewBag.Cities = cities;

            if (id != 0)
            {
                employee = await _context.Employees.FindAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                ViewData["MediaId"] = new SelectList(_context.MediaLibraries, "MediaId", "MediaId", employee.MediaId);
            }

            return View(employee);
        }
        //public async Task<SaveSchoolViewModel> SaveSchoolsData(SaveSchoolViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        GraduatedSchool graduatedSchool = new()
        //        {
        //            //GraduatedSchoolName = model.GratuatedSchoolName,
        //            SchoolId = model.SchoolSchoolId
        //        };

        //        EmployeeSchool employeeSchool = new EmployeeSchool
        //        {
        //            EmployeeId = model.EmployeeId,
        //            SchoolId = model.SchoolSchoolId,
        //            GraduatedSchoolId = model.GratuatedSchoolId

        //        };

        //        _context.Add(graduatedSchool);
        //        await _context.SaveChangesAsync();
        //        _context.Add(employeeSchool);
        //        _context.SaveChanges();
        //    }

        //    return model;
        //}

        public int SavePicture()
        {
            EmployeeViewModel model = new EmployeeViewModel();
            string picturePath = _hostEnvironment.WebRootPath;
            string extension = Path.GetExtension(model.MediaLibrary.ProfilePictureFile.FileName);
            string pictureName = Path.GetFileNameWithoutExtension(model.MediaLibrary.ProfilePictureFile.FileName);
            model.MediaLibrary.PictureName = pictureName + extension;

            string ReorganizedPictureName = model.MediaLibrary.ReorganizedPictureName = Guid.NewGuid() + pictureName + extension;
            string path = Path.Combine(picturePath + "/Media Library", model.MediaLibrary.ReorganizedPictureName + extension);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                model.MediaLibrary.ProfilePictureFile.CopyToAsync(fileStream);
            }


            MediaLibrary mediaLibrary = new MediaLibrary
            {
                ProfilePictureFile = model.ProfilePictureFile,
                PictureName = pictureName,
                ReorganizedPictureName = ReorganizedPictureName
            };
            _context.Add(mediaLibrary);
            _context.SaveChanges();

            var media = _context.MediaLibraries.ToList();

            return media[media.Count - 1].MediaId;
        }


        [HttpPost]
        public IActionResult SaveMedia(MediaLibraryViewModel model)
        {
            string picturePath = _hostEnvironment.WebRootPath;
            string extension = Path.GetExtension(model.ProfilePictureFile.FileName);
            string pictureName = Path.GetFileNameWithoutExtension(model.ProfilePictureFile.FileName);
            model.PictureName = pictureName + extension;

            string ReorganizedPictureName = model.ReorganizedPictureName = Guid.NewGuid() + pictureName + extension;
            string path = Path.Combine(picturePath + "/Media Library", model.ReorganizedPictureName + extension);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                model.ProfilePictureFile.CopyToAsync(fileStream);
            }


            MediaLibrary mediaLibrary = new MediaLibrary
            {
                ProfilePictureFile = model.ProfilePictureFile,
                PictureName = pictureName,
                ReorganizedPictureName = ReorganizedPictureName
            };
            _context.Add(mediaLibrary);
            _context.SaveChanges();

            var media = _context.MediaLibraries.ToList();
            int id = media[media.Count - 1].MediaId;

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Employees.ToList()) });

        }
        public async Task<MediaLibrary> WriteFile(IFormFile file)
        {

            var medya = new MediaLibrary();
            string fileName;
            var guid = Guid.NewGuid().ToString("N");

            if (file != null)
            {
                var wwwRootPath = _hostEnvironment.WebRootPath;
                fileName = guid + (file.FileName).Replace(" ", "_");
                var pathBuilt = Path.Combine(Environment.CurrentDirectory, wwwRootPath + "/Media Library");

                medya.PictureName = file.FileName;
                medya.ReorganizedPictureName = fileName;

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), wwwRootPath + "/Media Library/", fileName);


                await using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);

            }
            _context.Add(medya);
            _context.SaveChanges();
            return medya;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(EmployeeViewModel employee)
        {

            string[] a = employee.CityId.Split("-");
            Console.WriteLine(a[0]);
            employee.GeographicLibraryId = int.Parse(employee.CityId.Split("-")[0]);

            List<SelectListItem> values = (from x in _context.GeographicLibraries.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.Tanim,
                                               Value = (x.UstId == 0).ToString()
                                           }).ToList();
            ViewBag.v1 = values;


            var media = _context.MediaLibraries.ToList();

          

            EmployeeSchool employeeSchool1 = new EmployeeSchool()
            {
                EmployeeId = employee.Id,
                GraduatedSchoolId=employee.SGratuatedSchoolId,///******
            };
            //_context.Add(employeeSchool1);
            //_context.SaveChanges();

            employee.MediaId = media[media.Count - 1].MediaId;
            Employee employee1 = new Employee
            {
                Name = employee.Name,
                Surname = employee.Surname,
                Description = employee.Description,
                BirthDay = employee.BirthDay,
                GeographicLibraryId = employee.GeographicLibraryId,
                MediaId = employee.MediaId,
               
            };

            if (employee.Id == 0)
            {

                _context.Add(employee1);
                await _context.SaveChangesAsync();
            }
            else
            {
                try
                {
                    _context.Update(employee1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }
            ViewData["MediaId"] = new SelectList(_context.MediaLibraries, "MediaId", "MediaId", employee.MediaId);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.MediaLibrary)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Employees.ToList()) });
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        public IActionResult GetOkulTurleri()
        {
            List<School> okulTurleri =_context.Schools.ToList();
            return Json(okulTurleri);
        }
        [HttpPost]
        public IActionResult SaveGratuatedSchool(SaveSchoolViewModel model)
        {
            Thread.Sleep(3000);
            foreach (var item in model.SList)
            {
                GraduatedSchool graduated = new GraduatedSchool
                {
                    SchoolId=item.SchoolTypeId, 
                    GraduatedSchoolName=item.SchoolName
                };
                _context.Add(graduated);
                _context.SaveChanges();
            }


            var employe = _context.Employees.OrderByDescending(x => x.Id).Take(1).ToList();

            int employeeid = employe[0].Id;
           var gratuated =  _context.GraduatedSchools.OrderByDescending(x=>x.GraduatedSchoolId).Take(model.SList.Count).ToList();
            foreach (var item in gratuated)
            {
                
                _context.Database.ExecuteSqlRaw(
                    "INSERT INTO EmployeeSchools (EmployeeId , GraduatedSchoolId) " +
                    "VALUES ("+ employeeid+", "+item.GraduatedSchoolId+")");
                
                _context.SaveChanges();
            }
            return Ok();
        }

        public IActionResult SavedEmployeeGratuatedSchool(int id)
        {
                List<GraduatedSchool> schList = new List<GraduatedSchool>();

            var empSchool = _context.EmployeeSchools.Where(x => x.EmployeeId == id).ToList();
            for (int i = 0; i < empSchool.Count; i++)
            {
                var school = _context.GraduatedSchools.Where(x => x.GraduatedSchoolId == empSchool[i].GraduatedSchoolId).FirstOrDefault();
               schList.Add(school);
            }
          
      


            return Json(schList);
        }
    }
}

