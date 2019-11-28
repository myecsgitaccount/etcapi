using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EtcLibrary.API.Helpers;
using EtcLibrary.API.Models;
using EtcLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EtcLibrary.API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;

        
        public AuthorsController(ICourseLibraryRepository courseLibraryRepository)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository)); 
        }

        
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();
            //return new JsonResult(authorsFromRepo); 
            var authors = new List<AuthorDto>();

            foreach (var author in authorsFromRepo)
            {
                authors.Add(new AuthorDto()
                {
                    Id = author.Id,
                    Name = $"{author.FirstName} {author.LastName}",
                    MainCategory = author.MainCategory,
                    Age = author.DateOfBirth.GetCurrentAge()
                }); 
            }
            //return Ok(authorsFromRepo);
            return Ok(authors);
        }

        [HttpGet("{authorId}")]
        public IActionResult GetAuthorById(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId)) 
            {
                return NotFound(); 
            }
            var authorById = _courseLibraryRepository.GetAuthor(authorId);
            return Ok(authorById); 
        }
    }
}