using Microsoft.AspNetCore.Mvc;

namespace Bookify.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _context.Authors.AsNoTracking().ToList();

            var viewModel = _mapper.Map<IEnumerable<AuthorViewModel>>(categories);

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult Create()
        {
            return PartialView("_Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuthorFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var Author = _mapper.Map<Author>(model);
            _context.Add(Author);
            _context.SaveChanges();

            var viewModel = _mapper.Map<AuthorViewModel>(Author);

            return PartialView("_AuthorRow", viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public IActionResult Edit(int id)
        {
            var Author = _context.Authors.Find(id);

            if (Author is null)
                return NotFound();

            var viewModel = _mapper.Map<AuthorFormViewModel>(Author);

            return PartialView("_Form", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AuthorFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var Author = _context.Authors.Find(model.Id);

            if (Author is null)
                return NotFound();

            Author = _mapper.Map(model, Author);
            Author.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();

            var viewModel = _mapper.Map<AuthorViewModel>(Author);

            return PartialView("_AuthorRow", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {
            var Author = _context.Authors.Find(id);

            if (Author is null)
                return NotFound();

            Author.IsDeleted = !Author.IsDeleted;
            Author.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();

            return Ok(Author.LastUpdatedOn.ToString());
        }

        public IActionResult AllowItem(AuthorFormViewModel model)
        {
            var Author = _context.Authors.SingleOrDefault(c => c.Name == model.Name);
            var isAllowed = Author is null || Author.Id.Equals(model.Id);

            return Json(isAllowed);
        }
    }
}

