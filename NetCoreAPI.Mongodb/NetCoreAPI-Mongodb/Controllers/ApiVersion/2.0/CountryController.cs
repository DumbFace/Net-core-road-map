using Domain.EFCore.Entites;
using Infrastucture.EFCore;
using Infrastucture.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreAPI_Mongodb.Controllers.BaseController;

namespace NetCoreAPI_Mongodb.Controllers.ApiVersion._2._0
{
    public class CountryController : BaseController_v2
    {
        private readonly SecondDbContext _context;
        public IUnitOfWork<SecondDbContext> _unitOfWorkSecondDb;
        //public IUnitOfWork<ExampleDbContext> _unitOfWorkExampleDb;

        public CountryController(
            SecondDbContext context,
            IUnitOfWork<SecondDbContext> unitOfWorkSecondDb,
            IUnitOfWork<ExampleDbContext> unitOfWorkExampleDb)
        {
            _context = context;
            _unitOfWorkSecondDb = unitOfWorkSecondDb;
            //_unitOfWorkExampleDb = unitOfWorkExampleDb;
        }

        // GET: api/Country
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            //var employees = await _unitOfWorkExampleDb.Context.Employees.ToListAsync();
            return await _unitOfWorkSecondDb.Context.Countries.ToListAsync();
        }

        //// GET: api/Country/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Country>> GetCountry(Guid id)
        //{
        //    var country = await _context.Countries.FindAsync(id);

        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    return country;
        //}

        //// PUT: api/Country/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCountry(Guid id, Country country)
        //{
        //    if (id != country.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(country).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CountryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Country
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Country>> PostCountry(Country country)
        //{
        //    _context.Countries.Add(country);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        //}

        //// DELETE: api/Country/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCountry(Guid id)
        //{
        //    var country = await _context.Countries.FindAsync(id);
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Countries.Remove(country);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CountryExists(Guid id)
        //{
        //    return _context.Countries.Any(e => e.Id == id);
        //}
    }
}
