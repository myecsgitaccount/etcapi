using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        
        public IActionResult GetAuthors()
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors();
            //return new JsonResult(authorsFromRepo); 
            return Ok(authorsFromRepo); 
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